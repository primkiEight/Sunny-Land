using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHearth : MonoBehaviour {

    public int value;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //kada dodajem value za jedno srce, value mora vrijediti dva healtha, dakle 2
            collision.GetComponent<PlayerHealthManager>().AddHearth(value);
            Destroy(gameObject);
        }
    }
}
