using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PauseScreen : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer;

    [SerializeField]
    private GameObject[] pages;

    private Camera cam;
    [SerializeField] private Transform orientation;

    // public void SetScene(int buildIndex)
    //{
    //  SceneManager.LoadScene(buildIndex);
    //}

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void SetPage(int pageIndex)
    {
        if(pages[pageIndex] !=null)
        {
            for(int i =0; i < pages.Length; ++i)
            {
                pages[i].SetActive(pageIndex == i ? true : false);
            }
        }
    }

    public void SetMasterVolume(float sliderValue)
    {
        _mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSoundVolume(float sliderValue)
    {
        _mixer.SetFloat("SoundVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue)
    {
        _mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

   public void OnClickExitGame()
    {
        GameStateManager.ExitGame();
    }

   
}
