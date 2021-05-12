﻿using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float growTime = .2f;

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
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(scale, growTime);
    }

    private void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
