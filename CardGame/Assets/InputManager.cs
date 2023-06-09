using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameplayInput gameplayInput;

    private void Awake()
    {
        gameplayInput = new GameplayInput();
    }

    private void Start()
    {
        gameplayInput.Gameplay.Primary.started += ctx => GameplayManager.humanPlayer.ClickCard();
        gameplayInput.Gameplay.Primary.canceled += ctx => GameplayManager.humanPlayer.ReleaseCard();
    }

    private void OnEnable()
    {
        gameplayInput.Enable();
    }

    private void OnDisable()
    {
        gameplayInput.Disable();
    }
}
