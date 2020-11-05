using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform[] targets;
    [SerializeField]

    private int destinationTarget = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        SetTarget();
    }

    void Update()
    { 
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            SetTarget();
    }

    void SetTarget()
    {

        agent.destination = targets[destinationTarget].position;

        destinationTarget = (destinationTarget + 1) % targets.Length;
    }
}
