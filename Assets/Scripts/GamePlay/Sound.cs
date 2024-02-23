using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    #region Singleton
    public static Sound Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    [SerializeField] private AudioSource[] resultAudio;
    [SerializeField] private AudioSource[] click;
    [SerializeField] private AudioSource[] modeSound;

    [SerializeField] private AudioSource sign;

    public void WinSound() { 
        resultAudio[0].Play();
    }
    public void DrawSound() { 
        resultAudio[1].Play();
    }
    public void LoseSound() { 
        resultAudio[2].Play();
    }
    public void PositiveClick() { 
        click[0].Play();
    }
    public void NegativeClick() {
        click[1].Play();
    }
    [SerializeField] private AudioSource MusicSound;
    public float musicSound { get; set; } = 0.5f;
    void Update() {
        MusicSound.volume = musicSound;
    }
    public void StopSound() {
        MusicSound.Stop();
    }
    public void PlaySound() {
        MusicSound.Play();
    }
    public void SignSound() { 
        sign.Play();    
    }
    public void ModeSound(int i) { 
        modeSound[i].Play();    
    }
}
