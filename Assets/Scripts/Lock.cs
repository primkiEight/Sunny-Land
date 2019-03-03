using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

    //KeyColor enumeratoru možemo pristupiti
    //jer je definiran unutar Key skripte izvan Key klase
    public KeyColor KeyTypeToUnlock = KeyColor.Red;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        //ovdje na bravu dodajemo slušača OnKeyPickedUp eventa
        //definiranog unutar Key skripte, ključa (mogli smo i tamo definirati slušača)
        //gdje je OnKeyPickedUp dogačaj definiran kao static baš da bi ovdje mogli definirati slušača
        //a metoda koju pozivamo će moći primiti parametar keyType poslanog iz Key skripte
        //jer smo event definirali tako da može poslati parametar tipa KeyColor kod poziva Unity Eventa
        Key.OnKeyPickedUp.AddListener(Unlock);
    }

    //može primiti keyType parametar jer je event tako definiran
    //inače, Unity Event po defaultu ne šalje parametre
    //pa bi metoda koju pozivamo morala biti bez parametara
    private void Unlock(KeyColor keyType)
    {
        if (keyType != KeyTypeToUnlock)
            return;

        //mijenjamo prozirnost na Sprite renderer
        //ne možemo alfu mijenjati direktno, nego ćemo
        //prvo kreirati novu varijablu i dodijeliti joj sadašnju vrijednost boje
        Color color = _spriteRenderer.color;
        //zatim ćemo toj color varijabli promijeniti alfu (na pola vrijednosti)
        color.a = 0.5f;
        //i zatim spriteRenderer parametru color dodijeljujemo vrijednost te boje
        _spriteRenderer.color = color;

        //objektu gasimo collider da bi postao prohodan na sceni
        _boxCollider2D.enabled = false;
    }
}
