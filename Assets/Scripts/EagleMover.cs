using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMover : MonoBehaviour {

    public Transform Enemy;
    public float MovementSpeed;
    public float AttackSpeed;
    private Vector3 _playerPosition;
    //public bool IsStunned = false;

    public bool Attack;

    public List<Transform> Waypoints;
    private int _waypointIndex = 0;

    private Animator _enemyAnimator;

    private void Awake()
    {
        _enemyAnimator = Enemy.GetComponent<Animator>();
        Attack = false;
        _playerPosition = Vector3.zero;
    }

    private void Update()
    {
        if (!Attack)
        {
            //if (!IsStunned && Waypoints.Count != 0)
            if (Waypoints.Count != 0)
                {
                    Move();
                    //nije dobro rješenje pozivati stalno tokom Update-a
                    _enemyAnimator.SetBool("IsMoving", true);
            }

            //nije dobro rješenje pozivati stalno tokom Update-a
            //_enemyAnimator.SetBool("IsStunned", IsStunned);
        }
        else
        {
            AttackPlayer();
        }
    }

    private void Flip()
    {
        Vector3 localScale = Enemy.localScale;
        localScale.x *= -1.0f;
        Enemy.localScale = localScale;
    }

    private void Move()
    {
        Enemy.position = Vector3.MoveTowards(Enemy.position, Waypoints[_waypointIndex].position, MovementSpeed * Time.deltaTime);

        if (Vector3.Distance(Enemy.position, Waypoints[_waypointIndex].position) <= 0.01f)
        {
            _waypointIndex = (_waypointIndex + 1) % Waypoints.Count; //modulo
            Flip();
        }
    }

    public void AttackPlayer()
    {
        Enemy.position = Vector3.MoveTowards(Enemy.position, _playerPosition, AttackSpeed * Time.deltaTime);

        if (Vector3.Distance(Enemy.position, _playerPosition) <= 0.01f)
        {
            Attack = false;
        }
    }

    public void SetPlayerAttackPosition(Transform playerPosition)
    {
        _playerPosition = playerPosition.position;
        //okrenuti orla
        Attack = true;        
    }

    public void ResetMovingPosition()
    {
        Attack = false;
    }
}
