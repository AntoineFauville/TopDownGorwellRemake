using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingController : MonoBehaviour
{
    public void SetPlayerPrefInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public int GetPlayerPrefInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
}
