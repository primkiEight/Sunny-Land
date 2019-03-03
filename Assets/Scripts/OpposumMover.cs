using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpposumMover : MonoBehaviour {

    private Rigidbody2D _myRigidBody;
    private Transform Enemy;
    private Animator _myAnimator;
    public float HorizontalSpeed;
    private float orientation;

    private void Awake()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        Enemy = transform;
        _myAnimator = GetComponent<Animator>();
        orientation = transform.localScale.x;
    }

    private void Start()
    {
        _myAnimator.SetBool("IsMoving", true);
    }

    private void Flip()
    {
        Vector3 localScale = Enemy.localScale;
        localScale.x *= -1.0f;
        orientation = localScale.x;
        Enemy.localScale = localScale;
    }

    private void Update()
    {
        _myRigidBody.velocity = new Vector2(-orientation * HorizontalSpeed, _myRigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Flip();
    }

}
