using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAvoidTrigger : MonoBehaviour {

    public enum ETriggerPos {
    RIGHT_TRIGGER,
    LEFT_TRIGGER,
    }

    public ETriggerPos _triggerPos;

	public void OnTriggerEnter(Collider enterCollider)
    {
        if(_triggerPos == ETriggerPos.LEFT_TRIGGER)
        {
            enterCollider.GetComponentInChildren<WindAffected>().SetPlayerHideInLeft(true);
        }
        else
        {
            enterCollider.GetComponentInChildren<WindAffected>().SetPlayerHideInRight(true);
        }
    }

    public void OnTriggerExit(Collider enterCollider)
    {
        if (_triggerPos == ETriggerPos.LEFT_TRIGGER)
        {
            enterCollider.GetComponentInChildren<WindAffected>().SetPlayerHideInLeft(false);
        }
        else
        {
            enterCollider.GetComponentInChildren<WindAffected>().SetPlayerHideInRight(false);
        }
    }
}
