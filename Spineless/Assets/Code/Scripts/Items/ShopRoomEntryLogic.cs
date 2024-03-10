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
    void Start()
    {
        //when player enters shop, update shop visisted for shopo availability on board
        saveData.ShopVisited = true;

        //When shop room starts, spawn random item buttons in the top and middle buttons for purchasing

        //Spawn top button
        int topButtonIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
        GameObject topButton = Instantiate(saveData.MasterItemPool[topButtonIndex].itemShopButton, topButtonSpawn, false);

        //Spawn middle button
        int middleButtonIndex;
        while (true)
        {
            middleButtonIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
            if (middleButtonIndex != topButtonIndex)
            {
                GameObject middleButton = Instantiate(saveData.MasterItemPool[middleButtonIndex].itemShopButton, middleButtonSpawn, false);
                break;
            }
        }
        
        //Spawn bottom button
        int bottomButtonIndex;
        while (true)
        {
            bottomButtonIndex = UnityEngine.Random.Range(0, saveData.MasterItemPool.Count);
            if (bottomButtonIndex != topButtonIndex && bottomButtonIndex != middleButtonIndex)
            {
                GameObject bottomButton = Instantiate(saveData.MasterItemPool[bottomButtonIndex].itemShopButton, bottomButtonSpawn, false);
                break;
            }
        }
    }
}
