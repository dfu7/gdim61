using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    [SerializeField]
    private Text distanceText;

    public float distance;
    public Transform player; 

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position;
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
    }
    void DisplayScore(int distance)
    {
        distanceText.text = string.Format("{0} m", distance.ToString());
    }
}
