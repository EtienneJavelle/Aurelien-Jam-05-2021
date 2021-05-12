using System.Collections;
using UnityEditor;
using UnityEngine;

public class ShooterHandeler : MonoBehaviour {
    public Shooter MainShooter { get => mainShooter; }

    [SerializeField] private GameObject shooterRoot = null;
    [SerializeField, Min(1)] private int shooterAmount = 1;
    [SerializeField, Range(0f, 360f)] private float angle = 90f;

    private void Awake() {
        mainShooter = transform.GetChild(0).GetComponentInChildren<Shooter>();
        SpawnShooters();
    }

    public void SpawnShooters(int amount, float angle = 360, float firerate = 0, GameObject go = null) {
        shooterAmount = amount;
        this.angle = angle;
        SpawnShooters(firerate, go);
    }

    [ContextMenu("Get Shooters")]
    private void GetShooters() {
        shooters = GetComponentsInChildren<Shooter>();
        StartCoroutine(WaitGetShooters());
    }

    private IEnumerator WaitGetShooters() {
        yield return new WaitForEndOfFrame();
        GetShooters();
    }

    [ContextMenu("Spawn Shooters")]
    private void SpawnShooters(float firerate = 0, GameObject go = null) {
        GetShooters();
        if(shooters != null) {

            for(int i = shooterAmount; i < shooters.Length; i++) {
                if(Application.isPlaying) {
                    GameObject.Destroy(shooters[i].transform.parent.gameObject);
                } else {
                    GameObject.DestroyImmediate(shooters[i].transform.parent.gameObject);
                }
            }
            GetShooters();
        }

        if(shooterAmount == 1) {
            return;
        }

        for(int i = 1; i < shooterAmount; i += 2) {
            float angle = this.angle - this.angle / 2;
            Quaternion rotation = Quaternion.AngleAxis((angle / shooterAmount) * i, Vector3.up);
            if(shooters != null && i < shooters.Length) {
                shooters[i].transform.parent.rotation = rotation;
            } else {
                Shooter shooter = Instantiate(shooterRoot, transform.position + Vector3.up, rotation, transform).GetComponentInChildren<Shooter>();
                shooter.SetSpecial(firerate, go);
            }

            rotation = Quaternion.AngleAxis((-angle / shooterAmount) * i, Vector3.up);
            if(shooters != null && i + 1 < shooters.Length) {
                shooters[i + 1].transform.parent.rotation = rotation;
            } else {
                Shooter shooter = Instantiate(shooterRoot, transform.position + Vector3.up, rotation, transform).GetComponentInChildren<Shooter>();
                shooter.SetSpecial(firerate, go);
            }
        }
        GetShooters();
    }






#if  UNITY_EDITOR
    private void OnDrawGizmos() {
        for(int i = 1; i < shooterAmount; i += 2) {
            float angle = this.angle - this.angle / 2;
            Quaternion rotation = Quaternion.AngleAxis((angle / shooterAmount) * i, Vector3.up);
            Handles.ArrowHandleCap(0, transform.position + Vector3.up, rotation, 1f, EventType.Repaint);
            rotation = Quaternion.AngleAxis((-angle / shooterAmount) * i, Vector3.up);
            Handles.ArrowHandleCap(0, transform.position + Vector3.up, rotation, 1f, EventType.Repaint);
        }
    }
#endif

    private Shooter[] shooters = null;
    private Shooter mainShooter = null;
}
