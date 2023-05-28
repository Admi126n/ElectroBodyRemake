using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmsController : MonoBehaviour
{
    PlayerController playerController;
    PlayerAnimator playerAnimator;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
    }

    public void TakeGun()
    {
        playerController.HasGun = true;
    }

    public void SetIsManagingGun(string value)
    {
        if (value.ToLower() == "y")
        {
            playerAnimator.SetIsManagingGun(true);
        } else
        {
            playerAnimator.SetIsManagingGun(false);
        }
        
    }
}
