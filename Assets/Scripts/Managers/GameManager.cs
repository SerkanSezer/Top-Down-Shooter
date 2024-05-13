using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int enemyCount;
    private int totalEnemyCount;
    private int totalKillPoints;

    public Transform crossHair;
    public event Action<bool> OnGameFinished;
    public event Action<int, int> OnFinishedPanel;

    void Start()
    {
        SetCursorDeactive();
    }

    public void SetCursorActive() {
        Cursor.visible = true;
        crossHair.gameObject.SetActive(false);
    }
    public void SetCursorDeactive() {
        Cursor.visible = false;
        crossHair.gameObject.SetActive(true);
    }

    public void AddEnemy() {
        enemyCount++;
        totalEnemyCount = enemyCount;
    }
    public void RemoveEnemy() {
        enemyCount--;
        if (enemyCount <= 0) {
            OnGameFinished?.Invoke(true);
        }
    }
    public void AddKillPoints(int points) {
        totalKillPoints += points;
    }

    public void ShowGameWinPanel() {
        SaveLevel(SceneManager.GetActiveScene().buildIndex + 1);
        OnFinishedPanel?.Invoke(totalEnemyCount,totalKillPoints);
        SetCursorActive();
    }
    public void ShowGameLosePanel() {
        OnFinishedPanel?.Invoke(0,0);
        OnGameFinished?.Invoke(false);
        SetCursorActive();
    }

    public void SaveLevel(int levelNumber) {
        PlayerPrefs.SetInt("Level", levelNumber);
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
