using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoomButton : MonoBehaviour
{
    public void BackToBoardRoom()
    {
        LightManager.Instance.StartFlickeringTransitionTo("GameBoard");
    }
}
