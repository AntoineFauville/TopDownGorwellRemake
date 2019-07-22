using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadingScreenFactory
{
    [Inject] private GameSettings _gameSettings;

    public LoadingScreen CreateLoadingScreen()
    {
        LoadingScreen loadingScreen = Object.Instantiate(_gameSettings.LoadingScreen);

        return loadingScreen;
    }
}
