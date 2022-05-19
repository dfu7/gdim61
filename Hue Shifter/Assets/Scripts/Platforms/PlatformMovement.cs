using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed = 10;
    [SerializeField] private GameColor color;
    [SerializeField] private Collider collisionBox;

    void Start()
    {
       //SetColor(Random.Range(0, 1.0f) > 0.5 ? GameColor.RED : GameColor.BLUE);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.fixedDeltaTime * -100;
        if (transform.position.z <= -40)
        {
            Destroy(gameObject);
        }
    }

   /* void SetColor(GameColor _color)
    {
        color = _color;
        GetComponent<MeshRenderer>().material = colors[(int)_color];
    }
   */

    //Code for allowing player to ride on top of the platform
    // & only enable hitbox when player is of the right color
    private void OnTriggerEnter(Collider collider)
    {
        // get valid reference to player
        PlayerColor player = collider.gameObject.GetComponentInParent<PlayerColor>();

        if(player != null) {
            collisionBox.enabled = player.GetColor() == color;          // set platform collision box
            player.transform.parent = transform;                        // set player parent to let them stay on platform
            player.gameObject.GetComponent<PlayerMovement>().groundDrag = 0;
            player.gameObject.GetComponent<PlayerMovement>().airDrag = 0;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        PlayerColor player = collider.gameObject.GetComponentInParent<PlayerColor>();

        if(player != null) {
            collisionBox.enabled = player.GetColor() == color;          // set platform collision box
            player.transform.parent = null;                             // set player parent to let them stay on platform
            player.gameObject.GetComponent<PlayerMovement>().groundDrag = 6;
            player.gameObject.GetComponent<PlayerMovement>().airDrag = 1;


            //player.gameObject.GetComponent<Rigidbody>().velocity += gameObject.GetComponent<Rigidbody>().velocity;
        }
    }
}
