using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerArea : MonoBehaviour
{
    public bool IsTriggeredByPlayer;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == Tags.Player.ToString())
        {
            IsTriggeredByPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == Tags.Player.ToString())
        {
            IsTriggeredByPlayer = false;
        }
    }
}
