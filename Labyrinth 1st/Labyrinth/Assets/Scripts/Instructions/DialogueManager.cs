using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    GameObject informationPanel;    


    [SerializeField]
    Text textArea;

    float lastInteraction = 0.0f;

    //float maxCanvasLife = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        informationPanel.SetActive(false);
    }

    // Update is called once per frame


    void setTextArea(string text)
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
                setTextArea(interactible.GetString());
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
