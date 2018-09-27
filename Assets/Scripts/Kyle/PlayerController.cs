using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private LevelController levelController;

    private InputManager inputManager;
    public PlayerStatus playerStatus;
    

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();

        inputManager.OnSwitchButtonPressed += OnSwitchButtonPressed;
        inputManager.OnShootButtonPressed += OnShootButtonPressed;
        inputManager.OnInteractButtonPressed += OnInteractButtonPressed;

        playerStatus.isSpirit = false;
        playerStatus.moveAllowed = true;
    }

    private void OnSwitchButtonPressed()
    {
        if (playerStatus.isSpirit)
        {
            levelController.SwitchToRealWorld();
            playerStatus.isSpirit = false;
        }
        else
        {
            levelController.SwitchToSpiritWorld();
            playerStatus.isSpirit = true;
        }
    }

    private void OnShootButtonPressed()
    {
        // TODO: Add Shooting script
    }

    private void OnInteractButtonPressed()
    {
        // TODO: Add Interaction script
    }

    public struct PlayerStatus
    {
        public bool isSpirit;
        public bool moveAllowed;
    }
}
