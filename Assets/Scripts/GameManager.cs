using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager GM;

    public GameObject GameCanvas;
    public Sprite HearthFull;
    public Sprite HearthHalf;
    public Sprite HearthEmpty;
    public List<Image> HearthSlots;
    public GameObject LifeGrid;
    public LifeImage LifeImagePrefab;
    public List<LifeImage> LifeImagesList;

    private void Awake()
    {
        GM = this;
    }

    //Dodajem novi slot za srca, a kao parametar ću primiti
    //zaokruženu vrijednost (ne gore) polovice trenutne maksimalne vrijednosti healtha
    public void UpdateHearthBarCanvas(int numberOfHearts)
    {
        //Ako broj nije veći od maksimalno mogućeg
        if (numberOfHearts <= HearthSlots.Count)
        {
            //Prikaži i otključaj sve od novog broja srca
            for (int i = 0; i < numberOfHearts; i++)
            {
                HearthSlots[i].enabled = true;
            }

            //Sakrij i zaključaj sve od novog do maksimalnog broja slotova za srca
            for (int i = numberOfHearts; i < HearthSlots.Count; i++)
            {
                HearthSlots[i].enabled = false;
            }
        }        
    }

    //Ažuriraj canvas i primi trenutni mogući maksimum i trenutni health
    public void UpdateHealthCanvas(int totalHealth, int currentHealth)
    {
        //Ako je trenutni health djeljiv s dva, srca trenutnog healtha su puna
        if (currentHealth%2 == 0)
        {
            //Jedno srce vrijedi dva healtha, dakle duplo manje srca popuni
            for (int i = 0; i < (currentHealth / 2); i++)
            {
                HearthSlots[i].sprite = HearthFull;
            }
            //sve što je iznad trenutnog healtha, srca prikaži prazna
            for (int i = (currentHealth / 2); i < (totalHealth / 2); i++)
            {
                HearthSlots[i].sprite = HearthEmpty;
            }
        }//Ako trenutni health nije djeljiv s dva, srce gdje je trenutni health je na pola puno
        else if (currentHealth%2 == 1)
        {
            //Jedno srce vrijedi dva healtha, dakle duplo manje srca popuni
            for (int i = 0; i < (currentHealth / 2); i++)
            {
                HearthSlots[i].sprite = HearthFull;
            }
            //Ono srce gdje je trenutni health je na pola puno
            HearthSlots[currentHealth / 2].sprite = HearthHalf;
            //Sve što je iznad trenutnog healtha, srca prikaži prazna
            for (int i = (currentHealth / 2) + 1; i < (totalHealth / 2); i++)
            {
                HearthSlots[i].sprite = HearthEmpty;
            }
        }
    }

    public void UpdateLifeBarCanvas(bool addLife)
    {
        if (addLife)
        {
            LifeImage lifeImageClone = Instantiate(LifeImagePrefab, LifeGrid.transform);
            lifeImageClone.SetActive(true);
            LifeImagesList.Add(lifeImageClone);
        }

        if (!addLife)
        {
            int lastLife = LifeImagesList.Count - 1;
            
            //_lifeImages[lastLife].SetActive(false);
            if (lastLife >= 0)
            {
                Destroy(LifeImagesList[lastLife].gameObject);
                LifeImagesList.RemoveAt(lastLife);
            }
                
        }        
    }
}
