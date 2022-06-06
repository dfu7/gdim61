using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public static float highScore = 0;

    [SerializeField] Text highScoreText;

    private void Update()
    {
        highScoreText.text = string.Format("{0} m", (int)highScore);
    }
}
