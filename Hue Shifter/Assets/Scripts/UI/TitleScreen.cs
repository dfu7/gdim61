using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TitleScreen : MonoBehaviour
{
   public void OnClickNewGame()
    {
        GameStateManager.NewGame();
    }

    public void OnClickQuitGame()
    {
        Application.Quit();
    }
}
