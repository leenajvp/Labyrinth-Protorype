using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    Vector3 pressedScale;
    [SerializeField]
    Vector3 defaultScale;
    public bool activated = false;

    private void Start()
    {
        pressedScale = new Vector3(1, 0.01f, 1);
        defaultScale = new Vector3(1, 1f, 1);       
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.transform.localScale = pressedScale;
        Debug.Log("yay motherfuckers");
        activated = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        gameObject.transform.localScale = defaultScale;
        Debug.Log("boo motherfuckers");
        activated = false;
    }
}
