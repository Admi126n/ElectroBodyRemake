using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] List<Image> chipIndicator;
    [SerializeField] List<Image> weaponIndicator;
    [SerializeField] List<Image> temperatureIndicator;

    [Header("Weapon indicator sprites")]
    [SerializeField] Sprite blueWeapon;
    [SerializeField] Sprite greenWeapon;
    [SerializeField] Sprite yellowWeapon;

    [Header("Temperature indicator sprites")]
    [SerializeField] Sprite redIndicator;
    [SerializeField] Sprite lBlueSprite;
    [SerializeField] Sprite rBlueSprite;
    [SerializeField] Sprite lGreenSprite;
    [SerializeField] Sprite rGreenSprite;
    [SerializeField] Sprite lYellowSprite;
    [SerializeField] Sprite rYellowSprite;
    [SerializeField] Sprite lRedSprite;
    [SerializeField] Sprite rRedSprite;

    private Color32 _emptyChip = new(255, 255, 255, 64);
    private Color32 _solidChip = new(255, 255, 255, 255);
    private int _chipCounter = 0;

    private int _weaponCounter = 0;

    private Coroutine _weaponBlinking;

    void Start()
    {
        ResetChipIndicator();
        ResetWeaponIndicator();
    }

    // TODO: separate class ChipIndicatorManager

    private void ResetChipIndicator()
    {
        foreach (Image chip in chipIndicator)
        {
            chip.color = _emptyChip;
        }
    }

    private void FillChipIndicator()
    {
        for (int i = 0; i < _chipCounter; i++)
        {
            chipIndicator[i].color = _solidChip;
        }
    }

    public void UpdateChipIndicator()
    {
        if (_chipCounter >= 3) return;

        _chipCounter++;
        FillChipIndicator();

        if (_chipCounter == 3)
        {
            StartCoroutine(ChipBlinkinking());
        }
    }

    private IEnumerator ChipBlinkinking()
    {
        while (true)
        {
            FillChipIndicator();
            yield return new WaitForSeconds(0.3f);
            ResetChipIndicator();
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void ResetWeaponIndicator()
    {
        foreach (Image weapon in weaponIndicator)
        {
            weapon.sprite = blueWeapon;
        }
    }

    public void IncreaseWeaponIndicator(int weaponCounter)
    {
        StopCoroutine(WeaponBlinking());

        temperatureIndicator[0].sprite = greenWeapon;
        for (int i = 0; i < weaponCounter; i++)
        {
            if (i == 0)
            {
                weaponIndicator[i].sprite = yellowWeapon;
            }
            else
            {
                weaponIndicator[i].sprite = greenWeapon;
            }
        }
    }


    public void UpdateWeaponIndicator(int ammoCounter)
    {
        ResetWeaponIndicator();
        if (ammoCounter == 0)
        {
            //StopCoroutine(_weaponBlinking);
            temperatureIndicator[0].sprite = redIndicator;
        }
        else if (ammoCounter > K.Ammo.Weapon4 + 2)
        {
            FillWeaponIndicator(5);
        }
        else if (ammoCounter > K.Ammo.Weapon3 + 2)
        {
            FillWeaponIndicator(4);
        }
        else if (ammoCounter > K.Ammo.Weapon2 + 2)
        {
            FillWeaponIndicator(3);
        }
        else if (ammoCounter > K.Ammo.Weapon1 + 2)
        {
            FillWeaponIndicator(2);
        }
        else if (ammoCounter > 2)
        {
            FillWeaponIndicator(1);
        }
    }

    private void FillWeaponIndicator(int weapon)
    {
        weaponIndicator[0].sprite = yellowWeapon;

        for (int i = 1; i < weapon; i++)
        {
            weaponIndicator[i].sprite = greenWeapon;
        }
    }

    private void ManageWeaponBlinking()
    {
        if (_weaponBlinking == null)
        {
            _weaponBlinking = StartCoroutine(WeaponBlinking());
        }
    }

    private IEnumerator WeaponBlinking()
    {
        while (true)
        {
            weaponIndicator[_weaponCounter - 1].sprite = blueWeapon;
            yield return new WaitForSeconds(0.3f);

            if (_weaponCounter == 1)
            {
                weaponIndicator[_weaponCounter - 1].sprite = yellowWeapon;
            }
            else
            {
                weaponIndicator[_weaponCounter - 1].sprite = greenWeapon;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    public void UpdateTemperatureIndicator(int temp)
    {
        for (int i = 1; i < temperatureIndicator.Count; i++)
        {
            if (i < temp)
            {
                if (i == 1)
                {
                    temperatureIndicator[i].sprite = lGreenSprite;
                }
                else if (i == 2)
                {
                    temperatureIndicator[i].sprite = rGreenSprite;
                }
                else if (i == 3)
                {
                    temperatureIndicator[i].sprite = lYellowSprite;
                }
                else if (i == 4)
                {
                    temperatureIndicator[i].sprite = rYellowSprite;
                }
                else if (i % 2 != 0)
                {
                    temperatureIndicator[i].sprite = lRedSprite;
                }
                else
                {
                    temperatureIndicator[i].sprite = rRedSprite;
                }
            } else
            {
                if (i % 2 != 0)
                {
                    temperatureIndicator[i].sprite = lBlueSprite;
                } else
                {
                    temperatureIndicator[i].sprite = rBlueSprite;
                }
            }
        }
    }

}
