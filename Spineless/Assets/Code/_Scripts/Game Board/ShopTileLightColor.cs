using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTileLightColor : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private Color closedShopColor;
    [SerializeField] private Color openShopColor;

    void OnEnable()
    {
        if (saveData.ShopVisited)
        {

            GetComponent<Image>().color = closedShopColor;
        }
        else
        {

            GetComponent<Image>().color = openShopColor;
        }

    }
}
