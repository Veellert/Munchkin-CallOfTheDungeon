using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Логика меню с обучением
/// </summary>
public class TutorialMenu : MenuTemplate
{
    public static bool _isFirstOpen = false;

    protected override void Start()
    {
        // Открывать только один раз
        if (_isFirstOpen)
            _activeOnLoad = false;

        base.Start();
        _isFirstOpen = true;
    }
}
