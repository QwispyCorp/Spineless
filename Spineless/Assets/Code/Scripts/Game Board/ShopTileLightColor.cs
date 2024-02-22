using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTileLightColor : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [SerializeField] private Color closedShopColor;
    [SerializeField] private Color openShopColor;

    void OnEnable()
    {
        if (saveData.ShopVisited)
        {

            GetComponent<Light>().color = closedShopColor;
        }
        else
        {

            GetComponent<Light>().color = openShopColor;
        }

    }
}
