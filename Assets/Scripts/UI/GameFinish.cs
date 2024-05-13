using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Transform winBG;
    [SerializeField] private Transform loseBG;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI killPointText;
    [SerializeField] private TextMeshProUGUI bonusPointText;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnFinishedPanel += OpenFinishedPanel;
    }

    public void OpenFinishedPanel(int count, int totalKillPoints) {
        if (count > 0) {
            winBG.gameObject.SetActive(true);
            killCountText.SetText(count.ToString());
            killPointText.SetText(totalKillPoints.ToString());
            bonusPointText.SetText(CalculateBonusPoints().ToString());
        }
        else {
            loseBG.gameObject.SetActive(true);
        }
    }

    public int CalculateBonusPoints() {
        float timeSince = Time.timeSinceLevelLoad;
        int bonusPoint = (int)((1 / timeSince) * 100000);
        return bonusPoint;
    }

    public void LoadLevel() {
        SceneManager.LoadScene(gameManager.LoadLevel());
    }
    public void BackHome() {
        SceneManager.LoadScene(0);
    }
}
