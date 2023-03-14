using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIClock : MonoBehaviour
{
    [SerializeField] Image clockTimerImage;

    private void Update()
    {
        clockTimerImage.fillAmount = GameStateManager.Instance.GetGamePlayingTimerNormalized();
    }
}
