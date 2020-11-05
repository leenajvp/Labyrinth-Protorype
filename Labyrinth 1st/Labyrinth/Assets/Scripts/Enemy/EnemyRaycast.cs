using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRaycast : MonoBehaviour
{
    private Ray castedRay;
    private RaycastHit hit;

    [SerializeField]
    bool playerDetected = false;

    private NavMeshAgent agent;
    private PlayerController playerSript;

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject distraction;
    [SerializeField]
    float stopDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerSript = player.GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       // castedRay = new Ray(transform.position, transform.forward * 20);
        Debug.DrawRay(transform.position, transform.forward * 50);

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject == player)
                
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

            else if (hit.collider.gameObject == distraction)

            {
                agent.SetDestination(distraction.transform.position);
                Debug.Log("Distracted");

                if (Vector3.Distance(transform.position, distraction.transform.position) < stopDistance)
                {
                    agent.isStopped = true;
                }
            }

            else
            {
                agent.isStopped = false;
            }

            if (hit.collider.gameObject != player)
                
            {
                playerDetected = false;
                Debug.Log("Wall hit");
            }
        }
    }      
}

