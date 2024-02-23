using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour {
    #region Singleton
    public static StartController Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        state = GameState.Continue;
    }
    #endregion

    public enum GameState {
        Start,
        Continue,
        End
    }
    public GameState state;
    [SerializeField] private Sprite basicSprite;
    public void ReStart() {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                GamePlay.Instance.cellState[i, j] = 0;
            }           
        }
        for (int i = 0; i < 9; i++) {
            Cell.Instance.cells[i].interactable = true;
            Cell.Instance.cells[i].image.sprite = basicSprite;
        }
        Signin.Instance._turn = Signin.EPlayerTurn.Player1;
        WinLineUI.Instance.ResetLine();
    }
}
