using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Player;
    public float FollowAhead;
    public float yOffset;
    public float Smoothing;
    private Transform _myTransform;
    private Transform _targetTransform;
    private Vector3 _newPosition;


    private void Awake()
    {
        _myTransform = transform;
        _targetTransform = Player;
    }

    private void Update()
    {
        if (_targetTransform.transform.localScale.x > 0f)
        {
            _newPosition = new Vector3(_targetTransform.position.x + FollowAhead,
                _targetTransform.position.y + yOffset,
                _myTransform.position.z); //z od kamere je -10, ostavljamo ga takvog
        } else
        {
            _newPosition = new Vector3(_targetTransform.position.x - FollowAhead,
                _targetTransform.position.y + yOffset,
                _myTransform.position.z); //z od kamere je -10, ostavljamo ga takvog
        }

        _myTransform.position = Vector3.Lerp(_myTransform.position, _newPosition, Smoothing * Time.deltaTime);
    }
}
