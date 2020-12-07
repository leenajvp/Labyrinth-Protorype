using UnityEngine;

public class DistractionItem : MonoBehaviour, IDistraction
{
    [SerializeField]
    float existenceTime;

    void Start()
    {
        Object.Destroy(gameObject, existenceTime);
    }
}
