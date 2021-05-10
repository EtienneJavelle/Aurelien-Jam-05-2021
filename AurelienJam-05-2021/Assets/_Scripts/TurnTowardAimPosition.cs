using UnityEngine;

public class TurnTowardAimPosition : MonoBehaviour {

    [SerializeField] private float turnSpeed = 500f;

    private void LateUpdate() {

        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                            mouse.x,
                                                            mouse.y,
                                                            this.transform.position.y));
        Vector3 forward = mouseWorld - this.transform.position;
        //this.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        this.transform.LookAt(mouse);
    }
}
