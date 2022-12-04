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
        player.GetComponent<Player>().movementEnabled = false;
        GetComponent<Collider>().enabled = false;
        player.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        StartCoroutine(BenchAnim());
    }

    IEnumerator BenchAnim()
    {
        yield return new WaitForSeconds(1 + currentWeight * 1.5f);
        Debug.Log("2: " + oldPlayerPos.position + " | " + player.transform.position);
        GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(45 + currentWeight * 90);
        player.transform.SetParent(null);
        player.transform.SetPositionAndRotation(oldPlayerPos.position, oldPlayerPos.rotation);
        Debug.Log("3: " + oldPlayerPos.position + " | " + player.transform.position);
        player.GetComponent<Player>().movementEnabled = true;
        GetComponent<Collider>().enabled = true;
        Debug.Log("4: " + oldPlayerPos.position + " | " + player.transform.position);
    }
}
