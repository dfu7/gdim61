using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;      // TEMPORARY
using DG.Tweening;

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

      // play player death sounds if we've just died
      if(GameStateManager.justDied) {
         //SoundManager.instance.Play("PlayerDieSFX");
         SoundManager.instance.Play("PlayerDieMusic");
      }
   }

   void Update() {
      if (Input.GetKeyDown(colorSwitch)) {
         color = color == GameColor.RED ? GameColor.BLUE : GameColor.RED;
         foreach(Image x in allToChange)
         {
            if(color == GameColor.RED)
            {
               //x.color = Color.red;
               x.DOColor(Color.red,0.3f);
            }
            else
            {
               x.DOColor(Color.blue,0.3f);

               //x.color = Color.blue;
            }
         }
      }
   }

   public GameColor GetColor() {
      return color;
   }
}