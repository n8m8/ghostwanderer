using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField] private KeyCode switchWorldButton;
    [SerializeField] private KeyCode shootButton;
    [SerializeField] private KeyCode interactButton;

    public System.Action OnSwitchButtonPressed;
    public System.Action OnShootButtonPressed;
    public System.Action OnInteractButtonPressed;

    void Update()
    {
        if (Input.GetKeyDown(switchWorldButton) && OnSwitchButtonPressed != null) OnSwitchButtonPressed();
        if (Input.GetKeyDown(shootButton) && OnShootButtonPressed != null) OnShootButtonPressed();
        if (Input.GetKeyDown(interactButton) && OnInteractButtonPressed != null) OnInteractButtonPressed();
    }
}
