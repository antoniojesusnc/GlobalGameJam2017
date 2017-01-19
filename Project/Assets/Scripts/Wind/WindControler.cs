using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControler : MonoBehaviour {

    AnimationCurve _strenghtCurve;
    public float _timeBetweenWave;
    public float _timeWave;

    public float _windStrength;

    private float _timeStamp;

    private bool _waiting;
    public bool _start;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!_start)
            return;

        _timeStamp += Time.deltaTime;
        if (_waiting)
        {
            updateWaiting();
        }
        else
        {
            updateBlowing();
        }
    } // Update

    private void updateWaiting()
    {
        if (_timeStamp > _timeBetweenWave)
        {
            changeToBlowing();
        }
    } // updateWaiting

    private void updateBlowing()
    {
        if(_timeStamp > _timeWave) {
            changeToWaiting();
        }
    } // updateBlowing

    private void changeToBlowing()
    {
        _timeStamp = 0;
        _waiting = false;
        Debug.Log("blowing");
    } // changeToBlowing

    private void changeToWaiting()
    {
        _timeStamp = 0;
        _waiting = true;
        Debug.Log("waiting");
    } // changeToWaiting
}
