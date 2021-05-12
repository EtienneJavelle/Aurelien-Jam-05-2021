using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private GameObject explosion = null;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float growTime = .2f;
    [SerializeField] private GameObject Vfx_Explosion;

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Enemy>(out Enemy enemy)) {
            enemy.Die();
        } else if(other.TryGetComponent<DeathHandeler>(out DeathHandeler player)) {
            player.enabled = true;
        }
        if(Vfx_Explosion != null) {
            GameObject.Instantiate(Vfx_Explosion, transform.position, Quaternion.identity);
        }
        if(explosion != null) {
            GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
        }
        GameObject.Destroy(gameObject);
    }

    private void OnEnable() {
        GameObject.Destroy(gameObject, lifeTime);
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(scale, growTime).OnComplete(() => transform.DOScale(0, lifeTime - growTime).SetEase(Ease.InExpo));
    }

    private void OnDestroy() {
        transform.DOKill();
    }

    private void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


}
