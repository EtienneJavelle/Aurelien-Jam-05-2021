using UnityEngine;

public class DeathHandeler : MonoBehaviour {
    [SerializeField] private Sprite[] deadSprites = new Sprite[3];
    private void Awake() {
        powerHandeler = GetComponent<PowerHandeler>();
        enabled = false;
    }
    private void OnEnable() {
        int power = powerHandeler.Power;
        if(powerHandeler.LooseLife(power, deadSprites[power - 1])) {
            enabled = false;
            return;
        }
        GetComponentInChildren<Animator>().SetBool("isDead", true);
        GetComponent<PowerHandeler>().enabled = false;
        GetComponent<CMF.AdvancedWalkerController>().enabled = false;
        GetComponentInChildren<TurnTowardAimPosition>().enabled = false;
        GameManager.Instance.UI.LooseGo.SetActive(true);

    }

    private PowerHandeler powerHandeler = null;
}
