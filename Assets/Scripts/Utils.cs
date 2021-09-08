using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utils
{
    static readonly string detailsPanelName = "detailsHyperTxt/ScrollView/Viewport/Content/Text";

    private static Text _uiText;

    static public void SetText(string text)
    {
       RunOnUiThread.AddJob(() =>
            {
                _uiText = GameObject.Find(detailsPanelName).GetComponent<Text>();

                if (_uiText)
                    _uiText.text = text;
            }
        );
    }
}

