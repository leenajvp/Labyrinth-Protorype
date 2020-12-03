using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject requiredButton;

    [Header("UI settings")]
    [SerializeField]
    GameObject lvlComplete;

    private void Start()
    {
        lvlComplete.SetActive(false);
    }

    private void Update()
    {
        bool activated = requiredButton.GetComponent<Button>().activated;

        if (activated == true)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }

        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool activated = requiredButton.GetComponent<Button>().activated;

        if (other.gameObject == player && activated == true)
        {
            PlayerInventory inventory = other.gameObject.GetComponent<PlayerInventory>();

            if (inventory.coinsCollected.Count == 10)
            {
                lvlComplete.SetActive(true);
                LevelCompleted();
            }
        }
    }

    void LevelCompleted()
    {
        Time.timeScale = 0f;
    }

}
