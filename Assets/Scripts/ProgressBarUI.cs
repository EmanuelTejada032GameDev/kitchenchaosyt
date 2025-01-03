using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter CuttingCounter;

    private void Start()
    {
        CuttingCounter.OnCuttingProgressChanged += CuttingProgressChanged;
        barImage.fillAmount = 0;
        Hide();
    }

    private void CuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChangedEventsArgs e)
    {
        barImage.fillAmount = e.cuttingProgressNormalized;

        if(barImage.fillAmount == 0)
            Hide();
        else 
            Show(); 
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
