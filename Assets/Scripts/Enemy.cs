using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int damage;

    public float StunDuration = 2.0f;
    private bool _isStunned = false;

    private int _layerStunned;
    private int _layerMine;

    public Vector2 KnockBackForceMinMax;

    private void Awake()
    {
        //_layerMine = gameObject.layer;
        _layerStunned = LayerMask.NameToLayer("Stunned");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Damage!");
            //Application.LoadLevel(Application.loadedLevel);
            collision.gameObject.GetComponent<PlayerMovementController>().Bounce(Random.Range(KnockBackForceMinMax.x, KnockBackForceMinMax.y), BounceKnockback.Knockback);
            collision.GetComponent<PlayerHealthManager>().ChangeHealht(-damage);
        }
    }


    /*
    private void ChangeLayer(int newLayer)
    {
        gameObject.layer = newLayer;

        //prolazimo kroz djecu objekta i radimo isto
        foreach (Transform child in transform)
        {
            child.gameObject.layer = newLayer;
        }
    }

    public void ToggleStun(bool newIsStunned)
    {
        if (newIsStunned == _isStunned)
            return;

        _isStunned = newIsStunned;
        GetComponentInParent<EagleMover>().IsStunned = _isStunned;

        if (_isStunned)
        {
            ChangeLayer(_layerStunned);
            Invoke("DisableStun", StunDuration);
        }
    }

    private void DisableStun()
    {
        ChangeLayer(_layerMine);
        ToggleStun(false);
    }
    */
}
