using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour
{
    public void Play() {
        AudioManager.instance.PlayButtonSound();
        SceneManager.LoadScene(LoadLevel());
    }

    public void QuitGame() {
        AudioManager.instance.PlayButtonSound();
        Application.Quit();
    }

    public int LoadLevel() {
        if (PlayerPrefs.HasKey("Level")) {
            return PlayerPrefs.GetInt("Level");
        }
        else {
            return 1;
        }
    }
}
