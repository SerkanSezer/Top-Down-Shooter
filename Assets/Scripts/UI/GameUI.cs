using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Transform finishedSuccesPanel;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameFinished += ShowPlaceCleanPanel;
        gameManager.OnFinishedPanel += HidePlaceCleanPanel;
        levelText.SetText("LEVEL " + SceneManager.GetActiveScene().buildIndex.ToString());
    }
  
    public void ShowPlaceCleanPanel(bool status) {
        if (status) {
            finishedSuccesPanel.gameObject.SetActive(true);
        }
    }
    public void HidePlaceCleanPanel(int count, int totalPoint) {
        if (count>0) {
            finishedSuccesPanel.gameObject.SetActive(false);
        }
    }
}
