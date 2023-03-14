using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button sfxButton, musicButton, closeButton;
    [SerializeField] private TextMeshProUGUI sfxText, musicText;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        sfxButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateSFXVisuals();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateMusicVisuals();
        });
        closeButton.onClick.AddListener(() => Hide());
        GameStateManager.Instance.OnGameResumed += GameStateManager_OnGameResumed;
        UpdateSFXVisuals();
        UpdateMusicVisuals();
        Hide();
    }

    private void GameStateManager_OnGameResumed()
    {
        Hide();
    }

    private void UpdateSFXVisuals()
    {
        sfxText.text = $"SOUND EFFECTS: {Mathf.Round(SoundManager.Instance.GetVolume() * 10)}";
    }

    private void UpdateMusicVisuals()
    {
        musicText.text = $"MUSIC: {Mathf.Round(MusicManager.Instance.GetVolume() * 10)}";
    }

    // Update is called once per frame
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
