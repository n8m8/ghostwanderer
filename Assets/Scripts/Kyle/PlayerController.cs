using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle
public class PlayerController : MonoBehaviour {

    [SerializeField] private LevelController levelController;

    private InputManager inputManager;
    public PlayerStatus playerStatus;
    public Sprite ghostSprite;
    public Sprite humanSprite;
    

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();

        inputManager.OnSwitchButtonPressed += OnSwitchButtonPressed;
        inputManager.OnShootButtonPressed += OnShootButtonPressed;
        inputManager.OnInteractButtonPressed += OnInteractButtonPressed;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = humanSprite;
        playerStatus.isSpirit = false;
        playerStatus.moveAllowed = true;
    }

    private void OnSwitchButtonPressed()
    {
        if (playerStatus.isSpirit)
        {   
            this.gameObject.GetComponent<SpriteRenderer>().sprite = humanSprite;
            levelController.SwitchToRealWorld();
            playerStatus.isSpirit = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
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
