using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
   private string _color;

   [SerializeField] private KeyCode colorSwitch = KeyCode.E;

   void Start() {
      // initial color is set
      // has to match tag, so either 'Red' or 'Blue'
      _color = "Red";
   }

   void Update() {
      if (Input.GetKeyDown(colorSwitch)) {
         _color = _color == "Red" ? "Blue" : "Red";
      }
   }

   public string GetColor() {
      return _color;
   }
}
