using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTemplate : MonoBehaviour
{
    [SerializeField] private Image image;

    public void Setup(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.icon;
    }
}
