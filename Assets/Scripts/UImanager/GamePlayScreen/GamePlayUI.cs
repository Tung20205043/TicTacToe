using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [Header("Diffcult UI")]
    [SerializeField] private TextMeshProUGUI difficultText;
    [SerializeField] private GameObject difficultPref;
    [SerializeField] private GameObject pvpPref;
    [Header("Avatar - Name")]
    [SerializeField] private TextMeshProUGUI p1Name;
    [SerializeField] private TextMeshProUGUI p2Name;
    [SerializeField] private Image p1Avt;
    [SerializeField] private Image p2Avt;

    [SerializeField] private Sprite p1O;
    [SerializeField] private Sprite p2X;
    [SerializeField] private Sprite player;
    [SerializeField] private Sprite bot;
    
    [SerializeField] private TextMeshProUGUI turnText;
    void Update() {
        #region update turn
        switch (Signin.Instance._turn) {
            case Signin.EPlayerTurn.Player1:
                turnText.text = GameManager.Instance.vsCom ? "Player's turn" : "Player O's turn";
                break;
            case Signin.EPlayerTurn.Player2:
                turnText.text = GameManager.Instance.vsCom ? "Computer's turn" : "Player X's turn";
                break;
        }
        #endregion

        #region update avt - name
        switch (GameManager.Instance.difficult) {
            case GameManager.EDifficult.Easy:
                difficultText.text = "Easy";
                break;
            case GameManager.EDifficult.Medium:
                difficultText.text = "Medium";
                break;
            case GameManager.EDifficult.Hard:
                difficultText.text = "Hard";
                break;
        }
        difficultPref.SetActive(GameManager.Instance.vsCom ? true : false);
        pvpPref.SetActive(!GameManager.Instance.vsCom ? true : false);
        // avt - name
        p1Avt.sprite = !GameManager.Instance.vsCom ? p1O : player;
        p2Avt.sprite = !GameManager.Instance.vsCom ? p2X : bot;

        p1Name.text = !GameManager.Instance.vsCom ? "Player O" : "Player";
        p2Name.text = !GameManager.Instance.vsCom ? "Player X" : "Computer";
        #endregion
    }


    [Header("Pause")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePref;

    private void Awake() {
        pauseButton.onClick.AddListener(PausePref);
    }
    void PausePref() {
        Sound.Instance.PositiveClick();
        pausePref.SetActive(true);
        pauseButton.interactable = false;
    }

}
