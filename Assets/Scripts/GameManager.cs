using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonBaseClass<GameManager>
{
    [SerializeField] private Text panelTitle;
    [SerializeField] private GameObject panel,
        resumeButton;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);
        resumeButton.SetActive(true);
        panelTitle.text = "GAME PAUSED";
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void FinishGame()
    {
        panel.SetActive(true);
        resumeButton.SetActive(false);
        panelTitle.text = "GAME COMPLETED";
    }
    
    public void GameFailed()
    {
        panel.SetActive(true);
        resumeButton.SetActive(false);
        panelTitle.text = "GAME FAILED";
    }
}
