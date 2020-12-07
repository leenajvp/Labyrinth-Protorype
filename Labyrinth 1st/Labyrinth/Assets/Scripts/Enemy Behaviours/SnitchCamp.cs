using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchCamp : MonoBehaviour, IDefaultPos
{
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
