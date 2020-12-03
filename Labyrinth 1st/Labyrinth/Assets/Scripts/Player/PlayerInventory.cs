using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    
    public List<Coins> coinsCollected = new List<Coins>();

    [Header("Coins UI")]
    [SerializeField]
    Text numberOfCoins;

    private void Update()
    {
        numberOfCoins.text = coinsCollected.Count.ToString();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Coins Coin = collision.GetComponent<Coins>();

        if (Coin != null)
        {
           coinsCollected.Add(Coin);
           Coin.gameObject.SetActive(false);
        }
    }
}
