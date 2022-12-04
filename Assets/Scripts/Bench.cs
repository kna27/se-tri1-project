using System.Collections;
using UnityEngine;

public class Bench : Interactable
{
    public int maxWeight;
    public int currentWeight = 0;
    [SerializeField] private GameObject[] leftPlates;
    [SerializeField] private GameObject[] rightPlates;
    [SerializeField] private Transform playerAnchor;
    Transform oldPlayerPos;
    GameObject player;

    void Start()
    {
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
        StartCoroutine(BenchAnim());
    }

    IEnumerator BenchAnim()
    {
        yield return new WaitForSeconds(1 + currentWeight * 1.5f);
        GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(45 + currentWeight * 90);
        player.transform.SetParent(null);
        player.transform.SetPositionAndRotation(oldPlayerPos.position, oldPlayerPos.rotation);
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Player>().movementEnabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
