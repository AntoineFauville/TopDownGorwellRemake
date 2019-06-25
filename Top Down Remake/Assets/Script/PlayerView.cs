using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public Image PlayerLifeView;

    public GameObject Panel;

    public void IsEnable(bool status)
    {
        Panel.SetActive(status);
    }
}
