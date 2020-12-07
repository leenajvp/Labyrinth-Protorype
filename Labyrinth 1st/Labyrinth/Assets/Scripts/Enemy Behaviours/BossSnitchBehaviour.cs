using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSnitchBehaviour : MonoBehaviour, IBossSnitch
{
    public enum BossSnitchState
    {
        Camp,
        SnitchPlayer
    }

    [Header("BossSnitchSettings")]
    [SerializeField]
    float mSpeed = 2.0f;
    [SerializeField]
    float detectingRadius = 1.0f;

    [Header("PlayerDetection")]
    [SerializeField]
    GameObject gameBoss;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float slowPlayerTimer;

    private float speed;
    private Vector3 playerPos;
    private Vector3 defaultPos;
    private CapsuleCollider snitchCol;
    private bool playerTouched;

    private GameBoss gameBossScript => gameBoss.GetComponent<GameBoss>();
    private List<GameObject> nearbyBoids = new List<GameObject>(10);
    
    public BossSnitchState CurrentState;

    void Start()
    {
        speed = mSpeed;

        snitchCol = gameObject.GetComponent<CapsuleCollider>();
        snitchCol.isTrigger = true;
    }

    void Update()
    {
        switch (CurrentState)
        {
            case BossSnitchState.Camp:

                Observe();
                TurnTo(defaultPos);
                Avoid();
                Move();
                Camping();
                gameObject.GetComponent<Renderer>().material.color = Color.green;

                break;

            case BossSnitchState.SnitchPlayer:

                FindPLayer();
                TurnTo(playerPos);
                Move();
                gameObject.GetComponent<Renderer>().material.color = Color.red;

                break;

            default:

                CurrentState = BossSnitchState.Camp;

                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            gameBossScript.playerDetected = true;
            playerTouched = true;
            CurrentState = BossSnitchState.SnitchPlayer;
            StartCoroutine(HoldPlayer());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            gameBossScript.playerDetected = false;
            playerTouched = false;
            CurrentState = BossSnitchState.Camp;
        }
    }

     IEnumerator HoldPlayer()
    {
        var playerController = player.GetComponent<PlayerController>();

        playerController.speed = playerController.slowedSpeed;
        yield return new WaitForSeconds(slowPlayerTimer);
        playerController.speed = playerController.defaultSpeed;
    }

    void Observe()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectingRadius);
        nearbyBoids.Clear();

        foreach (var col in colliders)
        {
            ISnitch littleSnitch = col.gameObject.GetComponent<ISnitch>();

            if (littleSnitch != null)
            {
                nearbyBoids.Add(col.gameObject);
            }
        }
    }

    void Camping()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectingRadius);

        foreach (var col in colliders)
        {
            IDefaultPos snitchcamp = col.gameObject.GetComponent<IDefaultPos>();

            if (snitchcamp != null && playerTouched == false)
            {
                defaultPos = snitchcamp.GetPosition();
                TurnTo(defaultPos);
            }

            if (playerTouched == true)
            {
                CurrentState = BossSnitchState.SnitchPlayer;
            }
        }
    }

    void FindPLayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectingRadius);

        foreach (var col in colliders)
        {
            IPlayer player = col.gameObject.GetComponent<IPlayer>();

            if (player != null)
            {
                playerPos = player.GetPosition();
                TurnTo(playerPos);
            }
        }
    }

    void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    // Boss snitch still want to avoid other snitches
    void Avoid()
    {
        if (nearbyBoids.Count > 0)
        {
            for (int i = 0; i < nearbyBoids.Count; ++i)
            {
                Vector3 toNeighbour = nearbyBoids[i].transform.position - transform.position;
                TurnTo(transform.position - toNeighbour);
            }
        }
    }

    void TurnTo(Vector3 turnTarget)
    {
        Quaternion lookAt = Quaternion.LookRotation(transform.forward, transform.up);
        Quaternion fromTo = Quaternion.FromToRotation(transform.up, (turnTarget - transform.position));

        Quaternion centerRot = fromTo * lookAt;

        float angleDiff = Quaternion.Angle(centerRot, transform.rotation) * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, centerRot, angleDiff);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
