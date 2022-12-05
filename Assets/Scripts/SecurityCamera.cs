using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private Light indicatorLight;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Color playerInViewColor = Color.red;
    [SerializeField] private Color playerNotInViewColor = Color.green;
    [SerializeField] private static float timeToCatchPlayer = 1f; // Time player must be in view to be caught
    [SerializeField] private static int wantednessIncrease = 10; // Amount to increase wantedness when player is caught
    [SerializeField] private float speed = 0.1f; // The speed the camera rotates
    [SerializeField] private float fromRot; // The minimum rotation of the camera in degrees
    [SerializeField] private float toRot; // The maximum rotation of the camera in degrees
    [SerializeField] private float pitch; // The amount to pitch the camera up or down

    Quaternion from;
    Quaternion to;
    private bool playerInView;
    private float timePlayerIsInView;
    private float totalTimePlayerIsInView;

    void Start()
    {
        from = Quaternion.Euler(pitch, fromRot, 90);
        to = Quaternion.Euler(pitch, toRot, 90);
        indicator.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (playerInView)
        {
            timePlayerIsInView += Time.deltaTime;
            totalTimePlayerIsInView += Time.deltaTime;
            if (timePlayerIsInView > timeToCatchPlayer)
            {
                CatchPlayer();
            }
            UpdateIndicatorColor(playerInViewColor);
        }
        else
        {
            // Only rotate camera if player is not in view
            transform.rotation = Quaternion.Lerp(from, to, Mathf.PingPong((Time.time - totalTimePlayerIsInView) * speed, 1));
            UpdateIndicatorColor(playerNotInViewColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInView = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInView = false;
            timePlayerIsInView = 0;
        }
    }

    void UpdateIndicatorColor(Color color)
    {
        indicatorLight.color = color;
        indicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }

    void CatchPlayer()
    {
        Debug.Log("caught player");
        timePlayerIsInView = 0f;
        GameObject.Find("GameManager").GetComponent<GameManager>().ChangeWantedness(wantednessIncrease);
    }
}
