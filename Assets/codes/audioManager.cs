using UnityEngine;
using System;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    public Sound[] sounds;
    void Awake()
    {
               foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

       public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        s.source.Play();
    }
}

// Move the Sound class outside of the audioManager class
[System.Serializable]public class Sound
{
    public string name;    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop;
    [HideInInspector]    public AudioSource source;
}
