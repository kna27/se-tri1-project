using UnityEngine;

public class Plates : Interactable
{
    Bench bench;
    
    void Start()
    {
        bench = GameObject.Find("Bench").GetComponent<Bench>();
    }
    public override void Interact()
    {
        bench.AddWeight();
        if (bench.currentWeight >= bench.maxWeight)
        {
            Destroy(gameObject);
        }
    }
}
