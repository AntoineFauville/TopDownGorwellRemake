using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [Inject] private PlayerController _playerController;
    [Inject] private GameSettings _gameSettings;
    [Inject] private SceneController _sceneController;

    void Update()
    {
        if (_sceneController.GetActiveSceneIndex() == 0)
        {
            Vector3 newPosition = new Vector3(_playerController.gameObject.transform.position.x, _playerController.gameObject.transform.position.y, -10);

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * _gameSettings.CameraSmoothSpeedInFreeMode);
            transform.rotation = Quaternion.Slerp(transform.rotation, _playerController.gameObject.transform.rotation, Time.deltaTime * _gameSettings.CameraSmoothSpeedInFreeMode);
        }
    }
}
