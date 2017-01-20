using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Range(0.5f, 1)]
    public float _safeTopArea;

    [Range(0.5f, 1)]
    public float _safeBotArea;

    [Header("Go To Center Img")]
    public float _timeToCenterCamera;

    private Transform _player;
    private Rigidbody _playerRigidbody;
    private Camera _camera;
    private Vector3 _viewportPlayerPos;

    private float _playerOffsetY;
    bool _goingToCenter = true;
    float _timeStamp;


    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _camera = gameObject.GetComponent<Camera>();
    }

    public void LateUpdate()
    {
        _viewportPlayerPos = _camera.WorldToViewportPoint(_player.position);
        if (_playerRigidbody.velocity.y < 0 && _viewportPlayerPos.y < (1 - _safeBotArea))
        {
            if(_playerOffsetY == 0)
                _playerOffsetY = _camera.transform.position.y - _player.position.y;
            FollowPlayer();
        } else if(_playerRigidbody.velocity.y > 0 && _viewportPlayerPos.y > _safeTopArea)
        {
            if (_playerOffsetY == 0)
                _playerOffsetY = _camera.transform.position.y - _player.position.y;
            FollowPlayer();
        }
        else
        {
            _playerOffsetY = 0;
            if (Mathf.Abs(_playerRigidbody.velocity.y) < 0.1f && _viewportPlayerPos.y > 0.4f)
            {
                GoToCenter();
            }
        }
        
    } // Update

    private void FollowPlayer()
    {
        _goingToCenter = false;
        _camera.transform.position = new Vector3(_camera.transform.position.x,
            _player.position.y + _playerOffsetY, _camera.transform.position.z);
    } // FollowPlayer

    private void GoToCenter()
    {
        if (!_goingToCenter)
        {
            _goingToCenter = true;
            _timeStamp = 0;
        }
        _timeStamp += Time.deltaTime;
        _camera.transform.position = new Vector3(_camera.transform.position.x,
            Mathf.Lerp(_camera.transform.position.y, _player.position.y, _timeStamp/_timeToCenterCamera), _camera.transform.position.z);
    }
}
