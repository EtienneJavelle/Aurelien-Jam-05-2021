using System.Collections;
using UnityEngine;

public class PowerHandeler : MonoBehaviour {
    public int Power { get => power; }
    public bool CanP1 { get => canP1; }
    public bool CanP2 { get => canP2; }
    public bool CanP3 { get => canP3; }

    [SerializeField] private KeyCode power1 = KeyCode.Alpha1, power2 = KeyCode.Alpha2, power3 = KeyCode.Alpha3;
    [SerializeField] private int power = 1;
    [SerializeField] private float coolDown = 200f;
    [SerializeField] private float amountGained = 2f;
    [Header("Power 1")]
    [SerializeField] private float p1Cost = 10;
    [SerializeField] private float p1Firerate = 60f;
    [SerializeField] private GameObject p1Object = null, p1Character = null;
    [SerializeField] private Avatar p1Avatar = null;
    [Header("Power 2")]
    [SerializeField] private float p2Cost = 30;
    [SerializeField] private float p2Firerate = 500f;
    [SerializeField] private GameObject p2Object = null, p2Character = null;
    [SerializeField] private Avatar p2Avatar = null;
    [Header("Power 3")]
    [SerializeField] private float p3Cost = 60;
    [SerializeField] private float p3Firerate = 800f;
    [SerializeField] private GameObject p3Object = null, p3Character = null;
    [SerializeField] private Avatar p3Avatar = null;
    [SerializeField] private int shooterAmount = 9;
    [SerializeField] private float angle = 360f;

    private void Awake() {
        shooterHandeler = GetComponentInChildren<ShooterHandeler>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        SetPower1();
        StartCoroutine(RegainMana());
    }

    private void Update() {
        if(Input.GetKeyDown(power1)) {
            if(power == 1) {
                return;
            }
            SetPower1();
        }
        if(Input.GetKeyDown(power2)) {
            if(power == 2) {
                return;
            }

            SetPower2();
        }
        if(Input.GetKeyDown(power3)) {
            if(power == 3) {
                return;
            }
            SetPower3();
        }


    }

    private IEnumerator RegainMana() {
        while(true) {
            yield return new WaitForSecondsRealtime(coolDown / 1000f);
            currentP1 = Mathf.Min(100, currentP1 + amountGained);
            GameManager.Instance.UI.P1Slider.value = currentP1;
            currentP2 = Mathf.Min(100, currentP2 + amountGained);
            GameManager.Instance.UI.P2Slider.value = currentP2;
            currentP3 = Mathf.Min(100, currentP3 + amountGained);
            GameManager.Instance.UI.P3Slider.value = currentP3;
        }
    }

    private bool SetPower1() {
        if(!CanP1) {
            return false;
        }
        power = 1;
        shooterHandeler.SpawnShooters(1);
        shooterHandeler.MainShooter.SetSpecial(p1Firerate, p1Object);
        animator.avatar = p1Avatar;
        p1Character.SetActive(true);
        p2Character.SetActive(false);
        p3Character.SetActive(false);
        return true;
    }

    private bool SetPower2() {
        if(!CanP2) {
            return false;
        }
        power = 2;
        shooterHandeler.SpawnShooters(1);
        shooterHandeler.MainShooter.SetSpecial(p2Firerate, p2Object);
        animator.avatar = p2Avatar;
        p1Character.SetActive(false);
        p2Character.SetActive(true);
        p3Character.SetActive(false);
        return true;
    }

    private bool SetPower3() {
        if(!CanP3) {
            return false;
        }
        power = 3;
        shooterHandeler.SpawnShooters(shooterAmount, angle, p3Firerate, p3Object);
        shooterHandeler.MainShooter.SetSpecial(p3Firerate, p3Object);
        animator.avatar = p3Avatar;
        p1Character.SetActive(false);
        p2Character.SetActive(false);
        p3Character.SetActive(true);
        return true;
    }

    public bool Shoot(AudioManager audio) {
        switch(power) {
            case 1:
                if(currentP1 - p1Cost <= 0) {
                    return false;
                }
                currentP1 -= p1Cost;
                GameManager.Instance.UI.P1Slider.value = currentP1;
                return true;
            case 2:
                if(currentP2 - p1Cost <= 0) {
                    return false;
                }
                currentP2 -= p2Cost;
                GameManager.Instance.UI.P2Slider.value = currentP2;
                return true;
            case 3:
                if(currentP3 - p3Cost <= 0) {
                    return false;
                }
                currentP3 -= p3Cost;
                GameManager.Instance.UI.P3Slider.value = currentP3;
                return true;
            default:
                return false;
        }
    }

    public bool LooseLife(int power, Sprite deadSprite) {
        switch(power) {
            case 1:
                canP1 = false;
                GameManager.Instance.UI.P1Image.sprite = deadSprite;
                if(!SetPower2()) {
                    if(!SetPower3()) {
                        return false;
                    }
                }
                return true;
            case 2:
                canP2 = false;
                GameManager.Instance.UI.P2Image.sprite = deadSprite;
                if(!SetPower3()) {
                    if(!SetPower1()) {
                        return false;
                    }
                }
                return true;
            case 3:
                canP3 = false;
                GameManager.Instance.UI.P3Image.sprite = deadSprite;
                if(!SetPower1()) {
                    if(!SetPower2()) {
                        return false;
                    }
                }
                return true;
            default:
                return false;
        }
    }

    private bool canP1 = true, canP2 = true, canP3 = true;
    private float currentP1 = 100f, currentP2 = 100f, currentP3 = 100f;
    private ShooterHandeler shooterHandeler = null;
    private Animator animator = null;

}
