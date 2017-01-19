using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControler : MonoBehaviour {

    [Header("Wind Properties")]
    public WindProperties _windProperties;

    [Header("Wind Status")]
    private Vector3 _windDirection;
    private float _timeStamp;
    private bool _waiting = true;
    public bool _start;

    private Vector3 _windBlowing;

    public List<WindAffected> _canBeBlowed;

    // Use this for initialization
    void Start() {
        _canBeBlowed = new List<WindAffected>();

        int layerAffectess = LayerMask.NameToLayer("Affectees");
        var allAffectessObjs = FindObjectsOfType<WindAffected>();
        for (int i = allAffectessObjs.Length- 1; i >= 0; --i)
        {
            if (allAffectessObjs[i].gameObject.layer == layerAffectess)
                _canBeBlowed.Add(allAffectessObjs[i]);
        }
    } // Start

    // Update is called once per frame
    void Update() {
        if (!_start)
            return;

        _timeStamp += Time.deltaTime;
        if (_waiting)
        {
            UpdateWaiting();
        }
        else
        {
            UpdateBlowing();
        }
    } // Update

    private void UpdateWaiting()
    {
        if (_timeStamp > _windProperties.timeBetweenWaves)
        {
            ChangeToBlowing();
        }
    } // updateWaiting

    private void UpdateBlowing()
    {
        _windBlowing = _windProperties.maxStrength * _windDirection;
        _windBlowing *= _windProperties.strenghtCurve.Evaluate(_timeStamp / _windProperties.timeBlowing);

        for (int i = _canBeBlowed.Count - 1; i >= 0; i--)
        {

            _canBeBlowed[i].AddForce(_windBlowing);
        }

        if (_timeStamp > _windProperties.timeBlowing) {
            ChangeToWaiting();
        }
    } // updateBlowing

    private void ChangeToBlowing()
    {
        _timeStamp = 0;
        _waiting = false;

        _windDirection = Vector3.one * (UnityEngine.Random.value > 0.5? -1: 1);

        Debug.Log("blowing to "+(_windDirection.x >= 1?"right":"left"));
    } // changeToBlowing

    private void ChangeToWaiting()
    {
        _timeStamp = 0;
        _waiting = true;
        Debug.Log("waiting");
    } // changeToWaiting
}
