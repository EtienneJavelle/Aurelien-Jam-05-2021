using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public AudioManager AudioManager { get => this.audioManager; }

    [SerializeField] private AudioManager audioManager = null;


    private void Awake() {
        if(GameManager.Instance == null) {
            GameManager.Instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
        this.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
    }
}
