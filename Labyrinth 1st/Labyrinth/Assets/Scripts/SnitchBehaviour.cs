using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchBehaviour : MonoBehaviour
{
    [SerializeField]
    float baseSpeed = 10.0f;

    [SerializeField]
    float sightRadius = 5.0f;

    [SerializeField]
    Vector3 gravityPoint = new Vector3(0.0f, 0.0f, 0.0f);

    float speed;

    List<GameObject> nearbyBoids = new List<GameObject>(10);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Snitch";

        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Observe();

        Align();
        Cohere();
        Avoid();

        TurnTo(gravityPoint);

        Move();
    }

    void Observe()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRadius);
        nearbyBoids.Clear();
        foreach (var col in colliders)
        {
            if (col.gameObject != gameObject && col.gameObject.tag == "Snitch")
            {
                //Debug.Log(col.gameObject);
                nearbyBoids.Add(col.gameObject);
            }
        }
        //Debug.Log(colliders.Length);
        //Debug.Log("Nearby boids: "+nearbyBoids.Count);
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

    void Cohere()
    {
        if (nearbyBoids.Count > 0)
        {
            Vector3 centerOfMass = transform.position;
            for (int i = 0; i < nearbyBoids.Count; ++i)
            {
                centerOfMass += nearbyBoids[i].transform.position;
            }
            centerOfMass /= nearbyBoids.Count + 1;

            TurnTo(centerOfMass);
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
