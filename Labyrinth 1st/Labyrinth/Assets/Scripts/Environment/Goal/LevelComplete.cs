using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [Header("PassingRequirements")]
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject requiredButton;
    [SerializeField]
    float requiredAmountCoins;

    [Header("UI settings")]
    [SerializeField]
    GameObject lvlComplete;

    private PlayerInventory coinsCount;

    private void Start()
    {
        lvlComplete.SetActive(false);
        coinsCount = player.GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        bool activated = requiredButton.GetComponent<Button>().activated;

        if (activated == true && coinsCount.coinsCollected.Count == requiredAmountCoins)
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

            if (inventory.coinsCollected.Count == requiredAmountCoins)
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
