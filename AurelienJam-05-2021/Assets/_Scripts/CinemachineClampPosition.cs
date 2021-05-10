using Cinemachine;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's chosen Axis co-ordinate
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CinemachineClampPosition : CinemachineExtension {
    [SerializeField] private Vector3 minPos, maxPos;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
        if(stage == CinemachineCore.Stage.Body) {
            Vector3 pos = state.RawPosition;
            pos.x = Mathf.Clamp(pos.x, this.minPos.x, this.maxPos.x);
            pos.y = Mathf.Clamp(pos.y, this.minPos.y, this.maxPos.y);
            pos.z = Mathf.Clamp(pos.z, this.minPos.z, this.maxPos.z);

            state.RawPosition = pos;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CinemachineClampPosition))]
public class MinMaxEditor : Editor {


    private void OnSceneGUI() {
        this.minPos = this.serializedObject.FindProperty("minPos");
        this.maxPos = this.serializedObject.FindProperty("maxPos");

        Vector3 min = this.minPos.vector3Value, max = this.maxPos.vector3Value;
        Vector3[] points = new Vector3[] {
            min,
            new Vector3(min.x,min.y,max.z),
            new Vector3(min.x,max.y,max.z),
            new Vector3(min.x,max.y,min.z),
            min,
            new Vector3(max.x,min.y,min.z),
            new Vector3(max.x,max.y,min.z),
            new Vector3(min.x,max.y,min.z),
            min,
            new Vector3(max.x,min.y,min.z),
            new Vector3(max.x,min.y,max.z),
            max,
            new Vector3(min.x,max.y,max.z),
            max,
            new Vector3(max.x,max.y,min.z)
        };
        Handles.DrawAAPolyLine(points);
    }
    private SerializedProperty minPos, maxPos;
}
#endif