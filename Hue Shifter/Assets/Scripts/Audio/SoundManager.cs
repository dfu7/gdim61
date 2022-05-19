using System.Collections;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] sounds;

    void Awake() {
        if(instance == null) {
            instance = this;

            foreach(Sound s in sounds) {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.playOnAwake = false;
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }

        } else {
            Debug.Log("Two instances of SoundManager exist, keeping oldest...");
            Destroy(gameObject);
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, s => s.name == name);
        if(s == null) {
            Debug.LogError("Attempted to play sound '" + name + "' which doesn't exist.");
            return;
        }
        s.source.Play();
    }
}
