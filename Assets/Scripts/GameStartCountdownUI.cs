using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI countdownText;
    private Animator animator;
    private int previousCountdownNumber;
    private const string NUMBER_POPUP = "NumberPopup";
    private void Awake()
    {
       animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;
        Hide();
    }
    private void Update()
    {
        int currentCountdownNumber = Mathf.RoundToInt(GameStateManager.Instance.GetCountdownToStartTimer());
        if (currentCountdownNumber == 0)
        {
            countdownText.text = "COOK!";
        }
        else
        {
            countdownText.text = currentCountdownNumber.ToString();
        }
        if(previousCountdownNumber != currentCountdownNumber)
        {
            previousCountdownNumber = currentCountdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
    }
    private void GameStateManager_OnStateChanged()
    {
        if (GameStateManager.Instance.IsCountdownToStartActive())
        {
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
