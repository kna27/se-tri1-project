using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private Light indicatorLight;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Color playerInViewColor = Color.red;
    [SerializeField] private Color playerNotInViewColor = Color.green;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float speed = 0.1f; // The speed the camera rotates
    [SerializeField] private float fromRot; // The minimum rotation of the camera in degrees
    [SerializeField] private float toRot; // The maximum rotation of the camera in degrees
    [SerializeField] private float pitch; // The amount to pitch the camera up or down
    Quaternion from;
    Quaternion to;

    void Start()
    {
        from = Quaternion.Euler(pitch, fromRot, 90);
        to = Quaternion.Euler(pitch, toRot, 90);
        indicator.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (enemy.playerInView)
        {
            enemy.timePlayerIsInView += Time.deltaTime;
            enemy.totalTimePlayerIsInView += Time.deltaTime;
            if (enemy.timePlayerIsInView > enemy.timeToCatchPlayer)
            {
                CatchPlayer();
            }
            UpdateIndicatorColor(playerInViewColor);
        }
        else
        {
            // Only rotate camera if player is not in view
            transform.rotation = Quaternion.Lerp(from, to, Mathf.PingPong((Time.time - enemy.totalTimePlayerIsInView) * speed, 1));
            UpdateIndicatorColor(playerNotInViewColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Physics.Raycast(transform.position, other.transform.position - transform.position, out RaycastHit hit))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    enemy.playerInView = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.playerInView = false;
            enemy.timePlayerIsInView = 0;
        }
    }

    void UpdateIndicatorColor(Color color)
    {
        indicatorLight.color = color;
        indicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }

    void CatchPlayer()
    {
        enemy.timePlayerIsInView = 0f;
        GameObject.Find("GameManager").GetComponent<GameManager>().ChangeWantedness(enemy.wantednessIncrease);
    }
}
