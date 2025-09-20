using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image gameClockUIFillImage;

    private void Update()
    {
        gameClockUIFillImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
