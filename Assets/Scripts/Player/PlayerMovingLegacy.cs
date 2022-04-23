using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovingLegacy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float _speed = 3.0f;

    /// <summary>
   /// Update is called every frame, if the MonoBehaviour is enabled.
   /// </summary>
   void Update()
   {
       if (Input.GetKey(KeyCode.A))
       {
           var position = player.transform.position;
           player.transform.position = new Vector3(position.x - Time.deltaTime * _speed, position.y);
       }
       else if (Input.GetKey(KeyCode.D))
       {
           var position = player.transform.position;
           player.transform.position = new Vector3(position.x + Time.deltaTime * _speed, position.y);
       }
       
   }
}
