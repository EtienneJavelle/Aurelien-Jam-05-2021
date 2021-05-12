using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Slider P1Slider { get => p1Slider; }
    public Slider P2Slider { get => p2Slider; }
    public Slider P3Slider { get => p3Slider; }
    public Image P1Image { get => p1Image; }
    public Image P2Image { get => p2Image; }
    public Image P3Image { get => p3Image; }
    public GameObject LooseGo { get => looseGo; }

    [SerializeField] private Image p1Image = null, p2Image = null, p3Image = null;
    [SerializeField] private Slider p1Slider = null, p2Slider = null, p3Slider = null;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject looseGo = null;


    public int AddScore(int value) {
        score += value;
        scoreText.text = score.ToString();
        return score;
    }

    private int score = 0;
}
