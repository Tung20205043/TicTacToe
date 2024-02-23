using UnityEngine;
using static EndGameManager;
public class GamePlay : MonoBehaviour {
    #region Singleton
    public static GamePlay Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public int[,] cellState = new int[3, 3];

    public void CheckForWin() {
        #region Check row, col
        for (int i = 0; i < 3; i++) {
            if (cellState[i, 0] != 0 && cellState[i, 0] == cellState[i, 1] && cellState[i, 1] == cellState[i, 2]) {
                WinLineUI.Instance.DrawHorizontal(i);
                UpdateResult(cellState[i, 0] == 1 ? 1 : 2);
            }
            if (cellState[0, i] != 0 && cellState[0, i] == cellState[1, i] && cellState[1, i] == cellState[2, i]) {
                WinLineUI.Instance.DrawVertical(i);
                UpdateResult(cellState[0, i] == 1 ? 1 : 2);
            }
        }
        #endregion

        #region Check diagonal
        if (cellState[0, 0] != 0 && cellState[0, 0] == cellState[1, 1] && cellState[1, 1] == cellState[2, 2]) {
            WinLineUI.Instance.DrawDiagonal(0);
            UpdateResult(cellState[0, 0] == 1 ? 1 : 2);
        }
        if (cellState[0, 2] != 0 && cellState[0, 2] == cellState[1, 1] && cellState[1, 1] == cellState[2, 0]) {
            WinLineUI.Instance.DrawDiagonal(1);
            UpdateResult(cellState[0, 2] == 1 ? 1 : 2);
        }
        #endregion

        #region Check draw
        bool isDraw = true;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (cellState[i, j] == 0) {
                    isDraw = false;
                    break;
                }
            }
            if (!isDraw) break;
        }
        if (isDraw) {
            if (StartController.Instance.state == StartController.GameState.End)
                return;
            EndGameManager.Instance._result = EResult.Draw;
            EndGameManager.Instance.EndGame(EndGameManager.Instance._result);
        }
        #endregion

    }
    public void UpdateResult(int i) {
        switch (i) {
            case 1:
                if (!GameManager.Instance.vsCom) {
                    EndGameManager.Instance._result = EResult.P1win;
                    EndGameManager.Instance.EndGame(EndGameManager.Instance._result);
                } 
                else {
                    EndGameManager.Instance._result = EResult.PlayerWin;
                    EndGameManager.Instance.EndGame(EndGameManager.Instance._result);
                }
                break;
            case 2:
                if (!GameManager.Instance.vsCom) {
                    EndGameManager.Instance._result = EResult.P2win;
                    EndGameManager.Instance.EndGame(EndGameManager.Instance._result);
                } else {
                    EndGameManager.Instance._result = EResult.AIWin;
                    EndGameManager.Instance.EndGame(EndGameManager.Instance._result);
                }
                break;
        }
    }
}
