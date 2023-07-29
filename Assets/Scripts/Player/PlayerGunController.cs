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

    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;
    private AudioPlayer _audioPlayer;
    private UIManager _UIManager;

    private int _weaponCounter = 0;
    private int _ammoCounter = 0;
    private int _weaponTemp = 0;
    private const int MaxTemp = 10;
    private Dictionary<int, int> WeaponTempDict = new() {
        {1, 2},
        {2, 3},
        {3, 4},
        {4, 5},
        {5, 6}
    };

    public int AmmoCounter
    {
        get
        {
            return _ammoCounter;
        }
        private set
        {
            _ammoCounter = value;
            _UIManager.UpdateWeaponIndicator(_ammoCounter);
        }
    }

    public int WeaponTemp
    {
        get { return _weaponTemp; }
        private set
        {
            _weaponTemp = value;
            _UIManager.UpdateTemperatureIndicator(_weaponTemp);
        }
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _UIManager = FindObjectOfType<UIManager>();

        StartCoroutine(WeaponCooling());
    }

    private void RefillWeaponMagazine()
    {
        switch (_weaponCounter)
        {
            case 1:
                AmmoCounter = K.Ammo.Weapon1;
                break;
            case 2:
                AmmoCounter = K.Ammo.Weapon2;
                break;
            case 3:
                AmmoCounter = K.Ammo.Weapon3;
                break;
            case 4:
                AmmoCounter = K.Ammo.Weapon4;
                break;
            case 5:
                AmmoCounter = K.Ammo.Weapon5;
                break;
        }
    }

    private void UpdateWeapon()
    {
        if (AmmoCounter == 0)
        {
            _weaponCounter = 0;
        } else if (AmmoCounter <= K.Ammo.Weapon1)
        {
            _weaponCounter = 1;
        } else if (AmmoCounter <= K.Ammo.Weapon2)
        {
            _weaponCounter = 2;
        } else if (AmmoCounter <= K.Ammo.Weapon3)
        {
            _weaponCounter = 3;
        } else if (AmmoCounter <= K.Ammo.Weapon4)
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

    public void ResetAmmo()
    {
        _playerController.HasGun = false;
        _weaponCounter = 0;
        AmmoCounter = 0;
    }

    /// <summary>
    /// Method triggered by input system.
    /// </summary>
    void OnFire()
    {
        if (AmmoCounter > 0
            && _playerController.HasGun
            && WeaponTemp + WeaponTempDict[_weaponCounter] <= MaxTemp)
        {
            Fire();
            AmmoCounter--;
            WeaponTemp += WeaponTempDict[_weaponCounter];
            UpdateWeapon();

            if (AmmoCounter == 0)
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

    private IEnumerator WeaponCooling()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (WeaponTemp > 0)
            {
                WeaponTemp--;
            }
        }
    }
}
