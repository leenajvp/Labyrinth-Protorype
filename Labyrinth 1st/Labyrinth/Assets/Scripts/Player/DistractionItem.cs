using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionItem : MonoBehaviour, IDistraction
{
    void Start()
    {
        Object.Destroy(gameObject, 10.0f);
    }
}
