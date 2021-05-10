using UnityEngine;

public class TurnTowardAimPosition : MonoBehaviour {

    [SerializeField, Range(0f, 1f)] private float turnSpeed = .5f;

    private void LateUpdate() {
        Plane plane = new Plane(Vector3.up, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPosition = Vector3.zero;
        float distance;
        if(plane.Raycast(ray, out distance)) {
            worldPosition = ray.GetPoint(distance);
            Debug.DrawLine(this.transform.position, worldPosition);
        }
        worldPosition.y = this.transform.position.y;
        Vector3 direction = worldPosition - this.transform.position;
        direction.Normalize();
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction, this.transform.up), this.turnSpeed);
        //this.transform.rotation = Quaternion.LookRotation(direction, this.transform.up);
        Debug.DrawRay(this.transform.position, direction, Color.red);
    }
}
