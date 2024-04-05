using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRoomEntryLogic : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;
    [Header("Button Spawn Points")]
    [SerializeField] private Transform topButtonSpawn;
    [SerializeField] private Transform middleButtonSpawn;
    [SerializeField] private Transform bottomButtonSpawn;
    void Awake()
    {

        //when player enters shop, update shop visisted for shopo availability on board
        saveData.ShopVisited = true;
        SteamIntegration.UnlockAchievement("ShopVisited");

        //When shop room starts, spawn random item buttons in the top and middle buttons for purchasing

        //Spawn top button
        int topButtonIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
        GameObject topButton = Instantiate(saveData.MasterItemPool[topButtonIndex].itemShopButton, topButtonSpawn, false);

        //Spawn middle button
        int middleButtonIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
        GameObject middleButton = Instantiate(saveData.MasterItemPool[middleButtonIndex].itemShopButton, middleButtonSpawn, false);

        //Spawn bottom button
        int bottomButtonIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
        GameObject bottomButton = Instantiate(saveData.MasterItemPool[bottomButtonIndex].itemShopButton, bottomButtonSpawn, false);
    }
}
