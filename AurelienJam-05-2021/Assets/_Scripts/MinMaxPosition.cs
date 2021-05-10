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
public class MinMaxPosition : CinemachineExtension {
    [SerializeField] private Vector3 minPos, maxPos;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
        if(stage == CinemachineCore.Stage.Body) {
            Vector3 pos = state.RawPosition;
            //if(pos) {

            //}
            state.RawPosition = pos;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MinMaxPosition))]
public class MinMaxEditor : Editor {

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        if(GUILayout.Button(this.useTransforms ? "Use Transforms" : "Use Vector3")) {
            if(FindMinMax()) {
                this.useTransforms = !this.useTransforms;
            }
        }
        if(this.useTransforms) {
            EditorGUILayout.BeginHorizontal();
            this.minTPos = EditorGUILayout.ObjectField(this.minTPos, typeof(Transform), true) as Transform;
            this.maxTPos = EditorGUILayout.ObjectField(this.maxTPos, typeof(Transform), true) as Transform;
            EditorGUILayout.EndHorizontal();
            this.minPos.vector3Value = this.minTPos.position;
            this.maxPos.vector3Value = this.maxTPos.position;
        } else {
            EditorGUILayout.PropertyField(this.minPos);
            EditorGUILayout.PropertyField(this.maxPos);
        }
        this.serializedObject.ApplyModifiedProperties();
    }

    private bool FindMinMax() {
        MinMaxPosition inspectedObject = this.target as MinMaxPosition;
        Debug.Log(inspectedObject);
        Transform _min;
        Transform _max;
        if(inspectedObject.transform.childCount == 1) {
            GameObject go = Instantiate(new GameObject("min"), inspectedObject.transform);
            _min = go.transform;
            go = Instantiate(new GameObject("max"), inspectedObject.transform);
            _max = go.transform;

        } else {
            _min = inspectedObject.transform.GetChild(0);
            _max = inspectedObject.transform.GetChild(1);
        }
        this.minTPos = _min;
        this.maxTPos = _max;
        return this.maxTPos != null && this.minTPos != null;
    }

    private void OnSceneGUI() {
        this.minPos = this.serializedObject.FindProperty("minPos");
        this.maxPos = this.serializedObject.FindProperty("maxPos");

        if(this.minTPos != null && this.maxTPos != null) {
            if(this.useTransforms) {
                this.minPos.vector3Value = this.minTPos.position;
            }
        }
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


    private bool useTransforms = false;
    private Transform minTPos, maxTPos;
    private SerializedProperty minPos, maxPos;
}
#endif