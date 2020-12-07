using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyStates : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        PlayerSeen,
        PlayerCatched,
        Distracted,
        Snitched
    }

    [Header("Patrol")]
    [SerializeField]
    Transform[] targets;

    [Header("PlayerDetection")]
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject distraction;
    [SerializeField]
    float catchedDistance = 10;
    [SerializeField]
    float lostDistance = 20f;
    [SerializeField]
    float stopDistance = 3f;
    [SerializeField]
    GameObject gameBoss;

    private NavMeshAgent agent => GetComponent<NavMeshAgent>();
    private float defaultSpeed => agent.speed = 3.5f;
    private float runningSpeed => agent.speed = 7;
    private float speed;

    private int destinationTarget = 0;
    private RaycastHit hit;

    private PlayerController playerSript => player.GetComponent<PlayerController>();

    public EnemyState CurrentState;

    private void Start() => agent.autoBraking = false;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        Debug.DrawRay(transform.position, transform.forward * 50);

        switch (CurrentState)
        {
            case EnemyState.Patrol:

                RaycastCheck();
                speed = defaultSpeed;
                agent.isStopped = false;

                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    Patrolling();

                    break;

            case EnemyState.PlayerSeen:
                
                GoToPlayer();
                speed = runningSpeed;

                break;

            case EnemyState.PlayerCatched:
                PlayerCatched();
                break;

            case EnemyState.Distracted:

                RaycastCheck();
                DistractionDetected();
                speed = runningSpeed;

                break;

            case EnemyState.Snitched:

                PLayerBeenSnitched();
                RaycastCheck();
                speed = runningSpeed;

                break;

            default:

                CurrentState = EnemyState.Patrol;

                break;
        }
    }

    void RaycastCheck()
    {
        if (hit.collider == null)
        {
            return;
        }

        else
        {
            var hitDistraction = hit.collider.GetComponent<IDistraction>();

            if (hitDistraction != null)
            {
                distraction = hit.collider.gameObject;
                CurrentState = EnemyState.Distracted;
            }

            if (hit.collider.gameObject == player)
            {
                CurrentState = EnemyState.PlayerSeen;
            }
        }
    }

    void Patrolling()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;

        agent.destination = targets[destinationTarget].position;
        destinationTarget = (destinationTarget + 1) % targets.Length;

        gameBoss.GetComponent<GameBoss>().spottedImage.SetActive(false);

        if (gameBoss.GetComponent<GameBoss>().playerDetected == true)
        {
            CurrentState = EnemyState.Snitched;
        }
    }

    void PLayerBeenSnitched()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        if (gameBoss.GetComponent<GameBoss>().playerDetected == true)
        {
            agent.SetDestination(player.transform.position);
        }

        else
        {
            CurrentState = EnemyState.Patrol;
        }
    }

    void GoToPlayer()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        agent.SetDestination(player.transform.position);

        gameBoss.GetComponent<GameBoss>().spottedImage.SetActive(true);

        if (Vector3.Distance(transform.position, player.transform.position) <= catchedDistance)
        {
            CurrentState = EnemyState.PlayerCatched;
        }

        if (Vector3.Distance(transform.position, player.transform.position) >= lostDistance)
        {
            CurrentState = EnemyState.Patrol;
        }
    }

    void PlayerCatched()
    {
        if(Vector3.Distance(transform.position,player.transform.position) <= catchedDistance)
        {
            playerSript.playerCaught();
            gameBoss.GetComponent<GameBoss>().youLost.SetActive(true);
        }
        
        if (Vector3.Distance(transform.position, player.transform.position) <= stopDistance)

        {
            agent.isStopped = true;
        }
    }

    void DistractionDetected()
    {
        if (distraction == null) CurrentState = EnemyState.Patrol;

        while (distraction != null)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            agent.SetDestination(distraction.transform.position);

            if (Vector3.Distance(transform.position, distraction.transform.position) <= stopDistance)
            {
                agent.isStopped = true;
            }
            break;
        }
    }
}
