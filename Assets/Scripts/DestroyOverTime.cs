using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

    public float TimeToDestroy = 1.5f;

    private void Awake()
    {
        Destroy(gameObject, TimeToDestroy);
    }
}
