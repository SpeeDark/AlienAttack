using UnityEngine;
using UnityEngine.Events;

public class UI_Elements : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject menuButton;

    private bool gameIsOver => GameFinisher.Instance.GameIsOver;

    public UnityEvent OnContinueButtonPressed;

    private void Start()
    {
        GameFinisher.Instance.OnGameOver.AddListener(ShowGameOverUI);
    }

    public void ActivatePauseUI()
    {
        SetActivePauseUI(true);
    }

    public void DeactivatePauseUI()
    {
        SetActivePauseUI(false);
    }

    public void OnContinueButton()
    {
        if (gameIsOver)
        {
            SceneLoader.SwitchScene("Menu");
        }
        else
        {
            DeactivatePauseUI();
            OnContinueButtonPressed.Invoke();
        }
    }

    public void OnMenuButton()
    {
        SetActivePauseUI(false);
        gameOverText.SetActive(false);

        SceneLoader.SwitchScene("Menu");
    }

    private void ShowGameOverUI()
    {
        continueButton.SetActive(true);
        pausePanel.SetActive(true);
        gameOverText.SetActive(true);
    }

    private void SetActivePauseUI(bool active)
    {
        continueButton.SetActive(active);
        pausePanel.SetActive(active);
        menuButton.SetActive(active);
    }
}