using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private Animator _animation;

    private void Awake()
    {
        _animation = GetComponent<Animator>();
        _animation.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //pusti zvuk
            _animation.enabled = true;
            //aktiviraj CheckPoint
        }
    }
}
