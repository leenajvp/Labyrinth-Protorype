using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Items")]
    [SerializeField]
    GameObject informationPanel;    
    [SerializeField]
    Text textArea;

    private float lastInteraction = 0.0f;

    void Start()
    {
        informationPanel.SetActive(false);
    }

    void SetTextArea(string text)
    {
        textArea.text = text;
    }

    public void Read()
    {
        if (Time.time < lastInteraction + 1.0f)
            return;

        lastInteraction = Time.time;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);

        foreach (var col in colliders)
        {
            IReadable interactible = col.GetComponent<IReadable>();

            if (interactible != null)
            {
                SetTextArea(interactible.GetString());
                informationPanel.SetActive(true);
                break;
            }
        }
    }

    public void BackToGame()
    {
        informationPanel.SetActive(false);
    }
}
