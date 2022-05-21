using UnityEngine;

namespace Components
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationKey;

        public void Switch()
        {
            _animator.SetTrigger(_animationKey);
        }

        [ContextMenu("Switch")]
        private void SwitchIt()
        {
            Switch();
        }
    }
}
