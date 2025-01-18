using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;
    public float MinSoundInterval = 0.5f;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

//    void Start ()
//   {
//        Play("Sparks");
//    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        //foreach (AudioSource s in sounds)
        //{
            if (s.source.isPlaying &&
                s.source.time <= MinSoundInterval)
            {
                return;
            }
        //}

        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

}