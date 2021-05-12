using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Slider P1Slider { get => p1Slider; }
    public Slider P2Slider { get => p2Slider; }
    public Slider P3Slider { get => p3Slider; }

    [SerializeField] private Slider p1Slider = null, p2Slider = null, p3Slider = null;

}
