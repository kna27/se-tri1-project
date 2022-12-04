using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float speed = 0.1f;
    public float fromY;
    public float toY;
    public float zPitch;
    Quaternion from;
    Quaternion to;
    bool playerInView;
    float timePlayerIsInView;

    void Start()
    {
        from = Quaternion.Euler(0, fromY, zPitch);
        to = Quaternion.Euler(0, toY, zPitch);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerInView)
        {
            float lerp = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * speed));
            transform.localRotation = Quaternion.Lerp(from, to, lerp);
        }
        else
        {
            timePlayerIsInView += Time.deltaTime;
            if (timePlayerIsInView > 1f)
            {
                CatchPlayer();
            }
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

    void CatchPlayer()
    {
        Debug.Log("caught player");
        timePlayerIsInView = 0f;
        GameObject.Find("GameManager").GetComponent<GameManager>().ChangeWantedness(10);
    }

}
