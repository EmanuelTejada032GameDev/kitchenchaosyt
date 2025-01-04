using System;
using UnityEngine;
using UnityEngine.UI;
using static IHasProgress;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;

    [SerializeField] private GameObject IHasProgressParentGasmeObject;
    private IHasProgress hasProgressItem;

    private void Start()
    {
        hasProgressItem = IHasProgressParentGasmeObject.GetComponent<IHasProgress>();
        if(hasProgressItem == null)
        {
            Debug.LogError($"ProgressParent Game object {IHasProgressParentGasmeObject} does not implements IHassProgress");
        }

        hasProgressItem.OnProgressChanged += IHasProgressItem_OnProgressChanged;
        barImage.fillAmount = 0;
        Hide();
    }

    private void IHasProgressItem_OnProgressChanged(object sender, OnProgressChangedEventsArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (barImage.fillAmount == 0)
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
