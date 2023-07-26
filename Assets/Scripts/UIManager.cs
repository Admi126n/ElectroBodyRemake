using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<Image> chips;

    private Color32 _emptyChip = new(255, 255, 255, 64);
    private Color32 _solidChip = new(255, 255, 255, 255);
    private int _chipCounter = 0;


    void Start()
    {
        ResetChipIndicator();
    }

    // TODO: separate class ChipIndicatorManager

    public void ResetChipIndicator()
    {
        foreach (Image chip in chips)
        {
            chip.color = _emptyChip;
        }
    }

    private void FillChipIndicator()
    {
        for (int i = 0; i < _chipCounter; i++)
        {
            chips[i].color = _solidChip;
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
            ResetChipIndicator();
            yield return new WaitForSeconds(0.3f);
            FillChipIndicator();
            yield return new WaitForSeconds(0.3f);
        }
    }

}
