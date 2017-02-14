using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public void StartLevel(int levelIndex) {
        SceneManager.LoadScene(levelIndex);
    }

    public void ExitGame() {
        Application.Quit();
    }

}
