using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Camera Cam;
    public Vector2 ShakeAmountMinMax = Vector2.zero;
    private float _shakeAmount = 0.3f;
    private float _shakeLenght = 0.3f;
    public Vector2 ShakeLenghtMinMax = Vector2.zero;

    private void Awake()
    {
        if (Cam == null)
            Cam = this.GetComponent<Camera>();
        //odnosno, dodati MainCameru preko inspektora

        //Pokušaj eventa
        StompCheck.OnEnemyStomp.AddListener(Shake);
    }

    public void Shake()
    {
        Debug.Log("ShakeShake");

        _shakeAmount = Random.Range(ShakeAmountMinMax.x, ShakeAmountMinMax.y);
        _shakeLenght = Random.Range(ShakeLenghtMinMax.x, ShakeLenghtMinMax.y);

        InvokeRepeating("DoShake", 0.0f, 0.05f);
        Invoke("StopShake", _shakeLenght);
    }

    void DoShake()
    {
        if(_shakeAmount > 0)
        {
            //kreiramo poziciju i u nju spremimo trenutnu poziciju kamere
            Vector3 camPosition = Cam.transform.position;

            //kreiramo offsete za shake
            float offsetX = Random.value * _shakeAmount * 2 - _shakeAmount;
            float offsetY = Random.value * _shakeAmount * 2 - _shakeAmount;

            //na vrijednosti trenutne pozicije dodamo kreirane offsete
            camPosition.x += offsetX;
            camPosition.y += offsetY;

            //damo kameri našu novu poziciju s offestima
            Cam.transform.position = camPosition;
        }
    }

    void StopShake()
    {
        CancelInvoke("DoShake");

        /*Unutar hijerarhije napraviti prazan GameObject, imenovat ga Camera
         * Postaviti ga na poziciju gdje nam je sada MainCamera
         * Na novi objekt dodati skriptu CameraController, a maknuti s MainCamera
         * Na sada child objektu postaviti poziciju 0, 0, 0 u odnosu na parent novi objekt
         * Dodati ovu skriptu MainCameri
         * */

        Cam.transform.localPosition = Vector3.zero;
    }
}
