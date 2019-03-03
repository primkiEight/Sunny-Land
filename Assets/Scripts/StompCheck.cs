using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StompCheck : MonoBehaviour {

    public GameObject DeathExplosion;
    public GameObject MyHolder;
    public Vector2 BounceForceMinMax;

    //Pokušaj eventa
    public static UnityEvent OnEnemyStomp = new UnityEvent();
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject explosion = Instantiate(DeathExplosion, explosionPosition, Quaternion.identity, null);
            collision.gameObject.GetComponent<PlayerMovementController>().Bounce(Random.Range(BounceForceMinMax.x, BounceForceMinMax.y), BounceKnockback.Bounce);

            //Pokušaj eventa
            OnEnemyStomp.Invoke();
            Debug.Log("Shake");

            Destroy(MyHolder);
        }
    }

}
