using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    public static Cell Instance { get; private set; }
    public Button[] cells;
    private void Awake() {
        Instance = this;
        for (int i = 0; i < cells.Length; i++) {
            int index = i;
            cells[i].onClick.AddListener(() => ChangeImage(index));
        }
    }
    
    public void ChangeImage(int i) {
        //sound
        Sound.Instance.SignSound();

        switch (GameManager.Instance.vsCom) {

            case false:
                #region vsHuman             
                
                if (!ContinueGame()) { return; }
                PlayerMakeMove(i);
                #endregion
                break;

            case true:
                #region vsCom
                //PlayerMove
                if (Signin.Instance._turn == Signin.EPlayerTurn.Player1) {
                    if (!ContinueGame()) { return; }
                    PlayerMakeMove(i);
                }
                //AiMove
                if (Signin.Instance._turn == Signin.EPlayerTurn.Player2) {
                    if (!ContinueGame()) { return; }                 
                    AIControl.Instance.BestMove();

                }
                #endregion
                break;
        }
    }

    public void InteractAble(Button a) {
        a.interactable = false;
        ColorBlock colors = a.colors;
        colors.disabledColor = Color.white;
        a.colors = colors;
    }
    public void DrawSign(Button a, int color) {
        a.image.sprite = Signin.Instance.signin[color];
    }
    public bool ContinueGame() {
        return StartController.Instance.state == StartController.GameState.Continue ? true : false;
    }
    public void PlayerMakeMove(int i) {
        int row = i / 3;
        int col = i % 3;
        // Cập nhật trạng thái của ô trong ma trận cellStates
        GamePlay.Instance.cellState[row, col] = Signin.Instance._turn == Signin.EPlayerTurn.Player1 ? 1 : 2;
        DrawSign(cells[i], Signin.Instance._turn == Signin.EPlayerTurn.Player1 ? 1 : 0);
        InteractAble(cells[i]);
        Signin.Instance.TurnCheck();
        GamePlay.Instance.CheckForWin();
    }
}
