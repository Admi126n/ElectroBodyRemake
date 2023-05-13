using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunController : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] Bullet bullet_1;
    [SerializeField] Bullet bullet_2;
    [SerializeField] Bullet bullet_3;
    [SerializeField] Bullet bullet_4;
    [SerializeField] Bullet bullet_5;

    [Header("Gun")]
    [SerializeField] Transform gun;

    const int WeaponAmmo1 = 5;   // 20
    const int WeaponAmmo2 = 10;  // 35
    const int WeaponAmmo3 = 15;  // 60
    const int WeaponAmmo4 = 20;  // 70
    const int WeaponAmmo5 = 25;  // 85

    PlayerController playerController;
    AudioPlayer audioPlayer;

    private int weaponCounter = 0;
    private int ammoCounter = 0;

    public int AmmoCounter
    {
        get
        {
            return ammoCounter;
        }
    }

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
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
                Instantiate(bullet_1, gun.position, transform.rotation);
                break;
            case 2:
                Instantiate(bullet_2, gun.position, transform.rotation);
                break;
            case 3:
                Instantiate(bullet_3, gun.position, transform.rotation);
                break;
            case 4:
                Instantiate(bullet_4, gun.position, transform.rotation);
                break;
            case 5:
                Instantiate(bullet_5, gun.position, transform.rotation);
                break;
        }
    }

    public void PickUpAmmo()
    {
        audioPlayer.PlayAmmoPickedUpClip(playerController.transform.position);
        weaponCounter++;
        weaponCounter = Mathf.Clamp(weaponCounter, 0, 5);
        playerController.HasGun = true;
        RefillWeaponMagazine();
    }

    void OnFire()
    {
        if (ammoCounter > 0 && playerController.HasGun)
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
