using System;
using UnityEngine;

namespace Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
