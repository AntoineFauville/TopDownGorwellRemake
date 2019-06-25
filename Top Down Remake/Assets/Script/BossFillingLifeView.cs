using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFillingLifeView : MonoBehaviour
{
    public Image BossLife;

    public GameObject Panel;

    public void IsEnable(bool status)
    {
        Panel.SetActive(status);
    }
}
