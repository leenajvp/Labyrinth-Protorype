using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<Coins> coinsCollected = new List<Coins>();
    public List<Invisibility> invisibilityCapes = new List<Invisibility>();
    public List<DistractionItem> distractionItems = new List<DistractionItem>();

    [Header("Coins UI")]
    [SerializeField]
    Text numberOfCoins;

    [Header("InvisibilityCapes UI")]
    [SerializeField]
    Text numberOfCapes;
    [SerializeField]
    GameObject capeimage;

    private void Start()
    {
        numberOfCapes.enabled=false;
        capeimage.SetActive(false);
    }

    private void Update()
    {
        numberOfCoins.text = coinsCollected.Count.ToString();
        numberOfCapes.text = invisibilityCapes.Count.ToString();
    }

    //Collecting objects on trigger
    private void OnTriggerEnter(Collider collision)
    {
        Coins Coin = collision.GetComponent<Coins>();

        if (Coin != null)
        {
            coinsCollected.Add(Coin);
            Coin.gameObject.SetActive(false);
        }

        Invisibility Cape = collision.GetComponent<Invisibility>();
        Invisibility collectedCape = GetComponent<Invisibility>();

        if (Cape != null)
        {
            numberOfCapes.enabled = true;
            capeimage.SetActive(true);
            invisibilityCapes.Add(collectedCape);
            Cape.gameObject.SetActive(false);
        }

        if (invisibilityCapes.Count <= 0)
        {
            numberOfCapes.enabled = false;
            capeimage.SetActive(false);
        }
    }
}
