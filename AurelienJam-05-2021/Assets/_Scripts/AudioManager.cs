using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }

    [SerializeField] private Sound[] sounds = new Sound[0];

    private void Awake() {
        if(AudioManager.Instance == null) {
            AudioManager.Instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
        this.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);

        foreach(Sound sound in this.sounds) {
            sound.Source = this.gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }


    public void Play(string name) {
        Sound s = Array.Find(this.sounds, sound => sound.Name == name);
        if(s == null) {
            return;
        }
        s.Source.Play();
    }
    public void Stop(string name) {
        Sound s = Array.Find(this.sounds, sound => sound.Name == name);
        if(s == null) {
            return;
        }
        s.Source.Stop();
    }

}
