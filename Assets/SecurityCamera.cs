using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float speed = 0.1f;
    public float fromRot;
    public float toRot;
    public float pitch;
    Quaternion from;
    Quaternion to;
    bool playerInView;
    float timePlayerIsInView;
    float totalTimePlayerIsInView;
    public Light indicatorLight;
    public GameObject indicator;

    void Start()
    {
        from = Quaternion.Euler(pitch, fromRot, 90);
        to = Quaternion.Euler(pitch, toRot, 90);
        indicator.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerInView)
        {
            transform.rotation = Quaternion.Lerp(from, to, Mathf.PingPong((Time.time - totalTimePlayerIsInView) * speed, 1));
            UpdateIndicatorColor(Color.green);
        }
        else
        {
            timePlayerIsInView += Time.deltaTime;
            totalTimePlayerIsInView += Time.deltaTime;
            if (timePlayerIsInView > 1f)
            {
                CatchPlayer();
            }
            UpdateIndicatorColor(Color.red);
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
        GameObject.Find("GameManager").GetComponent<GameManager>().ChangeWantedness(10);
    }

}
