using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed = 10;
    private string _color;
    [SerializeField] private GameObject collisionBox;
    [SerializeField] private Material[] colors;

    void Start() {
       SetColor(Random.Range(0, 1.0f) > 0.5 ? "Red" : "Blue");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += -transform.forward * speed * Time.deltaTime;

        if (transform.position.z <= -20)
        {
            Destroy(gameObject);
        }
    }

<<<<<<< Updated upstream
    // only enable hitbox when player is of the right color
    void OnTriggerStay(Collider collision) {
       PlayerColor player = collision.gameObject.GetComponentInParent<PlayerColor>();
       if(player != null) {
          collisionBox.SetActive(player.GetColor() == _color);
       }
    }

    void SetColor(string color) {
       GetComponent<MeshRenderer>().material = color == "Red" ? colors[0] : colors[1];
       _color = color;
=======
    //Code for allowing player to ride on top of the platform
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null; 
>>>>>>> Stashed changes
    }
}
