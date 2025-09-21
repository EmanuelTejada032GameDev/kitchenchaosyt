using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour
{
    public static GameOptionsUI Instance;

    [SerializeField] private Button soundEffectsBtn;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;

        soundEffectsBtn.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicBtn.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeBtn.onClick.AddListener(() => Hide());

    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        Hide(); 
        UpdateVisual();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
       Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "sound effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

}
