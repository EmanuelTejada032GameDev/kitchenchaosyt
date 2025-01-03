using System;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] clearCounterSelectedVisuals;


    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectCounterChangedEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show(); 
        }
        else
            Hide();
    }


    private void Show()
    {
        foreach (var item in clearCounterSelectedVisuals)
        {
            item.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var item in clearCounterSelectedVisuals)
        {
            item.SetActive(false);
        }
    }
}
