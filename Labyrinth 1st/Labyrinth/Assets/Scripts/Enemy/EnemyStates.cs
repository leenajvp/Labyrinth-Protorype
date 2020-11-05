using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        PlayerSeen,
        Distracted
    }

    private int destinationTarget = 0;
    private NavMeshAgent agent;
    private RaycastHit hit;
    private PlayerController playerSript;

    [Header("Patrol")]
    [SerializeField]
    Transform[] targets;

    [Header("PlayerDetection")]
    [SerializeField]
    bool playerDetected = false;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject distraction;
    [SerializeField]
    float stopDistance = 3f;

    public EnemyState CurrentState;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        playerSript = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
            RaycastCheck();

        Debug.DrawRay(transform.position, transform.forward * 50);

        switch (CurrentState)
        {
            case EnemyState.Patrol:

                agent.isStopped = false;
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    Patrolling();
                break;

            case EnemyState.PlayerSeen:

                GoToPlayer();
                
                break;

            case EnemyState.Distracted:

                DistractionDetected();
                break;

            default:
                break;
        }
    }

    void RaycastCheck()
    {
        if (hit.collider.gameObject == player)
        {
            CurrentState = EnemyState.PlayerSeen;
        }

        else if (hit.collider.CompareTag("Distraction"))
        {
            CurrentState = EnemyState.Distracted;
        }

        else
        {
            CurrentState = EnemyState.Patrol;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Distraction"))
        {
            CurrentState = EnemyState.Distracted;
        }
    }

    void Patrolling()
    {
        agent.isStopped = false;
        agent.destination = targets[destinationTarget].position;
        destinationTarget = (destinationTarget + 1) % targets.Length;
    }

    void GoToPlayer()
    {
        playerDetected = true;
        agent.SetDestination(player.transform.position);
        playerSript.playerCaught();
        Debug.Log("Player hit");

        if (Vector3.Distance(transform.position, player.transform.position) <= stopDistance)

        {
            agent.isStopped = true;
        }
    }

    void DistractionDetected()
    {
        agent.SetDestination(GameObject.FindWithTag("Distraction").transform.position);
        Debug.Log("Distracted");

        if (Vector3.Distance(transform.position, GameObject.FindWithTag("Distraction").transform.position) < stopDistance)
        {
            agent.isStopped = true;
        }
    }
}
