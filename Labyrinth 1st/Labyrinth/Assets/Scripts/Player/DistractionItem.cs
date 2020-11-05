using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionItem : MonoBehaviour
{
    void Start()
    {
        Object.Destroy(gameObject, 30.0f);

    }
}
