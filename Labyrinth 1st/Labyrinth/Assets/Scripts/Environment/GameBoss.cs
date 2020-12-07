using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameBoss : MonoBehaviour
{
    public bool playerDetected;
    public void Start() => playerDetected = false;

    [Header("Enemy UI")]
    public GameObject spottedImage;
    public GameObject youLost;

    public void EndGame()
    {
        Application.Quit();
    }
}
