using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WindAffected : MonoBehaviour {

    private bool _hideInLeft;
    private bool _hideInRight;

    Rigidbody _rigidbody;
	
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
    } // Start

    public void SetPlayerHideInRight(bool hideInRight)
    {
        _hideInRight = hideInRight;
    } // SetPlayerHideInRight

    public void SetPlayerHideInLeft(bool hideInLeft)
    {
        _hideInLeft = hideInLeft;
    } // SetPlayerHideInLeft

    public void AddForce(Vector3 directionalForce)
    {
        if (directionalForce.x < 0 && !_hideInLeft || directionalForce.x > 0 && !_hideInRight)
        {
            _rigidbody.AddForce(directionalForce);
        }
    } // AddForce
}
