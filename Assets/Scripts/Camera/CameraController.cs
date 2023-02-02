using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController playerController;
    public Animator CameraAnimator;
    private void Start()
    {
        Subscribe();
    }
    void SwitchCam()
    {
        if (playerController.currentPlayer == PlayerType.Stickman)
            CameraAnimator.SetTrigger("Stickman");
        else if (playerController.currentPlayer == PlayerType.Car)
            CameraAnimator.SetTrigger("Car"); 
        else if (playerController.currentPlayer == PlayerType.Aircraft)
            CameraAnimator.SetTrigger("Aircraft"); 
        else if (playerController.currentPlayer == PlayerType.Yacht)
            CameraAnimator.SetTrigger("Yacht");
    }

    void Subscribe()
    {
        playerController.OnPlayerChange += SwitchCam;
    }
    void Unsubscribe()
    {
        playerController.OnPlayerChange -= SwitchCam;
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
}
