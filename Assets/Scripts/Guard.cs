using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform[] targets;
    [SerializeField] private static float timeToCatchPlayer = 1f; // Time player must be in view to be caught
    [SerializeField] private static int wantednessIncrease = 20; // Amount to increase wantedness when player is caught
    private bool playerInView;
    private float timePlayerIsInView;
    private float totalTimePlayerIsInView;
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
        if (playerInView)
        {
            timePlayerIsInView += Time.deltaTime;
            totalTimePlayerIsInView += Time.deltaTime;
            if (timePlayerIsInView > timeToCatchPlayer)
            {
                timePlayerIsInView = 0f;
                GameObject.Find("GameManager").GetComponent<GameManager>().ChangeWantedness(wantednessIncrease);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // check if raycast from guard to player hits player
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out _))
            {
                playerInView = true;
                nav.stoppingDistance = 2f;
                nav.destination = player.transform.position;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInView = false;
            timePlayerIsInView = 0;
            nav.stoppingDistance = 0f;
            nav.destination = targets[targetIndex].position;
        }
    }
}
