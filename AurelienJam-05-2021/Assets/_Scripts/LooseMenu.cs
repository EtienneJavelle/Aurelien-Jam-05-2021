using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseMenu : MonoBehaviour {

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
