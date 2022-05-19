using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftScreen : MonoBehaviour
{
    public float HorizontalSpeed = 1;
    public float MaxHorizontalPosition = 1000;
    public float MinHorizontalPosition = -200;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 position = rectTransform.anchoredPosition;

        position.x += HorizontalSpeed * Time.deltaTime;

        if (position.x > MaxHorizontalPosition)
            position.x = MinHorizontalPosition;

        rectTransform.anchoredPosition = position;
    }

    /* public Transform startMarker;
     public Transform endMarker;

     private bool Shifted = false; 

     // Movement speed in units per second.
     public float speed = 1.0F;

     // Time when the movement started.
     private float startTime;

     // Total distance between the markers.
     private float journeyLength;

     void Start()
     {
         // Keep a note of the time the movement started.
         startTime = Time.time;

         // Calculate the journey length.
         journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
     }


     // Move to the target end position.
     void Update()
     {
         // Distance moved equals elapsed time times speed..
         float distCovered = (Time.time - startTime) * speed;

         // Fraction of journey completed equals current distance divided by total distance.
         float fractionOfJourney = distCovered / journeyLength;

         if (Input.anyKeyDown == true && Shifted == false)
         {
           // Set our position as a fraction of the distance between the markers.
           transform.position = Vector3.Lerp(startMarker.position, startMarker.position*2 , fractionOfJourney);
           Shifted = true;
           Debug.Log("Shifted");
         }

     }*/

}
