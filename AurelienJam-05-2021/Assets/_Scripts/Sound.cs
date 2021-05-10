using UnityEngine;

[System.Serializable]
public class Sound {
    public string Name { get => this.name; set => this.name = value; }
    public AudioClip Clip { get => this.clip; set => this.clip = value; }
    public float Volume { get => this.volume; set => this.volume = value; }
    public float Pitch { get => this.pitch; set => this.pitch = value; }
    public bool Loop { get => this.loop; set => this.loop = value; }
    public AudioSource Source { get => this.source; set => this.source = value; }

    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;
    [SerializeField, Range(0, 1)] private float volume = 1;
    [SerializeField, Range(.1f, 3)] private float pitch = 1;
    [SerializeField] private bool loop;

    private AudioSource source;
}
