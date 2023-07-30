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

    private Coroutine _weaponBlinking;

    private readonly List<List<Sprite>> _tempIndicatorSprites = new();

    void Start()
    {
        ResetWeaponIndicator();
        UpdateChipIndicator();

        SetTempIndicatorSprites();
    }

    private void SetTempIndicatorSprites()
    {
        _tempIndicatorSprites.Add(new() { lGreenSprite, rGreenSprite, lGreenSprite, rGreenSprite,
            lGreenSprite, rGreenSprite, lYellowSprite, rYellowSprite,
            lRedSprite, rRedSprite });

        _tempIndicatorSprites.Add(new() { lGreenSprite, rGreenSprite, lGreenSprite, rGreenSprite,
            lYellowSprite, rYellowSprite, lRedSprite, rRedSprite,
            lRedSprite, rRedSprite });

        _tempIndicatorSprites.Add(new() { lGreenSprite, rGreenSprite, lGreenSprite, rGreenSprite,
            lGreenSprite, rGreenSprite, lYellowSprite, rYellowSprite,
            lRedSprite, rRedSprite });

        _tempIndicatorSprites.Add(new() { lYellowSprite, rYellowSprite, lRedSprite, rRedSprite,
            lRedSprite, rRedSprite, lRedSprite, rRedSprite,
            lRedSprite, rRedSprite });

        _tempIndicatorSprites.Add(new() { lGreenSprite, rGreenSprite, lYellowSprite, rYellowSprite,
            lRedSprite, rRedSprite, lRedSprite, rRedSprite,
            lRedSprite, rRedSprite });
    }

    // TODO: separate classes ChipIndicatorManager, WeaponIndicatorManager,
    // TemperatureIndicatorManager

    private void ResetChipIndicator()
    {
        foreach (Image chip in chipIndicator)
        {
            chip.color = _emptyChip;
        }
    }

    private void FillChipIndicator()
    {
        for (int i = 0; i < ScenePresist.GetChipCounter(); i++)
        {
            chipIndicator[i].color = _solidChip;
        }
    }

    public void UpdateChipIndicator()
    {
        FillChipIndicator();

        if (ScenePresist.GetChipCounter() == 3)
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

    public void UpdateWeaponIndicator(int ammoCounter)
    {
        temperatureIndicator[0].sprite = ammoCounter == 0 ? redIndicator : greenWeapon;

        ResetWeaponIndicator();
        if (ammoCounter == 0)
        {
            if (_weaponBlinking == null) return;
            
            StopCoroutine(_weaponBlinking);
            _weaponBlinking = null;
            
        }
        else if (ammoCounter > K.Ammo.Weapon4 + 2) FillSolidWeaponIndicator(5);
        
        else if (ammoCounter > K.Ammo.Weapon4) FillBlinkingWeaponIndicator(5);

        else if (ammoCounter > K.Ammo.Weapon3 + 2) FillSolidWeaponIndicator(4);

        else if (ammoCounter > K.Ammo.Weapon3) FillBlinkingWeaponIndicator(4);

        else if (ammoCounter > K.Ammo.Weapon2 + 2) FillSolidWeaponIndicator(3);

        else if (ammoCounter > K.Ammo.Weapon2) FillBlinkingWeaponIndicator(3);

        else if (ammoCounter > K.Ammo.Weapon1 + 2) FillSolidWeaponIndicator(2);

        else if (ammoCounter > K.Ammo.Weapon1) FillBlinkingWeaponIndicator(2);

        else if (ammoCounter > 2) FillSolidWeaponIndicator(1);

        else if (ammoCounter >= 0) FillBlinkingWeaponIndicator(1);
    }

    private void FillWeaponIndicator(int weapon)
    {
        if (weapon == 0) return;

        weaponIndicator[0].sprite = yellowWeapon;

        for (int i = 1; i < weapon; i++)
        {
            weaponIndicator[i].sprite = greenWeapon;
        }
    }

    private void FillBlinkingWeaponIndicator(int weapon)
    {
        FillWeaponIndicator(weapon - 1);
        _weaponBlinking ??= StartCoroutine(WeaponBlinking(weapon));
    }

    private void FillSolidWeaponIndicator(int weapon)
    {
        if (_weaponBlinking != null)
        {
            StopCoroutine(_weaponBlinking);
            _weaponBlinking = null;
        }

        FillWeaponIndicator(weapon);
    }

    private IEnumerator WeaponBlinking(int weapon)
    {
        while (true)
        {
            weaponIndicator[weapon - 1].sprite = blueWeapon;
            yield return new WaitForSeconds(0.3f);

            if (weapon == 1)
            {
                weaponIndicator[weapon - 1].sprite = yellowWeapon;
            }
            else
            {
                weaponIndicator[weapon - 1].sprite = greenWeapon;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    public void UpdateTemperatureIndicator(int temp, int weapon)
    {
        for (int i = 1; i < temperatureIndicator.Count; i++)
        {
            if (i < temp)
            {
                if (weapon != 0)
                {
                    temperatureIndicator[i].sprite =
                    _tempIndicatorSprites[weapon - 1][i - 1];
                } else
                {
                    if (i % 2 != 0)
                    {
                        temperatureIndicator[i].sprite = lRedSprite;
                    }
                    else
                    {
                        temperatureIndicator[i].sprite = rRedSprite;
                    }
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
