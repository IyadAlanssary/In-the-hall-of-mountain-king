using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    public static Sound music;
    public static bool audioPaused = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        music = Array.Find(sounds, sound => sound.name == "in the hall");
        ToggleSfxVolume();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !PauseMenu.isPaused)
        {
            if (music.source.pitch == 1)
                music.source.pitch = 2;
            else
                music.source.pitch = 1;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " does not exist");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " does not exist");
            return;
        }
        s.source.Stop();
    }
    
    // public void SetMusicVolume(float v)
    // {
    //     foreach (Sound s in sounds)
    //     {
    //         if (!s.isSfx)
    //             s.source.volume = s.volume * v;
    //     }
    // }
    public void ToggleSfxVolume()
    {
        foreach (Sound s in sounds)
        {
            if (s.isSfx)
                if(s.source.volume == 0)
                    s.source.volume = s.volume;
                else
                    s.source.volume = 0;
        }
    }
}
