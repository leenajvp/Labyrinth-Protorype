using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
//Set sphere to trigger

public class InfoStop : MonoBehaviour, IReadable

{
    [SerializeField]
    string objectsName;

    [SerializeField]
    List<string> lines = new List<string>();

    int crtLineIndex = 0;

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

    // Update is called once per frame
    void Update()
    {

    }

    public string GetString()
    {
            string result = objectsName + ": " + lines[crtLineIndex];
            if (crtLineIndex < lines.Count - 1) crtLineIndex++;
            return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        noticeSign.SetActive(true);
        advisePlayer.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        noticeSign.SetActive(false);
        advisePlayer.SetActive(false);
    }
}
