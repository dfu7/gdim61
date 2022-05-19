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

   public Text colorText;     // TEMPORARY

   [SerializeField] private KeyCode colorSwitch = KeyCode.E;

   void Start() {
      // initial color is set
      // has to match tag, so either 'Red' or 'Blue'
      color = GameColor.RED;
   }

   void Update() {
      if (Input.GetKeyDown(colorSwitch)) {
         color = color == GameColor.RED ? GameColor.BLUE : GameColor.RED;
         colorText.text = color == GameColor.RED ? "Red" : "Blue";       // TEMPORARY
      }
   }

   public GameColor GetColor() {
      return color;
   }
}