using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BY
{
    public class InputController : MonoBehaviour
    {
        [Header("DEBUG")]
        public Vector2 movement;
        public bool shoot;
        public float deadZone = 0.2f;
        public PlayerInputActions inputActions;
        public bool noMenu = false;

        void Awake()
        {
            inputActions = new PlayerInputActions();
            inputActions.PlayerActionMap.Movement.performed += GetMovementInput; //Subscribing the events
            inputActions.PlayerActionMap.Movement.canceled += GetMovementInput;
            inputActions.PlayerActionMap.Shoot.performed += GetShootInput;
            inputActions.PlayerActionMap.Shoot.canceled += GetShootInput;
            inputActions.PlayerActionMap.Pause.performed += _ => PauseGame();
            inputActions.UIActionMap.Start.performed += _ => StartGame();
            if (noMenu)
            {
                StartGame();
            }    
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            inputActions.UIActionMap.Enable();
        }

        private void OnDisable()
        {
            inputActions.UIActionMap.Disable();
            inputActions.PlayerActionMap.Disable();
        }

        private void GetMovementInput(InputAction.CallbackContext ctx)
        {
            movement = ctx.ReadValue<Vector2>();
        }

        public void StartGame()
        {
            Debug.Log("Starting/Resuming game");
            inputActions.UIActionMap.Disable();
            inputActions.PlayerActionMap.Enable();
        }

        public void PauseGame()
        {
            Debug.Log("Pausing game");
            inputActions.UIActionMap.Enable();
            inputActions.PlayerActionMap.Disable();
        }

        private void GetShootInput(InputAction.CallbackContext ctx)
        {
            shoot = ctx.ReadValue<float>() >= deadZone;
        }
    }

}