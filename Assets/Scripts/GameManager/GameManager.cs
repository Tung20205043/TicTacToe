using UnityEngine;

public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        difficult = EDifficult.Easy;
    }
    #endregion

    public bool vsCom { get; set; } = false;

    public enum EDifficult { 
        Easy,
        Medium,
        Hard
    }
    public EDifficult difficult;
    

}