using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunController : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] PlayerBullet bullet_1;
    [SerializeField] PlayerBullet bullet_2;
    [SerializeField] PlayerBullet bullet_3;
    [SerializeField] PlayerBullet bullet_4;
    [SerializeField] PlayerBullet bullet_5;

    [Header("Gun")]
    [SerializeField] Transform gun;

    private const int _WeaponAmmo1 = 5;   // 20
    private const int _WeaponAmmo2 = 10;  // 35
    private const int _WeaponAmmo3 = 15;  // 60
    private const int _WeaponAmmo4 = 20;  // 70
    private const int _WeaponAmmo5 = 25;  // 85

    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;
    private AudioPlayer _audioPlayer;

    private int _weaponCounter = 0;
    private int _ammoCounter = 0;

    public int AmmoCounter
    {
        get
        {
            return _ammoCounter;
        }
        private set
        {
            _ammoCounter = value;
        }
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void RefillWeaponMagazine()
    {
        switch (_weaponCounter)
        {
            case 1:
                _ammoCounter = _WeaponAmmo1;
                break;
            case 2:
                _ammoCounter = _WeaponAmmo2;
                break;
            case 3:
                _ammoCounter = _WeaponAmmo3;
                break;
            case 4:
                _ammoCounter = _WeaponAmmo4;
                break;
            case 5:
                _ammoCounter = _WeaponAmmo5;
                break;
        }
    }

    private void UpdateWeapon()
    {
        if (_ammoCounter == 0)
        {
            _weaponCounter = 0;
        } else if (_ammoCounter <= _WeaponAmmo1)
        {
            _weaponCounter = 1;
        } else if (_ammoCounter <= _WeaponAmmo2)
        {
            _weaponCounter = 2;
        } else if (_ammoCounter <= _WeaponAmmo3)
        {
            _weaponCounter = 3;
        } else if (_ammoCounter <= _WeaponAmmo4)
        {
            _weaponCounter = 4;
        } else
        {
            _weaponCounter = 5;
        }

    }

    private void Fire()
    {
        switch (_weaponCounter)
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

    private void PickUpAmmo()
    {
        _audioPlayer.PlayAmmoPickedUpClip(_playerController.transform.position);
        _weaponCounter++;
        _weaponCounter = Mathf.Clamp(_weaponCounter, 0, 5);

        if (!_playerController.HasGun)
        {
            _playerAnimator.TriggerGunTaking();
        }
        RefillWeaponMagazine();
    }

    /// <summary>
    /// Method triggered by input system.
    /// </summary>
    void OnFire()
    {
        if (_ammoCounter > 0 && _playerController.HasGun)
        {
            Fire();
            _ammoCounter--;
            UpdateWeapon();

            if (_ammoCounter == 0)
            {
                _playerController.HasGun = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.Ammo))
        {
            Destroy(collision.gameObject);
            PickUpAmmo();
        }
    }
}
