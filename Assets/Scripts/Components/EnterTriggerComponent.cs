using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private string objectTag;
    [SerializeField] private UnityEvent action;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(objectTag))
        {
            action?.Invoke();
        }
    }
}
