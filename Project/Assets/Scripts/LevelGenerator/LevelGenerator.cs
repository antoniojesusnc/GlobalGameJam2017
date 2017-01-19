using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject _platformPrefab;
    public GameObject _obstaclePrefab;

    [Header("Prefabs")]
    public GameObject _firstFloor;

    [Header("Properties")]
    public int _numberElements;
    public float _minHorizontalDistance;
    public float _maxHorizontalDistance;

    public float _minVerticalDistance;
    public float _maxVerticalDistance;

    public float _minPlatformSize;
    public float _maxPlatformSize;

    [Header("OffsetToGenerateNewPlatrform")]
    public float _offsetToGenerateNewPlatrform;
    // private vars

    private List<GameObject> _platforms;
    private int _platformIndex;
    private List<GameObject> _obstacles;
    private int _obstacleIndex;

    private Transform _lastPlatformGenerated;
    private Camera _camera;

    // Use this for initialization
    void Start () {

        _camera = Camera.main;

        _lastPlatformGenerated = _firstFloor.transform;

        Vector3 initialPos = -Vector3.up * -1000;
        GameObject temp;
        _platforms = new List<GameObject>();
        _obstacles = new List<GameObject>();
        for (int i = 0; i < _numberElements; i++)
        {
            temp = Instantiate(_platformPrefab, transform);
            _platforms.Add(temp);
            temp.name = "Platform " + i.ToString("00");
            temp.transform.position = initialPos;
            temp = Instantiate(_obstaclePrefab, transform);
            _obstacles.Add(temp);
            temp.name = "Obstacle " + i.ToString("00");
            temp.transform.position = initialPos;

            setPlatform();
        }

    } // Start

    private void setPlatform()
    {
        Transform newPosPlatform = _platforms[_platformIndex].transform;
        float newPosX = getValueCheckingDifficulty(_minHorizontalDistance, _maxHorizontalDistance);
        float newPosy =  getValueCheckingDifficulty(_minVerticalDistance, _maxVerticalDistance);
        Vector3 newPos;
        if (_lastPlatformGenerated.position.x < 0)
        {
            newPos = _lastPlatformGenerated.position + new Vector3(newPosX, newPosy,0);
        }else
        {
            newPos = _lastPlatformGenerated.position + new Vector3(-newPosX, newPosy, 0);
        }
        newPosPlatform.position = newPos;

        newPosPlatform.localScale = new Vector3(getValueCheckingDifficulty(_minPlatformSize, _maxPlatformSize),
            newPosPlatform.localScale.y, newPosPlatform.localScale.z);
        _platformIndex = (_platformIndex + 1) % _numberElements;

        _lastPlatformGenerated = newPosPlatform;
    } // setPlatform

    private float getValueCheckingDifficulty(float easyNumber, float hardNumber)
    {
        //after will be a number between 0 and 1
        float difficultRange = 0;
        float averageNumber = (easyNumber + hardNumber) * 0.5f;
        return Random.Range(Mathf.Lerp(easyNumber, averageNumber, difficultRange),
            Mathf.Lerp(averageNumber,hardNumber, difficultRange));

        // if we have this  ----------------
        // with value 0, the range is  |--------|--------
        // if value is 0.5, the range is ----|-------|-----
        // and with value 1, the range is --------|--------|
    } // getValueCheckingDifficulty

    public void Update()
    {
        Vector3 downCameraPos = _camera.ViewportToWorldPoint(Vector3.zero);
        if((_platforms[_platformIndex].transform.position.y +_offsetToGenerateNewPlatrform)< downCameraPos.y)
        {
            setPlatform();
        }
    } // Update
}
