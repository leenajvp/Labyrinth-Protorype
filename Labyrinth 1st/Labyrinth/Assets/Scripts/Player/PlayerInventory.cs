using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Coins> keysCollected = new List<Coins>();

    private void OnTriggerEnter(Collider collision)
    {
        Coins Coin = collision.GetComponent<Coins>();

        if (Coin != null)
        {
            keysCollected.Add(Coin);

            Coin.gameObject.SetActive(false);
        }
    }
}
