using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayerArchetype : MonoBehaviour
{
    private PlayerArchetypeController _playerArchetypeController;

    private bool _hasThisSwitchBeenUsed;

    public void Setup()
    {
        _playerArchetypeController = GameObject.Find("PlayerArchetypeController(Clone)").GetComponent<PlayerArchetypeController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !_hasThisSwitchBeenUsed)
        {
            _hasThisSwitchBeenUsed = true;

            _playerArchetypeController.SwitchClassRandom();
        }
    }
}
