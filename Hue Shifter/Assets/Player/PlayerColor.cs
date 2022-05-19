using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;      // TEMPORARY

public enum GameColor {
   RED = 0,
   BLUE,
}

public class PlayerColor : MonoBehaviour
{
   private GameColor color;

   [SerializeField] private KeyCode colorSwitch = KeyCode.E;
   [SerializeField] Image[] allToChange = new Image[0];

   void Start() {
      // initial color is set
      // has to match tag, so either 'Red' or 'Blue'
      color = GameColor.RED;

      SoundManager.instance.Play("Theme");
   }

   void Update() {
      if (Input.GetKeyDown(colorSwitch)) {
         color = color == GameColor.RED ? GameColor.BLUE : GameColor.RED;
         foreach(Image x in allToChange)
         {
            if(color == GameColor.RED)
            {
               x.color = Color.red;
            }
            else
            {
               x.color = Color.blue;
            }
         }
      }
   }

   public GameColor GetColor() {
      return color;
   }
}