using DG.Tweening;
using UnityEngine;

public class Footprint : MonoBehaviour {
    [SerializeField] private float shrinkTime = 3f;
    [SerializeField] private Ease ease = Ease.InExpo;
    private void OnEnable() {
        transform.DOScale(0, shrinkTime).SetEase(ease).OnComplete(() => GameObject.Destroy(gameObject));
    }
}
