using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmsController : MonoBehaviour
{
    PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void TakeGun()
    {
        playerController.HasGun = true;
    }
}
