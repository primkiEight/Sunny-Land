using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    public int StartingLives;
    public int StartingHealth;
    public int TotalHealth;
    public int MaxHealth;
    private int _currentLives;
    private int _currentHealth;

    private void Start()
    {
        StartingHealth = TotalHealth;
        _currentHealth = StartingHealth;
        _currentLives = StartingLives;

        ChangeHealht(0);
        AddHearth(0);
    }

    //Dodaj/Oduzmi health
    public void ChangeHealht(int health)
    {
        //Ako je trenutni health manji ili jednak maksimalnom
        if (_currentHealth <= TotalHealth)
        {
            //Povećaj/Umanji health za iznos koji si dobio
            _currentHealth += health;
            //Ako je zbroj veći ili jednak od trenutno maksimalno mogućeg...
            if (_currentHealth >= TotalHealth)
            {
                //...onda je trenutno zdravlje na trenutnom maksimumu
                _currentHealth = TotalHealth;
            }

            //Ako je zdravlje nula ili manje, izgubi život
            if (_currentHealth <= 0)
            {
                ChangeLife(-1);
            }
        }

        //U svakom slučaju, ažuriraj canvas
        //pošalji maksimum mogući healtha i trenutni health
        GameManager.GM.UpdateHealthCanvas(TotalHealth, _currentHealth);
    }

    //Dodajem novi slot za srca, a kao parametar moram primiti vrijednost dva healtha, dakle 2
    public void AddHearth(int amount)
    {
        //Trenutni maksimum healtha povećat ću za iznos vrijednosti jednog srca (vrijednost 2)
        int hearts = TotalHealth + amount;
        //Ako ne premašujem maksimalni health...
        if (hearts < MaxHealth)
        {
            //onda mi je trenutni maksimalni health jednak novoj vrijednosti...
            TotalHealth = hearts;
        }
        //inače...
        else if (hearts >= MaxHealth)
        {
            //... je jednak maksimalnoj mogućoj vrijednosti
            TotalHealth = MaxHealth;            
        }

        //Ažuriraj heart canvas i kao maksimalni broj srca pošalji
        //pola vrijednosti trenutno maksimalnog healtha ali zaokruženo na gornju vrijednost
        GameManager.GM.UpdateHearthBarCanvas(Mathf.CeilToInt((float)TotalHealth / 2));

        //Dodat ću health na novo dodano srce, poslat ću vrijednost healtha jednog punog srca
        ChangeHealht(2);
    }
    
    public void ChangeLife(int value)
    {
        if (value > 0)
        {
            _currentLives++;
            GameManager.GM.UpdateLifeBarCanvas(true);
        }
        if (value < 0)
        {
            _currentLives--;
            GameManager.GM.UpdateLifeBarCanvas(false);
            LoseLife();
        }

        
    }

    public void LoseLife()
    {
        if (_currentLives <= 0)
        {
            //kraj igre
        }
        else if (_currentLives > 0)
        {
            //resetiraj nivo
            
        }
    }
}
