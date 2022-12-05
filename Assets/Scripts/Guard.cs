using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform[] targets;
    [SerializeField] private Enemy enemy;
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
        if (enemy.playerInView)
        {
            enemy.timePlayerIsInView += Time.deltaTime;
            enemy.totalTimePlayerIsInView += Time.deltaTime;
            if (enemy.timePlayerIsInView > enemy.timeToCatchPlayer)
            {
                enemy.timePlayerIsInView = 0f;
                GameObject.Find("GameManager").GetComponent<GameManager>().ChangeWantedness(enemy.wantednessIncrease);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // check if raycast from guard to player hits player
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    enemy.playerInView = true;
                    nav.stoppingDistance = 2f;
                    nav.destination = player.transform.position;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.playerInView = false;
            enemy.timePlayerIsInView = 0;
            nav.stoppingDistance = 0f;
            nav.destination = targets[targetIndex].position;
        }
    }
}
