using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerArea : MonoBehaviour
{
    public bool IsTriggeredByPlayer;

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == Tags.Player.ToString())
        {
            IsTriggeredByPlayer = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == Tags.Player.ToString())
        {
            IsTriggeredByPlayer = false;
        }
    }
}
