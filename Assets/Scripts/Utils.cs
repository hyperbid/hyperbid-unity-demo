using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utils
{
    static readonly string _detailsPanelName = "detailsHyperTxt/ScrollView/Viewport/Content/Text";
    static readonly int _checkInterval = 3000;
    
    private static Text         _uiText;
    private static Stopwatch    _stopwatch = new Stopwatch();

    static public void SetText(string text)
    {
       RunOnUiThread.AddJob(() =>
            {
                _uiText = GameObject.Find(_detailsPanelName).GetComponent<Text>();

                if (_uiText)
                    _uiText.text = text;
            }
        );
    }

    static public bool ShouldCheckConnection()
    {
        if (!_stopwatch.IsRunning)
            _stopwatch.Start();

        if(_stopwatch.ElapsedMilliseconds >= _checkInterval)
        {
            _stopwatch.Reset();
            return true;
        }

        return false;
    }
}

