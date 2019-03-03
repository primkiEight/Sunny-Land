using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events; //da bi mogli definirati Event

//definiranjem ove varijable tipa enumerator izvan klase Key
//omogućujemo da mu možemo pristupiti i iz drugih skripti/klasa
public enum KeyColor
{
    Red,
    Green,
    Blue
}

//da bi mogli prenijeti informaciju o pokupljenom ključu kod pozivanja eventa kao parametar do slušača
//napravit ćemo novu proizvoljnu klasu koja nasljeđuje UnityEvent definiran s jednim parametrom
//KeyColor tipa kojeg želimo proslijediti
public class KeyPickedUpEvent : UnityEvent<KeyColor> { }
//ovo nam ne treba ako ćemo imati Unity Evente koji neće prenositi 
//a slušač primati ikakve parametre
//kreirali smo vlastitu klasu koja postaje jedan ovakav događaj
//i onda nju koristimo za naše potrebe komunikacije sa slušačem kojemu prenosimo,
//a on prima neki parametar

public class Key : MonoBehaviour {

    /*
    //Ovaj dio koda koristimo i na ovaj način definiramo parove
    //boja ključa iz enumeratora i određeni sprite iz asseta
    //od kojih onda napravimo listu parova koju kroz inspektor posložimo
    //uparivanjem enumerator vrijednosti i spriteva:
    [System.Serializable]
    public class KeyTypeSprite
    {
        public KeyColor KeyType;
        public Sprite KeySprite;
    }
    public List<KeyTypeSprite> KeyTypeSpritePairs;
    //isto bi trebalo definirati i na lockovima
    */


    public KeyColor KeyType = KeyColor.Red; //red je neka defaultna vrijednost

    //definiranje public static Unity događaja
    //static zato da brave (locks) ne moraju imati reference na svoje ključeve,
    //već će svaka brava znati da svaki ključ može trigerirati ovaj događaj
    //tj. da svaka brava može biti slušač ovog događaja
    //dobra je praksa događaje nazvati sa On...što se događa

    //na ovaj način definiramo unity event ako mu NEprosljeđujemo nikakav parametar:
    //public static UnityEvent OnKeyPickedUp = new UnityEvent();

    //da bi mogli prenijeti KeyColor parametar do slušača, kreiramo gornju proizvoljnu klasu
    //KeyPickedUpEvent koja nasljeđuje UnityEvent i prima/prenosi jedan parametar
    //kao novi tip eventa na ovaj način:
    public static KeyPickedUpEvent OnKeyPickedUp = new KeyPickedUpEvent();

    /*
    //Ovaj dio koda koristimo i na ovaj način definiramo par
    //boje ključa iz enumeratora koji kroz inspektor postavimo
    //s odgovarajućim spriteom koji smo uparili s tom enumerator bojom
    //iz liste parova definiranih klasom KeyTypeSPrite koju smo definirali gore
    private void Awake()
    {
        //za svaki element iz liste
        //provjeri odgovara li enumerator boja na ovom objektu s bojom u listi
        //i ako odgovara
        //promjeni sprite ovog objekta prema informaciji o spriteu s kojim je ta boja uparena
        SpriteRenderer mySpriteRenderer = GetComponent<SpriteRenderer>();
        foreach (KeyTypeSprite keyTypeSprite in KeyTypeSpritePairs)
        {
            if (KeyType == keyTypeSprite.KeyType)
                mySpriteRenderer.sprite = keyTypeSprite.KeySprite;
        }
    }
    //isto bi trebalo definirati i na lockovima
    */


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Key picked up: " + KeyType);

            //događaji se pokreću Invoke metodom
            //na ovaka način ako ne prenosimo nikakav parametar:
            //OnKeyPickedUp.Invoke();

            //odnosno na ovakav način ako ga definiramo da mođe prenositi neki parametar
            OnKeyPickedUp.Invoke(KeyType);

            Destroy(gameObject);
        }
    }
}
