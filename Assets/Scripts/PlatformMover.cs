using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {

    public bool ShouldLoop;
    private bool _shouldMove = true;

    public GameObject PlatformPrefab;
    private Transform _platformToMove;
    public float MovementSpeed;

    public float WaitAtWapointTime = 2.0f;
    private float _timer = 0.0f;
    
    public List<Transform> Waypoints;
    private int _waypointIndex = 0;

    private void Awake()
    {
        _platformToMove = Instantiate(PlatformPrefab, transform.position, Quaternion.identity, transform).transform; //varijabla nam je Transform
        _platformToMove.tag = "MovingPlatform"; //Za slučaj da nemamo zaseban prefab za moving platformu nego koristimo tipičnu platformu
    }

    //TODO: Dodati sklopku za aktivaciju moving platforme
    private void Update()
    {
        if ((Waypoints.Count != 0) && _shouldMove)
        {
            MovePlatrofm();
        }
    }

    private void MovePlatrofm()
    {
        //je li sada proteklo vremena od ranije uhvaćenog vremena (trenutno plus vrijeme koliko platforma treba čekati)
        if (Time.time >= _timer)
        {
            _platformToMove.position = Vector3.MoveTowards(_platformToMove.position, Waypoints[_waypointIndex].position, MovementSpeed * Time.deltaTime);

            if (Vector3.Distance(_platformToMove.position, Waypoints[_waypointIndex].position) <= 0.1f)
            {
                _waypointIndex = (_waypointIndex + 1) % Waypoints.Count; //modulo
                //zabilježi da je sljedeće vrijeme za nastavak kretanja trenutno vrijeme plus vrijeme koliko platforma treba čekati na mjestu
                _timer = Time.time + WaitAtWapointTime;

                if (!ShouldLoop && _waypointIndex == 0)
                    _shouldMove = false;
            }
        }   
    }
}
