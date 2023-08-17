using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string levelName;

    void Start()
    {
        text.text = "Electro Body Remake" +
            "\n" + levelName +
            "\n" + K.GameVersion +
            "\nadmi126n@gmail.com";
    }
}
