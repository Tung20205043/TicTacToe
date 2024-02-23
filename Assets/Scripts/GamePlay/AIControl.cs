using UnityEngine;
using Cysharp.Threading.Tasks;
using static GameManager;


public class AIControl : MonoBehaviour
{
    #region Singleton
    public static AIControl Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private readonly int none = 0;
    private readonly int human = 1;
    private readonly int ai = 2;

    public int[,] board = new int[3, 3];
    
    public void BestMove() {
        float bestScore  = int.MinValue;
        //Bien xac dinh nuoc di AI
        Vector2Int move = Vector2Int.zero;
        int moveInt = 0;


        for (int i = 0; i < 9; i++) {
            int row = i / 3;
            int col = i % 3;

            board = GamePlay.Instance.cellState;
            // Is the spot available?
            if (board[row, col] == none) {

                // Đánh dấu là sẽ đi nước nay
                board[row, col] = ai;
                // Kiểm tra xem Mode chơi hiện tại đang ở mức nào để quyết định thuật toán sử dụng
                int score = 0;
                if (GameManager.Instance.difficult == EDifficult.Medium)
                    score = Minimax(board, false);
                else if (GameManager.Instance.difficult == EDifficult.Hard)
                    score = Minimax(board, 0, false);
                //Hủy nước đi này 
                board[row, col] = none;
                if (score > bestScore) {
                    bestScore = score;
                    move = new Vector2Int(row, col);
                    moveInt = i;
                }
            }

        }

    //Draw: 
        AIsign(move.x, move.y, moveInt);
        
    }
    public void AIsign(int row, int col, int i) {
        Cell.Instance.DrawSign(Cell.Instance.cells[i], 0);
        GamePlay.Instance.cellState[row, col] = 2;
        Signin.Instance.TurnCheck();
        Cell.Instance.InteractAble(Cell.Instance.cells[i]);
        GamePlay.Instance.CheckForWin();
    }
    #region Ramdom Play
    public void MoveRandom() {
        
    }
    #endregion
    public enum EResult {
        Player = -10,
        AI = +10,
        Tie = 0,
        Null = -99
    }
    #region Minimax with depth
    public int Minimax(int[,] board, int depth, bool isMaximizing) {
        EResult result = GetWinner();
        if (result != EResult.Null) {
            // nếu đã tìm được người chiến thắng || hoà thì kết thúc đệ quy
            return (int)result - depth;
        }
        #region Maximizing
        if (isMaximizing) {
            // duyệt với lượt chơi của AI
            int bestScore = int.MinValue;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (board[i, j] == none) {
                        board[i, j] = ai;
                        int score = Minimax(board, depth + 1, false);
                        board[i, j] = none;
                        bestScore = Mathf.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        #endregion

        #region Minimizing
        else {
            // duyệt với lượt chơi của Human
            int bestScore = int.MaxValue;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (board[i, j] == none) {
                        board[i, j] = human;
                        int score = Minimax(board, depth + 1, true);
                        board[i, j] = none;
                        bestScore = Mathf.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        #endregion
    }
    #endregion

    #region Minamax without depth
    public int Minimax(int[,] board, bool isMaximizing) {
        EResult result = GetWinner();
        if (result != EResult.Null) {
            // nếu đã tìm được người chiến thắng || hoà thì kết thúc đệ quy
            return (int)result;
        }

        if (isMaximizing) {
            // duyệt với lượt chơi của AI
            int bestScore = int.MinValue;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (board[i, j] == none) {
                        board[i, j] = ai;
                        int score = Minimax(board, false);
                        board[i, j] = none;
                        bestScore = Mathf.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        } else {
            // duyệt với lượt chơi của Human
            int bestScore = int.MaxValue;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (board[i, j] == none) {
                        board[i, j] = human;
                        int score = Minimax(board, true);
                        board[i, j] = none;
                        bestScore = Mathf.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }
    #endregion
    private EResult GetWinner() {
        EResult winner = EResult.Null;
        #region check winner
        for (int i = 0; i < 3; i++) {
            if (board[i, 0] != none && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) {
                winner = board[i, 0] == human ? EResult.Player : EResult.AI;
            }
            if (board[0, i] != none && board[0, i] == board[1, i] && board[1, i] == board[2, i]) {
                winner = board[0, i] == human ? EResult.Player : EResult.AI;
            }
        }
        if (board[0, 0] != none && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) {
            winner = board[0, 0] == human ? EResult.Player : EResult.AI;
        }
        if (board[0, 2] != none && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) {
            winner = board[0, 2] == human ? EResult.Player : EResult.AI;
        }
        #endregion

        int openSpots = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (board[i, j] == none) {
                    openSpots++;
                }
            }
        }

        if (winner == EResult.Null && openSpots == 0) {
            return EResult.Tie;
        }
        return winner;
    }
}
