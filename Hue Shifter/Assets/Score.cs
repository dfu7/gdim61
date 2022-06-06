using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    [SerializeField]
    private Text distanceText;

    public static float distance;
    public Transform player;

    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = player.transform.position;
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.transform.position + ", " + startingPosition);
        distance = player.transform.position.z - startingPosition.z;
        DisplayScore(distance);
    }
    void DisplayScore(float distance)
    {
        //Debug.Log(string.Format("{0} m", distance.ToString()));
        distanceText.text = string.Format("{0} m", (int)distance);
    }
}
