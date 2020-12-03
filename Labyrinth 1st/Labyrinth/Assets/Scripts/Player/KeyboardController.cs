using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [Header("ControlScrip")]
    [SerializeField]
    KeyCode forward = KeyCode.UpArrow;
    [SerializeField]
    KeyCode backward = KeyCode.DownArrow;
    [SerializeField]
    KeyCode right = KeyCode.RightArrow;
    [SerializeField]
    KeyCode left = KeyCode.LeftArrow;
    [SerializeField]
    KeyCode jump = KeyCode.Space;
    [SerializeField]
    KeyCode drop = KeyCode.LeftControl;
    [SerializeField]
    KeyCode pickup = KeyCode.LeftAlt;

    [Header("Player character script")]
    [SerializeField]
    MonoBehaviour target;

    Controls playerCharacter;

    [Header("Dialogue")]
    [SerializeField]
    KeyCode read = KeyCode.Tab;
    [SerializeField]
    KeyCode exitText = KeyCode.E;
    [Header("DialogueScript")]
    [SerializeField]
    DialogueManager dialogue;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = target as Controls;
        //dialogue = GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(forward)) playerCharacter.Forward();
        if (Input.GetKey(backward)) playerCharacter.Backward();
        if (Input.GetKey(right)) playerCharacter.Right();
        if (Input.GetKey(left)) playerCharacter.Left();
        if (Input.GetKey(jump)) playerCharacter.Jump();
        if (Input.GetKeyDown(drop)) playerCharacter.Drop();
        if (Input.GetKeyDown(pickup)) playerCharacter.PickUp();

        if (Input.GetKeyDown(read)) dialogue.Read();
        if (Input.GetKeyDown(exitText)) dialogue.BackToGame();

    }


}
