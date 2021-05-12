using UnityEngine;

public class PowerHandeler : MonoBehaviour {
    [SerializeField] private KeyCode power1 = KeyCode.Alpha1, power2 = KeyCode.Alpha2, power3 = KeyCode.Alpha3;
    [SerializeField] private int power = 1;
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

    public void Shoot(AudioManager audio) {
        switch(power) {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
}
