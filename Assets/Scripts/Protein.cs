using UnityEngine;

public class Protein : Interactable
{
    private RandomProteinSpawner randomProteinSpawner;
    public GameObject GameManagerObject;

    private void Start()
    {
        randomProteinSpawner = GameManagerObject.GetComponent<RandomProteinSpawner>();
    }
    public override void Interact()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(10);
        randomProteinSpawner.proteinNotSpawned = true;
        Destroy(gameObject);
    }
}
