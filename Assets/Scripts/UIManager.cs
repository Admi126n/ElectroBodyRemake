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

    public void UpdateWeaponIndicator(int ammoCounter)
    {

    }

    //private IEnumerator WeaponBlinking()
    //{
        //while (true)
        //{
        //    weaponIndicator[_weaponCounter - 1].sprite = blueWeapon;
        //    yield return new WaitForSeconds(0.3f);
        //    weaponIndicator[_weaponCounter - 1].sprite = greenWeapon;
        //    yield return new WaitForSeconds(0.3f);
        //}
    //}

    public void UpdateTemperatureIndicator(int temp)
    {
        for (int i = 0; i < temperatureIndicator.Count; i++)
        {
            if (i < temp)
            {
                if (i == 0)
                {
                    temperatureIndicator[i].sprite = lGreenSprite;
                }
                else if (i == 1)
                {
                    temperatureIndicator[i].sprite = rGreenSprite;
                }
                else if (i == 2)
                {
                    temperatureIndicator[i].sprite = lYellowSprite;
                }
                else if (i == 3)
                {
                    temperatureIndicator[i].sprite = rYellowSprite;
                }
                else if (i % 2 == 0)
                {
                    temperatureIndicator[i].sprite = lRedSprite;
                }
                else
                {
                    temperatureIndicator[i].sprite = rRedSprite;
                }
            } else
            {
                if (i % 2 == 0)
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
