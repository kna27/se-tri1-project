using System.Collections;
using UnityEngine;

public class Bench : Interactable
{
    public int maxWeight;
    public int currentWeight = 0;
    [SerializeField] private GameObject[] leftPlates;
    [SerializeField] private GameObject[] rightPlates;
    [SerializeField] private Transform playerAnchor;
    Animator anim;
    Transform oldPlayerPos;
    GameObject player;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    public void AddWeight()
    {
        currentWeight++;
        leftPlates[currentWeight - 1].SetActive(true);
        rightPlates[currentWeight - 1].SetActive(true);
    }

    override public void Interact()
    {
        oldPlayerPos = player.transform;
        player.transform.SetParent(playerAnchor);
        player.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        player.GetComponent<Player>().movementEnabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        float scale = 1f;
        switch (currentWeight)
        {
            case 1:
                scale = 0.5f;
                break;
            case 2:
                scale = 0.33f;
                break;
            case 3:
                scale = 0.25f;
                break;
            default:
                scale = 1f;
                break;
        }
        anim.speed = scale;
        anim.Play("Bench");
        StartCoroutine(BenchAnim());
    }

    IEnumerator BenchAnim()
    {
        yield return new WaitForSeconds(2 + currentWeight * 2f);
        anim.Play("Default");
        GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(45 + currentWeight * 90);
        player.transform.SetParent(null);
        player.transform.SetPositionAndRotation(oldPlayerPos.position, oldPlayerPos.rotation);
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Player>().movementEnabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
