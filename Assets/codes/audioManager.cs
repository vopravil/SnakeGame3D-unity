using UnityEngine;
using System;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    public Sound[] sounds; // Array of sounds.

    void Awake()
    {
        // Add an AudioSource component for each sound.
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Method to play a sound by name.
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
[System.Serializable] // This attribute makes it show up in the Inspector.
public class Sound
{
    public string name; // Name of the sound.
    public AudioClip clip; // The audio file.

    [Range(0f, 1f)]
    public float volume = 0.5f; // Volume level.

    [Range(0.1f, 3f)]
    public float pitch = 1f; // Pitch level.

    public bool loop; // Whether the sound should loop.

    [HideInInspector] // This is assigned dynamically, so hide it in the Inspector.
    public AudioSource source;
}
