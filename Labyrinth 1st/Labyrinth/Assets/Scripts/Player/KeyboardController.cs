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
    [SerializeField]
    KeyCode invisible = KeyCode.RightShift;

    [Header("PlayerControllerScript")]
    [SerializeField]
    MonoBehaviour target;

    [Header("DialogueControls")]
    [SerializeField]
    KeyCode read = KeyCode.Tab;
    [SerializeField]
    KeyCode exitText = KeyCode.E;

    [Header("DialogueScript")]
    [SerializeField]
    DialogueManager dialogue;

    IControls playerCharacter;

    void Start() => playerCharacter = target as IControls;

    void Update()
    {
        if (Input.GetKey(forward)) playerCharacter.Forward();
        if (Input.GetKey(backward)) playerCharacter.Backward();
        if (Input.GetKey(right)) playerCharacter.Right();
        if (Input.GetKey(left)) playerCharacter.Left();
        if (Input.GetKey(jump)) playerCharacter.Jump();
        if (Input.GetKeyDown(drop)) playerCharacter.Drop();
        if (Input.GetKeyDown(pickup)) playerCharacter.PickUp();
        if (Input.GetKeyDown(invisible)) playerCharacter.GoInvisible();

        if (Input.GetKeyDown(read)) dialogue.Read();
        if (Input.GetKeyDown(exitText)) dialogue.BackToGame();
    }


}
