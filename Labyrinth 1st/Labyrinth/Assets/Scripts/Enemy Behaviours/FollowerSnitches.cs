using System.Collections.Generic;
using UnityEngine;

public class FollowerSnitches : MonoBehaviour, ISnitch
{
    [SerializeField]
    float mSpeed = 2.0f;
    [SerializeField]
    float detectingRadius = 1.0f;

    private Vector3 bossSnitchPos;
    private float speed;

    List<GameObject> nearbyBoids = new List<GameObject>(10);

    void Start() => speed = mSpeed;

    void Update()
    {
        Observe(); //Check for other snitches
        Align();    // Line up with them   
        TurnTo(bossSnitchPos); //Chase boss
        Avoid();    //do not collide with other snitches
        Move();     
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
        
            IBossSnitch bossSnitch = col.gameObject.GetComponent<IBossSnitch>();

            if (bossSnitch != null)
            {
                bossSnitchPos = bossSnitch.GetPosition();
                TurnTo(bossSnitchPos);
            }
        }
    }

    void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void Align()
    {
        if (nearbyBoids.Count > 0)
        {
            for (int i = 0; i < nearbyBoids.Count; ++i)
            {
                float angleDiff = Quaternion.Angle(transform.rotation, nearbyBoids[i].transform.rotation) * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, nearbyBoids[i].transform.rotation, angleDiff);
            }
        }
    }

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
        Quaternion lookRot = Quaternion.LookRotation(transform.forward, transform.up);
        Quaternion fromTo = Quaternion.FromToRotation(transform.up, (turnTarget - transform.position));

        Quaternion centerRot = fromTo * lookRot;

        float angleDiff = Quaternion.Angle(centerRot, transform.rotation) * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, centerRot, angleDiff);
    }
}

