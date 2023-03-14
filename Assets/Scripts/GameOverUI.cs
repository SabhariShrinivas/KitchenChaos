using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI reciepieDeliveredText;
    [SerializeField] private Button mainMenuButton, playAgainButton;
    // Start is called before the first frame update
    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => Loader.LoadScene(Loader.Scene.MainMenu));
        playAgainButton.onClick.AddListener(() => Loader.LoadScene(Loader.Scene.GameScene));
    }
    private void Start()
    {
        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;
        Hide();
    }
    private void Update()
    {

    }
    private void GameStateManager_OnStateChanged()
    {
        if (GameStateManager.Instance.IsGameOver())
        {
            reciepieDeliveredText.text = DeliveryManager.Instance.GetSuccessfulReciepieAmount().ToString();
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
