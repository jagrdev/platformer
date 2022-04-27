using UnityEngine;

public class JumpDetector : MonoBehaviour
{
    [SerializeField] private Hero hero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        hero.CanJump = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        hero.CanJump = false;
    }
}
