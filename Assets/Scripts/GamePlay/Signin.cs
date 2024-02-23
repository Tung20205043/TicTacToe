using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signin : MonoBehaviour
{
    #region Singleton
    public static Signin Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public enum EPlayerTurn { 
        Player1,
        Player2
    }
    public EPlayerTurn _turn { get; set; } /*= EPlayerTurn.Player2;*/

    public Sprite[] signin;

    public void TurnCheck() {
        
        _turn = (_turn == EPlayerTurn.Player1) ? EPlayerTurn.Player2 : EPlayerTurn.Player1;

    }
}
