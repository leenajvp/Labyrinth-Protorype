using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
//Set sphere to trigger

public class InfoStop : MonoBehaviour, IReadable

{
    [Header("ReactToPlayer")]
    [SerializeField]
    GameObject player;

    [Header("SignDetails")]
    [SerializeField]
    string objectsName;

    //[SerializeField]
    // List<string> lines = new List<string>();
    public string message;

    //int crtLineIndex = 0;

    [Header("UI Elements")]
    [SerializeField]
    GameObject noticeSign;
    [SerializeField]
    GameObject advisePlayer;


    SphereCollider detectingCollider;

    // Start is called before the first frame update
    void Awake()
    {
        noticeSign.SetActive(false);
        advisePlayer.SetActive(false);
        detectingCollider = gameObject.GetComponent<SphereCollider>();
        detectingCollider.isTrigger = true;
    }

    public string GetString()
    {
            string result = objectsName + message;
            return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            noticeSign.SetActive(true);
            advisePlayer.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            noticeSign.SetActive(false);
            advisePlayer.SetActive(false);
        }
    }
}
