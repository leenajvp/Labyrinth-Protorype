using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, Controls, IPlayer
{
    [SerializeField]
    float speed = 10.0f;
    [SerializeField]
    float rotationSpeed = 70.0f;
    [SerializeField]
    GameObject objectToDrop;

    [Header("Jumping")]
    [SerializeField]
    float jumpHeight = 7f;
    [SerializeField]
    bool isGrounded;

   // float shootingWait = 1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    public void Forward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void Backward()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
    }

    public void Left()
    {
        transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
    }

    public void Right()
    {
        transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
    }

    public void Jump()
    {
        if (isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
        
    }

    public void playerCaught()
    {
        speed = 0;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void PickUp()
    {

    }

    public void Drop()
    {
        GameObject.Instantiate(objectToDrop, transform.position - Vector3.forward, Quaternion.identity);
    }

    public void DroppingTimer()
    {
        GameObject.Instantiate(objectToDrop, transform.position - Vector3.forward, Quaternion.identity);
    }



    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

}
