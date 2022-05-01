using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameColor {
   RED = 0,
   BLUE,
}

public class PlayerColor : MonoBehaviour
{
   private GameColor color;

   [SerializeField] private KeyCode colorSwitch = KeyCode.E;

   void Start() {
      // initial color is set
      // has to match tag, so either 'Red' or 'Blue'
      color = GameColor.RED;
   }

   void Update() {
      if (Input.GetKeyDown(colorSwitch)) {
         color = color == GameColor.RED ? GameColor.BLUE : GameColor.RED;
      }
   }

   public GameColor GetColor() {
      return color;
   }
}