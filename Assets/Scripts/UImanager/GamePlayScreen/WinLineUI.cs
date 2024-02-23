using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLineUI : MonoBehaviour
{
    #region Singleton
    public static WinLineUI Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField] private GameObject[] vertical;
    [SerializeField] private GameObject[] horizontal;
    [SerializeField] private GameObject[] diagonal;
    public void DrawVertical(int i){
        vertical[i].gameObject.SetActive(true);
    }
    public void DrawHorizontal(int i) {
        horizontal[i].gameObject.SetActive(true) ;
    }
    public void DrawDiagonal(int i) {
        diagonal[i].gameObject.SetActive(true);
    }
    public void ResetLine() {
        foreach (GameObject obj in vertical) {
            obj.SetActive(false);
        }
        foreach (GameObject obj in horizontal) {
            obj.SetActive(false);
        }
        foreach (GameObject obj in diagonal) {
            obj.SetActive(false);
        }
    }
}
