using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private float backSpeed = 6f, forwardSpeed = 10f;
    [SerializeField] private float shootRangeMin = 10f, shootRangeMax = 15f;
    [SerializeField, Range(0f, 1f)] private float turnSpeed = .5f;

    private void Awake() {
        this.shooter = GetComponentInChildren<Shooter>();
        this.shooter.enabled = false;
    }

    public void Die() {
        GameObject.Destroy(this.gameObject);
    }

    private void Update() {

        Vector3 targetPos = GameManager.Instance.Player.transform.position;

        Vector3 direction = targetPos - this.transform.position;
        direction.Normalize();
        this.transform.rotation = Quaternion.Slerp(
            this.transform.rotation,
            Quaternion.LookRotation(direction, this.transform.up),
            this.turnSpeed);

        if(Vector3.Distance(targetPos, this.transform.position) < this.shootRangeMin) {
            this.transform.position -= direction.normalized * this.backSpeed * Time.deltaTime;
        } else if(Vector3.Distance(targetPos, this.transform.position) < this.shootRangeMax) {
            this.shooter.enabled = true;
        } else {
            this.transform.position += direction.normalized * this.forwardSpeed * Time.deltaTime;
            this.shooter.enabled = false;
        }
    }

    private Shooter shooter = null;
}
