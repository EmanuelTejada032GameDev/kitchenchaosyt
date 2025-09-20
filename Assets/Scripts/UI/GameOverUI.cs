using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deliveredRecipesText;

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        Hide();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        {
            Show();
            deliveredRecipesText.SetText(DeliveryManager.Instance.GetRecipesAmountDelivered().ToString());
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
