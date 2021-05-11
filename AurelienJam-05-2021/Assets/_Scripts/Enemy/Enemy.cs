using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private float backSpeed = 6f, forwardSpeed = 10f;
    [SerializeField] private float shootRangeMin = 10f, shootRangeMax = 15f;
    [SerializeField, Range(0f, 1f)] private float turnSpeed = .5f;

    private void Awake() {
        shooter = GetComponentInChildren<Shooter>();
        shooter.enabled = false;
        //rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    public void Die() {
        GameObject.Destroy(gameObject);
    }

    private void FixedUpdate() {

        Vector3 targetPos = GameManager.Instance.Player.transform.position;

        Vector3 direction = targetPos - transform.position;
        direction.Normalize();

        if(Vector3.Distance(targetPos, transform.position) < shootRangeMin) {
            controller.SimpleMove(-direction.normalized * backSpeed * Time.deltaTime);
            //rigidbody.MovePosition(transform.position - (direction.normalized * backSpeed * Time.deltaTime));
            //transform.position -= direction.normalized * backSpeed * Time.deltaTime;
        } else if(Vector3.Distance(targetPos, transform.position) < shootRangeMax) {
            shooter.enabled = true;
        } else {
            controller.SimpleMove(direction.normalized * forwardSpeed * Time.deltaTime);
            //rigidbody.MovePosition(transform.position + (direction.normalized * forwardSpeed * Time.deltaTime));
            //transform.position += direction.normalized * forwardSpeed * Time.deltaTime;
            shooter.enabled = false;
        }
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(direction, transform.up),
            turnSpeed);
    }

    private new Rigidbody rigidbody = null;
    private CharacterController controller = null;
    private Shooter shooter = null;
}
