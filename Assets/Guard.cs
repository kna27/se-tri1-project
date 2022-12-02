using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public GameObject player;
    public Transform currentTarget;
    public Transform[] targets;
    int index;
    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.destination = targets[index].position;
    }

    void Update()
    {
        float dist = Vector3.Distance(targets[index].position, transform.position);
        currentTarget = targets[index];

        if (dist < 2)
        {
            index = index < targets.Length - 1 ? index + 1 : 0;
            nav.destination = targets[index].position;
        }
    }
}
