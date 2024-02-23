using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    #region Singleton
    public static HomeScreen Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        // add lisner
        settingButton.onClick.AddListener(SettingOn);
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }
    #endregion
    [Header("Button")]
    public Button settingButton;
    public Button startButton;
    public Button exitButton;
    [SerializeField] private GameObject settingPrefab;
    [Header("UI")]
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject gamePrefab;
    public void SettingOn() {
        Sound.Instance.PositiveClick();
        settingPrefab.SetActive(true);
        settingButton.interactable = false;
    }
    public void StartGame() {
        Sound.Instance.PositiveClick();
        startPrefab.SetActive(false);
        gamePrefab.SetActive(true);
        StartController.Instance.state = StartController.GameState.Continue;
    }
    public void ExitGame() {
        Application.Quit();
    }
}
