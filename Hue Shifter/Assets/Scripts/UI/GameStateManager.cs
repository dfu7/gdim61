using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    #region Private Fields

    [SerializeField]
    private List<string> Levels = new List<string>();
   
    [SerializeField]
    private string TitleSceneName;

    [SerializeField]
    private string PauseScreenName;

    [SerializeField]
    private int m_StartingLives;

    private static int m_CurrentLives;

    private static GameStateManager _instance;

    

    #endregion
    // Start is called before the first frame update


    enum GAMESTATE
    { 
        TITLE,
        PLAYING,
        PAUSED,
        GAMEOVER

    }

    private static GAMESTATE m_State;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            m_CurrentLives = _instance.m_StartingLives;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }


  

    // Update is called once per frame
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameStateManager.TogglePause();
        }
    }

    public static void NewGame()
    {
        m_State = GAMESTATE.PLAYING;
        m_CurrentLives = _instance.m_StartingLives;
        if(_instance.Levels.Count > 0)
        {
            SceneManager.LoadScene(_instance.Levels[0]);
        }
    }

    public static void ExitGame()
    {
        m_State = GAMESTATE.TITLE;
        SceneManager.LoadScene(_instance.TitleSceneName);
    }

    public static void GameOver()
    {
        m_State = GAMESTATE.TITLE;
        SceneManager.LoadScene(_instance.TitleSceneName);
    }

    public static void TogglePause()
    {
        if(m_State == GAMESTATE.PLAYING)
        {
            m_State = GAMESTATE.PAUSED;
            SceneManager.LoadScene(_instance.PauseScreenName);
            
        }
        else if(m_State == GAMESTATE.PAUSED)
        {
            m_State = GAMESTATE.PLAYING;
            
        }
    }


    public static void LoseALife()
    {
        m_CurrentLives -= 1;
        if (m_CurrentLives <= 0)
        {
            GameStateManager.GameOver();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
