using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static StartController;

public class PausePanel : MonoBehaviour {
    [SerializeField] GameObject pausePanel;

    [SerializeField] Button giveUpButton;
    [SerializeField] Button continueButton;
    [SerializeField] Button replayButton;
    [SerializeField] Button homeButton;

    [SerializeField] Button setting;
    [SerializeField] GameObject youLose;
    private void Awake() {
        giveUpButton.onClick.AddListener(GiveUp);
        continueButton.onClick.AddListener(disAble);
        replayButton.onClick.AddListener(Replay);
        homeButton.onClick.AddListener(BackHome);

    }
    void disAble() {
        Sound.Instance.NegativeClick();
        pausePanel.SetActive(false);
        setting.interactable = true;
    }
    void GiveUp() {
        Sound.Instance.LoseSound();
        Sound.Instance.StopSound();
        youLose.SetActive(true);
        pausePanel.SetActive(false);
        StartController.Instance.state = StartController.GameState.End;
    }
    void Replay() {
        Sound.Instance.PositiveClick();
        pausePanel.SetActive(false);
        setting.interactable = true;
        StartController.Instance.state = GameState.Continue;
        StartController.Instance.ReStart(); 
    }
    void BackHome() {
        pausePanel.SetActive(false);
        setting.interactable = true;
        EndGameManager.Instance.Home();
    }
}