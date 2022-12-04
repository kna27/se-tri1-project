using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform[] targets;
    NavMeshAgent nav;
    int targetIndex;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        // Set agent's destination to the first target
        nav.destination = targets[targetIndex].position;
    }

    void Update()
    {
        currentTarget = targets[targetIndex];
        float dist = Vector3.Distance(currentTarget.position, transform.position);
        if (dist < 2)
        {
            // Set new target to the next target in the array unless at end of array, then set to first in array
            targetIndex = targetIndex < targets.Length - 1 ? targetIndex + 1 : 0;
            nav.destination = targets[targetIndex].position;
        }
    }
}
