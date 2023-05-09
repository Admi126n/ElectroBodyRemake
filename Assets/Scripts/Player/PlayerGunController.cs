using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunController : MonoBehaviour
{
    const int WeaponAmmo1 = 5;   // 20
    const int WeaponAmmo2 = 10;  // 20 + 15
    const int WeaponAmmo3 = 15;  // 20 + 15 + 25
    const int WeaponAmmo4 = 20;  // 20 + 15 + 25 + 10
    const int WeaponAmmo5 = 25;  // 20 + 15 + 25 + 10 + 15

    PlayerController playerController;

    private int weaponCounter = 0;
    private int ammoCounter = 0;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void RefillWeaponMagazine()
    {
        switch (weaponCounter)
        {
            case 1:
                ammoCounter = WeaponAmmo1;
                break;
            case 2:
                ammoCounter = WeaponAmmo2;
                break;
            case 3:
                ammoCounter = WeaponAmmo3;
                break;
            case 4:
                ammoCounter = WeaponAmmo4;
                break;
            case 5:
                ammoCounter = WeaponAmmo5;
                break;
        }
    }

    private void UpdateWeapon()
    {
        if (ammoCounter == 0)
        {
            weaponCounter = 0;
        } else if (ammoCounter <= WeaponAmmo1)
        {
            weaponCounter = 1;
        } else if (ammoCounter <= WeaponAmmo2)
        {
            weaponCounter = 2;
        } else if (ammoCounter <= WeaponAmmo3)
        {
            weaponCounter = 3;
        } else if (ammoCounter <= WeaponAmmo4)
        {
            weaponCounter = 4;
        } else
        {
            weaponCounter = 5;
        }

    }

    private void Fire()
    {
        switch (weaponCounter)
        {
            case 1:
                Debug.Log("Fire with 1 weapon");
                break;
            case 2:
                Debug.Log("Fire with 2 weapon");
                break;
            case 3:
                Debug.Log("Fire with 3 weapon");
                break;
            case 4:
                Debug.Log("Fire with 4 weapon");
                break;
            case 5:
                Debug.Log("Fire with 5 weapon");
                break;
        }
    }

    public void PickUpAmmo()
    {
        Debug.Log("Ammo picked up");

        weaponCounter++;
        playerController.HasGun = true;
        RefillWeaponMagazine();
    }

    void OnFire()
    {
        if (ammoCounter > 0)
        {
            Fire();
            ammoCounter--;
            UpdateWeapon();

            if (ammoCounter == 0)
            {
                playerController.HasGun = false;
            }
        }
    }
}
