using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour, IControls, IPlayer
{
    [Header("Movement")]
    public float speed;
    public float defaultSpeed = 10.0f;
    public float slowedSpeed = 3f;
    [SerializeField]
    float rotationSpeed = 70.0f;

    [Header("Jumping")]
    [SerializeField]
    float jumpHeight = 7f;
    [SerializeField]
    bool isGrounded;

    [Header ("Invisibility")]
    [SerializeField]
    bool invisible = false;
    [SerializeField]
    GameObject invisibility;
    [SerializeField]
    float invisiBilityTimer = 5;

    [Header("DistractionItems")]
    [SerializeField]
    GameObject objectToDrop;
    [SerializeField]
    GameObject[] distractionUI;
    [SerializeField]
    int distractionsHeld = 3;

    private PlayerInventory playerInventory;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInventory = gameObject.GetComponent<PlayerInventory>();
        speed = defaultSpeed;
    }

    void Update()
    {
        CountDistractions();
    }

    public void CountDistractions()
    {
        for (int i = 0; i < distractionUI.Length; i++)
        {
            if (distractionsHeld > i)
            {
                distractionUI[i].SetActive(true);
            }
            else
            {
                distractionUI[i].SetActive(false);
            }
        }
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
        Debug.Log("pick");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);

        foreach (var col in colliders)
        {
            IDistraction droppedDistraction = col.gameObject.GetComponent<IDistraction>();

            if (droppedDistraction != null)
            {
                Debug.Log("YAY");
                distractionsHeld++;
                col.gameObject.SetActive(false);

            }
        }
    }

    public void Drop()
    {
        if (distractionsHeld != 0)
        {
            GameObject.Instantiate(objectToDrop, transform.position - Vector3.forward, Quaternion.identity);
            distractionsHeld--;
        }
    }

    public void DroppingTimer()
    {
        GameObject.Instantiate(objectToDrop, transform.position - Vector3.forward, Quaternion.identity);
    }

    public void GoInvisible()
    {
        var collectedCapes = playerInventory.invisibilityCapes;
        Invisibility usedCape = GetComponent<Invisibility>();

        if (collectedCapes.Count >= 1 && invisible == false)
        {
            invisible = true;
            collectedCapes.Remove(usedCape);
            StartCoroutine(InvisibilityTimer());
        }
    }

    IEnumerator InvisibilityTimer()
    {
        invisibility.SetActive(true);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
        Debug.Log("Invisiblee");

        yield return new WaitForSeconds(invisiBilityTimer);

        invisible = false;
        invisibility.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        Debug.Log("nooo");
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

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
