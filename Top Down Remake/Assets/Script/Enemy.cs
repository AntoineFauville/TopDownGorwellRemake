using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerController _playerController;

    public void Setup(PlayerController playerController)
    {
        _playerController = playerController;
    }

    void Update()
    {
        //transform.LookAt(_playerController.transform);
    }
}
