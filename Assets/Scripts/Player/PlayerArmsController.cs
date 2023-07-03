using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmsController : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerAnimator = FindObjectOfType<PlayerAnimator>();
    }

    /// <summary>
    /// Method is called in ArmsTakeGun.
    /// </summary>
    /// <param name="value"></param>
    public void SetPlayerControlerHasGun(float value)
    {
        _playerController.HasGun = value == 1.0f;
    }

    /// <summary>
    /// Method is called in ArmsTakeGun and ArmsHideGun animations.
    /// </summary>
    /// <param name="value"></param>
    public void SetIsManagingGun(float value)
    {
        _playerAnimator.IsManagingGun = value == 1.0f;
    }
}
