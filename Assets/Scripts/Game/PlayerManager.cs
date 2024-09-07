using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Action<Character> PlayerJoined;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerInput.TryGetComponent(out Character character))
        {
            PlayerJoined?.Invoke(character);
        }
        else
        {
            Debug.LogError("YOU HAVE ROYALY SCREWED, BITCH");
        }
    }
}
