using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider),typeof(Rigidbody))]

public class PushBoxes : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        rb.mass = 0.01f;
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
