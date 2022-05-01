using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed = 10;
    private GameColor color;
    [SerializeField] private GameObject collisionBox;
    [SerializeField] private Material[] colors;

    void Start() {
       SetColor(Random.Range(0, 1.0f) > 0.5 ? GameColor.RED : GameColor.BLUE);
    }

    // Update is called once per frame
    void Update()
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

    void SetColor(GameColor _color) {
       color = _color;
       GetComponent<MeshRenderer>().material = colors[(int)_color];
    }
}
