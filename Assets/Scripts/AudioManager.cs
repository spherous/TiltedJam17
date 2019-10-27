using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    public static AudioManager instance;
    public GameObject player;

	// Use this for initialization

    void Update()
    {
        if(player != null)
            transform.position = player.transform.position;
    }

	void Awake () {
        if (instance == null)
            instance = this;
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
	}

    void Start()
    {
        if (!IsPlaying("Cemetery"))
        {
            Play("Cemetery");
        }

        if(!IsPlaying("Ghost Ship"))
        {
            Play("Ghost Ship");
        }

        player = GameManager.Instance.player.gameObject;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;

        }
             
        s.source.Play();
        Debug.Log("Playing " + name);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;

        }
             
        s.source.Stop();

    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return false;
        }
        return s.source.isPlaying;
    }
}