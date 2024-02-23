using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Setting : MonoBehaviour {
    #region Singleton
    public static Setting Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //listen button on Awake
        plus.onClick.AddListener(() => OnClick(1));
        minus.onClick.AddListener(() => OnClick(-1));
        x.onClick.AddListener(Disable);

        pvpMode.onClick.AddListener(() => ChangeColor(0));
        pveMode.onClick.AddListener(() => ChangeColor(1));
    }
    #endregion

    #region Music setting
    [Header("Music")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Image _music;
    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite soundSprite;

    void Update() {
        Sound.Instance.musicSound = musicSlider.value;
        _music.sprite = (musicSlider.value == 0) ? muteSprite : soundSprite;
    }
    #endregion

    #region Diffcult setting
    [Header("Diffcult")]
    [SerializeField] private Button plus;
    [SerializeField] private Button minus;
    private int _difficult = 1;
    [SerializeField] private GameObject easyUI;
    [SerializeField] private GameObject mediumUI;
    [SerializeField] private GameObject hardUI;
    public void OnClick(int index) {
        Sound.Instance.PositiveClick();
        _difficult += index;
        if (_difficult == 0) {
            _difficult = 3;
        }
        if (_difficult == 4) {
            _difficult = 1;
        }

        easyUI.SetActive(false);
        mediumUI.SetActive(false);
        hardUI.SetActive(false);

        switch (_difficult) { 
            case 1:
                GameManager.Instance.difficult = GameManager.EDifficult.Easy;
                easyUI.SetActive(true);
                break;
            case 2:
                GameManager.Instance.difficult = GameManager.EDifficult.Medium;
                mediumUI.SetActive(true);
                break;
            case 3:
                GameManager.Instance.difficult = GameManager.EDifficult.Hard;
                hardUI.SetActive(true);
                break;
        }
    }
    #endregion

    #region X button
    [Header("X button")]
    [SerializeField] private Button x;
    [SerializeField] private GameObject settingUI;
    public void Disable() {
        Sound.Instance.NegativeClick();
        settingUI.SetActive(false);
        HomeScreen.Instance.settingButton.interactable = true;
    }
    #endregion

    #region Mode
    [SerializeField] private Button pvpMode;
    [SerializeField] private Button pveMode;

    private Color darkColor = Color.gray;
    private Color brightColor = Color.white;
    private void Start() {
        pvpMode.GetComponent<Image>().color = brightColor;
        pveMode.GetComponent<Image>().color = darkColor;
        GameManager.Instance.vsCom = false;
    }

    void ChangeColor(int i) {
        switch (i) {
            case 0:
                Sound.Instance.ModeSound(0);
                pvpMode.GetComponent<Image>().color = brightColor;
                pveMode.GetComponent<Image>().color = darkColor;
                GameManager.Instance.vsCom = false;
                break;
            case 1:
                Sound.Instance.ModeSound(1);
                pveMode.GetComponent<Image>().color = brightColor;
                pvpMode.GetComponent<Image>().color = darkColor;
                GameManager.Instance.vsCom = true;
                break;

        }       
    }
    #endregion

}
