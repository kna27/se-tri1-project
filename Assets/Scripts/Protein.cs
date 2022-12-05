using UnityEngine;

public class Protein : Interactable
{
    public override void Interact()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(10);
        Destroy(gameObject);
    }
}
