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
        GameObject.Destroy(this.gameObject);
    }

    private void OnEnable() {
        GameObject.Destroy(this.gameObject, this.lifeTime);
    }

    private void Update() {
        this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
    }
}
