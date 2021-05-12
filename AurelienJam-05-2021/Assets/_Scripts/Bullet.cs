using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifeTime = 5f;

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Enemy>(out Enemy enemy)) {
            enemy.Die();
        } else if(other.TryGetComponent<DeathHandeler>(out DeathHandeler player)) {
            player.enabled = true;
        }
        GameObject.Destroy(gameObject);
    }

    private void OnEnable() {
        GameObject.Destroy(gameObject, lifeTime);
    }

    private void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
