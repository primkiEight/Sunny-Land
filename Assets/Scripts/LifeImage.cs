using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Da bi mogli koristiti editor funkcionalnosti
//moramo uključiti Library UnityEditor
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LifeImage : MonoBehaviour {

    public Image Life; //preko inspektora postavimo (dovučemo) Image komponentu s objekta na polje varijable Life

    public void SetActive(bool value)
    {
        Life.enabled = value;
    }
}

//Ovako sa #if i #endif definiramo da se ovaj dio skripte
//ne koristi prilikom builda ili igranja, već samo za edit
#if UNITY_EDITOR
//Omogućujemo editiranje selektiranih više objekata zajedno
//iako, ne radi ovako s gumbima, već tek vidimo inspektor za multiple selektirane objekte
[CanEditMultipleObjects]
//Preko atributa definiramo custom editor za tip komponente (naše skripte) LifeImage
[CustomEditor(typeof(LifeImage))]
//Kreiramo našu klasu koja nasljeđuje Editor klasu
public class LifeImageEditor : Editor {

    //Metoda za promjene na inspektoru i prikazivanju inspektora
    public override void OnInspectorGUI()
    {
        //Pozivamo metodu (defaultne klase Editor) koja omogućuje da se i dalje vidi redovni inspektor
        base.OnInspectorGUI(); //ili DrawDefaultInspector();

        //Dohvaćamo objekt koji je trenutno selektiran
        LifeImage lifeImage = (LifeImage)target;

        //Kreiramo button s natpisom Active i provjeravamo je li stisnut
        if (GUILayout.Button("Active"))
            lifeImage.SetActive(true); //pokrećemo metodu SetActive dohvaćenog selektiranog objekta

        //Kreiramo novi button s natpisom Deactivate i provjeravamo je li stisnut
        if (GUILayout.Button("Deactivate"))
            lifeImage.SetActive(false);
    }
}
#endif