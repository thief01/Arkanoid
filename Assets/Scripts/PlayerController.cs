using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private NewControls inputActions;
    private PlatformController platformController;
    private WeaponController weaponController;
    void Awake()
    {
        inputActions = new NewControls();
        inputActions.Enable();
        platformController = GetComponent<PlatformController>();
        weaponController = GetComponent<WeaponController>();
        inputActions.Keyboard.FreeBall.performed += ctg => platformController.FreeBall();
        inputActions.Keyboard.Shoot.performed += ctg => weaponController.Shoot();
    }
    void Start()
    {
        inputActions.Keyboard.Restart.performed += ctg => GameState.instace.RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        platformController.Move(inputActions.Keyboard.Horizontal.ReadValue<float>());
    }
}
