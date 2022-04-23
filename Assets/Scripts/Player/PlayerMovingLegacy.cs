using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovingLegacy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float _speed = 3.0f;

    void Update()
    {
        if (!Input.anyKey) return;

        var deltaX = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            deltaX = -Time.deltaTime * _speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            deltaX = Time.deltaTime * _speed;
        }

        var deltaY = Input.GetAxis("Vertical");
        var position = player.transform.position;

        player.transform.position = new Vector3(
            position.x + deltaX,
            position.y + deltaY
        );
    }
}