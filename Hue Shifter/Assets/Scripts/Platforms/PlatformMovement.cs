using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed = 10;
    private GameColor color;
    [SerializeField] private GameObject collisionBox;
    [SerializeField] private Material[] colors;

    void Start()
    {
       SetColor(Random.Range(0, 1.0f) > 0.5 ? GameColor.RED : GameColor.BLUE);
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

    // only enable hitbox when player is of the right color
    void OnTriggerStay(Collider collision) {
       PlayerColor player = collision.gameObject.GetComponentInParent<PlayerColor>();
       if(player != null) {
          collisionBox.SetActive(player.GetColor() == color);
       }
    }

    void SetColor(GameColor _color)
    {
        color = _color;
        GetComponent<MeshRenderer>().material = colors[(int)_color];
    }

    //Code for allowing player to ride on top of the platform
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null; 

    }
}
