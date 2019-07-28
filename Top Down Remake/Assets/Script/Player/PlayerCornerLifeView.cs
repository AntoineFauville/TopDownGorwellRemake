using UnityEngine;
using UnityEngine.UI;

public class PlayerCornerLifeView : MonoBehaviour
{
    public Image PlayerLifeView;

    public GameObject Panel;

    public void IsEnable(bool status)
    {
        Panel.SetActive(status);
    }
}
