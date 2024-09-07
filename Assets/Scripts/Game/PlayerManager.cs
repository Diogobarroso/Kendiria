using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Action PlayerJoined;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Player joined");
        PlayerJoined?.Invoke();
    }
}
