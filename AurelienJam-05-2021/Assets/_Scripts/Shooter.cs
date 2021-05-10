using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField] private GameObject bullet = null;
    [SerializeField, Range(0f, 1000f)] private float fireRate = 500f;

    private void Awake() {
        this.characterInput = this.transform.parent.parent.GetComponent<CMF.CharacterInput>();
    }

    private void OnDisable() {
        if(this.shootLoop != null) {
            StopCoroutine(this.shootLoop);
        }
    }

    private void OnEnable() {
        this.canShoot = true;
        this.wasShootKeyDown = false;
        HandleShootingInput();
    }

    private void FixedUpdate() {
        HandleShootingInput();
    }

    private void HandleShootingInput() {
        bool isShootKeyDown = this.characterInput.IsShootKeyPressed();
        if(isShootKeyDown) {
            if(!this.wasShootKeyDown) {
                this.shootLoop = StartCoroutine(ShootLoop());
            }
        } else if(this.shootLoop != null) {
            StopCoroutine(this.shootLoop);
        }
        this.wasShootKeyDown = isShootKeyDown;
    }


    private IEnumerator ShootLoop() {
        if(this.canShoot) {
            this.canShoot = false;
            StartCoroutine(ShootCooldown());
        }
        while(true) {
            if(this.canShoot) {
                Shoot();
            }
            yield return new WaitForSecondsRealtime(this.fireRate / 1000f);
        }
    }

    private IEnumerator ShootCooldown() {
        yield return new WaitForSecondsRealtime(this.fireRate / 1000f);
        this.canShoot = true;
    }

    private void Shoot() {
        Instantiate(this.bullet, this.transform.position, this.transform.rotation, null);
    }

    private bool canShoot = true;
    private Coroutine shootLoop = null;
    private bool wasShootKeyDown = false;
    private CMF.CharacterInput characterInput;
}
