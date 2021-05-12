using System.Collections;
using UnityEditor;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField] private GameObject bullet = null;
    [SerializeField, Range(0f, 1000f)] private float fireRate = 500f;

    private void Awake() {
        characterInput = transform.parent.parent.GetComponent<CMF.CharacterInput>();
        character = transform.parent.parent;
        audio = GetComponent<AudioManager>();
    }

    private void OnDisable() {
        if(shootLoop != null) {
            StopCoroutine(shootLoop);
        }
    }

    private void OnEnable() {
        canShoot = true;
        wasShootKeyDown = false;
        HandleShootingInput();
    }

    private void FixedUpdate() {
        HandleShootingInput();
    }

    private void HandleShootingInput() {
        bool isShootKeyDown = characterInput.IsShootKeyPressed();
        if(isShootKeyDown) {
            if(!wasShootKeyDown) {
                shootLoop = StartCoroutine(ShootLoop());
            }
        } else if(shootLoop != null) {
            StopCoroutine(shootLoop);
        }
        wasShootKeyDown = isShootKeyDown;
    }


    private IEnumerator ShootLoop() {
        if(canShoot) {
            canShoot = false;
            StartCoroutine(ShootCooldown());
        }
        while(true) {
            if(canShoot) {
                Shoot();
            }
            yield return new WaitForSecondsRealtime(fireRate / 1000f);
        }
    }

    private IEnumerator ShootCooldown() {
        yield return new WaitForSecondsRealtime(fireRate / 1000f);
        canShoot = true;
    }

    private void Shoot() {
        audio.Play("Pew");
        Instantiate(bullet, transform.position, transform.rotation, null);
    }

    private void OnDrawGizmos() {
        Color old = Handles.color;
        Handles.color = Color.cyan;
        Handles.ArrowHandleCap(0, transform.position, transform.rotation, 2, EventType.Repaint);
        Handles.color = old;
    }

    private new AudioManager audio = null;
    private Transform character = null;
    private bool canShoot = true;
    private Coroutine shootLoop = null;
    private bool wasShootKeyDown = false;
    private CMF.CharacterInput characterInput;
}
