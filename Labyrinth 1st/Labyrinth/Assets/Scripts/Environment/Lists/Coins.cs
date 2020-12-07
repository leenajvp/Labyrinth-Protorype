using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour, ICoin
{
    void Update()
    {
        transform.Rotate(1.5f, 0, 0 * Time.deltaTime);
    }
}
