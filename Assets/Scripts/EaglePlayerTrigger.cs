using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglePlayerTrigger : MonoBehaviour {

    private EagleMover _parentEagleMover;

    private void Awake()
    {
        _parentEagleMover = GetComponentInParent<EagleMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _parentEagleMover.SetPlayerAttackPosition(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _parentEagleMover.ResetMovingPosition();
    }
}
