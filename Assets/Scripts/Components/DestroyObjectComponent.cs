using UnityEngine;

namespace Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject objectForDestroy;

        private void OnTriggerEnter2D(Collider2D col)
        {
            Destroy(objectForDestroy);
        }
    }
}
