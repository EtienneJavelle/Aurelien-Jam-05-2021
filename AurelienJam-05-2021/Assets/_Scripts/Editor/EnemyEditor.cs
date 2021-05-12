using UnityEditor;

[UnityEditor.CustomEditor(typeof(Enemy))]
public class EnemyEditor : Etienne.Editor<Enemy> {

    private void OnEnable() {
        targetTransform = Target.gameObject.transform;
        minRange = serializedObject.FindProperty("shootRangeMin");
        maxRange = serializedObject.FindProperty("shootRangeMax");
    }

    private void OnSceneGUI() {
        Etienne.Gizmos.DrawCircle(targetTransform.position + UnityEngine.Vector3.up, targetTransform.rotation, minRange.floatValue * 2, UnityEngine.Color.blue);
        Etienne.Gizmos.DrawCircle(targetTransform.position + UnityEngine.Vector3.up, targetTransform.rotation, maxRange.floatValue * 2, UnityEngine.Color.blue);
    }

    private SerializedProperty minRange = null, maxRange = null;
    private UnityEngine.Transform targetTransform = null;
}
