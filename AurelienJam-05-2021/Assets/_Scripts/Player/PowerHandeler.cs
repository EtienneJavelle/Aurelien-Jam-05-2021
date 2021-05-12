using System.Collections;
using UnityEngine;

public class PowerHandeler : MonoBehaviour {
    [SerializeField] private KeyCode power1 = KeyCode.Alpha1, power2 = KeyCode.Alpha2, power3 = KeyCode.Alpha3;
    [SerializeField] private int power = 1;
    [SerializeField] private float coolDown = 200f;
    [SerializeField] private float amountGained = 2f;
    [Header("Power 1")]
    [SerializeField] private float p1Cost = 10;
    [SerializeField] private float p1Firerate = 60f;
    [SerializeField] private GameObject p1Object = null;
    [Header("Power 2")]
    [SerializeField] private float p2Cost = 30;
    [SerializeField] private float p2Firerate = 500f;
    [SerializeField] private GameObject p2Object = null;
    [Header("Power 3")]
    [SerializeField] private float p3Cost = 60;
    [SerializeField] private float p3Firerate = 800f;
    [SerializeField] private GameObject p3Object = null;
    [SerializeField] private int shooterAmount = 9;
    [SerializeField] private float angle = 360f;

    private void Awake() {
        shooterHandeler = GetComponentInChildren<ShooterHandeler>();
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

    private void SetPower1() {
        power = 1;
        shooterHandeler.SpawnShooters(1);
        shooterHandeler.MainShooter.SetSpecial(p1Firerate, p1Object);
    }

    private void SetPower2() {
        power = 2;
        shooterHandeler.SpawnShooters(1);
        shooterHandeler.MainShooter.SetSpecial(p2Firerate, p2Object);
    }

    private void SetPower3() {
        power = 3;
        shooterHandeler.SpawnShooters(shooterAmount, angle, p3Firerate, p3Object);
        shooterHandeler.MainShooter.SetSpecial(p3Firerate, p3Object);
    }

    private float currentP1 = 100f, currentP2 = 100f, currentP3 = 100f;
    private ShooterHandeler shooterHandeler = null;

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
}
