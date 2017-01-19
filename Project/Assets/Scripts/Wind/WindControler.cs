using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControler : MonoBehaviour {

    [Header("Wind Properties")]
    public AnimationCurve _strenghtCurve;
    public float _timeBetweenWave;
    public float _timeWave;

    public float _windMaxStrength;



    [Header("Wind Status")]
    private Vector3 _windDirection;
    private float _timeStamp;
    private bool _waiting;
    public bool _start;

    private Vector3 _windBlowing;

    public List<Rigidbody> _canBeBlowed;

    // Use this for initialization
    void Start() {
        _canBeBlowed = new List<Rigidbody>();

        int layerAffectess = LayerMask.NameToLayer("Affectees");
        var allRigidbody = FindObjectsOfType<Rigidbody>();
        for (int i = allRigidbody.Length- 1; i >= 0; --i)
        {
            if (allRigidbody[i].gameObject.layer == layerAffectess)
                _canBeBlowed.Add(allRigidbody[i]);
        }
    } // Start

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
        _windBlowing = _windMaxStrength * _windDirection;
        for (int i = _canBeBlowed.Count - 1; i >= 0; i--)
        {
            _canBeBlowed[i].AddForce(_windBlowing);
        }

        if (_timeStamp > _timeWave) {
            changeToWaiting();
        }
    } // updateBlowing

    private void changeToBlowing()
    {
        _timeStamp = 0;
        _waiting = false;

        _windDirection = Vector3.one * (UnityEngine.Random.value > 0.5? -1: 1);

        Debug.Log("blowing to "+(_windDirection.x >= 1?"right":"left"));
    } // changeToBlowing

    private void changeToWaiting()
    {
        _timeStamp = 0;
        _waiting = true;
        Debug.Log("waiting");
    } // changeToWaiting
}
