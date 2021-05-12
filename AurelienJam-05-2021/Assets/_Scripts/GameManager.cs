using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public AudioManager AudioManager { get => audioManager; }
    public GameObject Player { get => player; }
    public UIManager UI { get => uI; }

    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private UIManager uI = null;


    private void Awake() {
        if(GameManager.Instance == null) {
            GameManager.Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }
}
