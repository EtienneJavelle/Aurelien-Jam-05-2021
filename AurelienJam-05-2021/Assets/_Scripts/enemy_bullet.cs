using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifeTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        GameObject.Destroy(this.gameObject, this.lifeTime);
    }

    private void Update()
    {
        this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
    }
}