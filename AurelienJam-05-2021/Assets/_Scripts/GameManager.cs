using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public AudioManager AudioManager { get => this.audioManager; }
    public GameObject Player { get => this.player; }

    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private GameObject player = null;


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
