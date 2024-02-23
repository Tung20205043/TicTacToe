using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static StartController;

public class EndGameManager : MonoBehaviour
{
    #region Singleton
    public static EndGameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < restartButton.Length; i++) {
            restartButton[i].onClick.AddListener(Restart);
        }
        for (int i = 0; i < homeButton.Length; i++) {
            homeButton[i].onClick.AddListener(Home);
        }

    }
    #endregion
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject drawPanel;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private TextMeshProUGUI[] noti;

    [SerializeField] private Button pauseButton;
    public enum EResult { 
        P1win,
        P2win,
        PlayerWin,
        AIWin,
        Draw
    }
    public EResult _result;
    public void EndGame(EResult result) {
        StartController.Instance.state = GameState.End;
        pauseButton.interactable = false;
        switch (result) {
            case EResult.P1win:
                Sound.Instance.WinSound();
                Sound.Instance.StopSound();
                winPanel.SetActive(true);
                noti[0].text = "O Win";
                break;
            case EResult.P2win:
                Sound.Instance.WinSound();
                Sound.Instance.StopSound();
                winPanel.SetActive(true);
                noti[0].text = "X Win";
                break;
            case EResult.Draw:
                Sound.Instance.DrawSound();
                Sound.Instance.StopSound();
                drawPanel.SetActive(true);
                noti[1].text = "Draw";
                break;
            case EResult.PlayerWin:
                Sound.Instance.WinSound();
                Sound.Instance.StopSound();
                winPanel.SetActive(true);
                noti[0].text = "You Win";
                break;
            case EResult.AIWin:
                Sound.Instance.LoseSound();
                Sound.Instance.StopSound();
                losePanel.SetActive(true);
                noti[2].text = "You Lose";
                break;
        }
    }

    [SerializeField] private Button[] restartButton;
    [SerializeField] private Button[] homeButton;

    [SerializeField] private GameObject homeScreen;
    [SerializeField] private GameObject playScreen;
    void Restart() {
        Sound.Instance.PositiveClick();
        ResetPanel();
        StartController.Instance.state = GameState.Continue;
        StartController.Instance.ReStart();
        
    }
    public void Home() {
        Sound.Instance.NegativeClick();
        ResetPanel();
        StartController.Instance.ReStart();
        StartController.Instance.state = GameState.Start;
        homeScreen.SetActive(true);
        playScreen.SetActive(false);
    }
    public void ResetPanel() {
        WinLineUI.Instance.ResetLine();
        pauseButton.interactable = true;
        winPanel.SetActive(false);
        drawPanel.SetActive(false);
        losePanel.SetActive(false);
        Sound.Instance.PlaySound();
    }
    
}
