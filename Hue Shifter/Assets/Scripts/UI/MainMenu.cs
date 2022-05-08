using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private AudioMixer _mixer;
   [SerializeField] private GameObject[] pages;

    private Camera cam;
    [SerializeField] private Transform orientation;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /* Change scene based on build index. Use for UI buttons. */
    public void SetScene(int buildIndex) {
      SceneManager.LoadScene(buildIndex);
   }

   /* load a 'page.' page index can be found in the name of the page gameobject */
   public void SetPage(int pageIndex) {
      if(pages[pageIndex] != null) {
         for(int i = 0; i < pages.Length; i++) {
            pages[i].SetActive(pageIndex == i ? true : false);
         }
      }
   }
   
   /* quits application */
   public void QuitGame() {
      Application.Quit(0);
   }

   /* The following 3 are for volume sliders, linked to the master _mixer */
   public void SetMasterVolume(float sliderValue) {
      _mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
   }

   public void SetSoundVolume(float sliderValue) {
      _mixer.SetFloat("SoundVolume", Mathf.Log10(sliderValue) * 20);
   }

   public void SetMusicVolume(float sliderValue) {
      _mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
   }
}
