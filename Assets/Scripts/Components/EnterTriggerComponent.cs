using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private String objectTag;
    [SerializeField] private UnityEvent action;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(objectTag))
        {
            action?.Invoke();
        }
    }
}
