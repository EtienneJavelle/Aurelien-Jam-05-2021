using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private float range = 5f;
    [SerializeField] private float spawnFrequency = 500f;
    [SerializeField] private GameObject enemy = null;

    private void OnEnable() {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        while(true) {
            Vector2 rand = Random.insideUnitCircle;
            Vector3 spawnPos = this.transform.position + new Vector3(rand.x, 0, rand.y) * this.range;
            Instantiate(this.enemy, spawnPos, Quaternion.identity, this.transform);
            yield return new WaitForSecondsRealtime(this.spawnFrequency / 1000f);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.transform.position, this.range);
        Vector2 rand = Random.insideUnitCircle;
        Vector3 spawnPos = this.transform.position + new Vector3(rand.x, 0, rand.y) * this.range;
        Gizmos.DrawSphere(spawnPos, 1f);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor {
    private void OnSceneGUI() {
        //EnemySpawner inspectedGo = this.target as EnemySpawner;
        //SerializedProperty range = this.serializedObject.FindProperty("range");
        //Handles.SphereHandleCap(0, inspectedGo.transform.position, Quaternion.identity, range.floatValue, EventType.Repaint);
        //Handles.
        ////Gizmos.DrawSphere(inspectedGo.transform.position, range.floatValue);
    }
}
#endif
