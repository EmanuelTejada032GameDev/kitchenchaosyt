using System;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject clearCounterSelectedVisual;


    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectCounterChangedEventArgs e)
    {
        if(e.selectedCounter == clearCounter)
            Show();
        else
            Hide();
    }


    private void Show()
    {
        clearCounterSelectedVisual.SetActive(true);
    }

    private void Hide()
    {
        clearCounterSelectedVisual.SetActive(false);
    }
}
