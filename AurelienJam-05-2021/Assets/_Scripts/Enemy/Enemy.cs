using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] private LayerMask layers = 1024;
    [SerializeField] private float backSpeed = 6f, forwardSpeed = 10f;
    [SerializeField] private float shootRangeMin = 10f, shootRangeMax = 15f;
    [SerializeField, Range(0f, 1f)] private float turnSpeed = .5f;



    private void Awake() {
        shooter = GetComponentInChildren<Shooter>();
        shooter.enabled = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start() {
        playerTransform = GameManager.Instance.Player.transform;
        animator.SetBool("IsGrounded", true);
        animator.SetBool("IsStrafing", true);
    }
    public void Die() {
        GameManager.Instance.UI.AddScore(2);
        GameObject.Destroy(gameObject);


    }

    private void Update() {
        Vector3 direction = transform.position - playerTransform.position;
        Vector3 targetPos = transform.position;
        direction.Normalize();


        if(Vector3.Distance(playerTransform.position, transform.position) < shootRangeMin) {
            targetPos = playerTransform.position + direction * shootRangeMin;
            agent.speed = backSpeed;
            animator.SetFloat("HorizontalSpeed", 1f);
            animator.SetFloat("ForwardSpeed", -10f);
        } else if(Vector3.Distance(playerTransform.position, transform.position) <= shootRangeMax) {
            Ray ray = new Ray(transform.position + Vector3.up, -direction);
            if(Physics.Raycast(ray, out RaycastHit hit, shootRangeMax, layers)
                && hit.collider.gameObject == playerTransform.gameObject) {
                Debug.DrawLine(transform.position + Vector3.up, hit.point, Color.blue);
                shooter.enabled = true;
                animator.SetFloat("HorizontalSpeed", 0f);
            } else {
                targetPos += Vector3.left + transform.rotation.eulerAngles;
                shooter.enabled = false;
            }
        } else {
            targetPos = playerTransform.position + direction * shootRangeMax;
            agent.speed = forwardSpeed;
            shooter.enabled = false;
            animator.SetFloat("HorizontalSpeed", 1f);
            animator.SetFloat("ForwardSpeed", 10f);
        }
        agent.destination = targetPos;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(-direction, transform.up),
            turnSpeed);
    }

    private void OnDrawGizmos() {
        if(Application.isPlaying) {
            Gizmos.DrawWireSphere(agent.destination, 1);
        }
    }

    private Animator animator = null;
    private Transform playerTransform = null;
    private NavMeshAgent agent = null;
    private Shooter shooter = null;
}
