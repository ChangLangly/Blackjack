using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : HouseMonitor
{
    public TMP_Text cardScoreText;
    public Sprite[] deck;
    public Image[] currentDealerCardSlot;
    public Image[] currentPlayerCardSlot;
    public int currentPlayerHitSlot = 2;
    public int aceValue;
    public int numValue;

    public bool bust;
    public Button hit;
    public List<Image> cards;
    // Start is called before the first frame update
    void Start()
    {
        bust = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bust)
        {
            SceneManager.LoadScene("BustLose");
        }

        if (currentPlayerHitSlot > 6)
        {
            hit.interactable = false;
        }
    }

    public void Dealt()
    {
        ResetCardScore();
        for (int i = 0; i < 2; i++)
        {
            currentPlayerCardSlot[i].sprite = deck[Random.Range(0, 51)];  
        }
        
        Invoke("CardCount", 1.2f);
    }

    public void Hit()
    {
        currentPlayerCardSlot[currentPlayerHitSlot].sprite = deck[Random.Range(0, 51)];
        ResetCardScore();
        currentPlayerHitSlot++;
        
        Invoke("CardCount", 1.2f);
    }
    public void CardCount()
    {
        foreach (var cardSlot in currentPlayerCardSlot)
        {
            if (cardSlot.sprite != null)
            {
                cards.Add(cardSlot);
            }
        }

        int aceStartIndex = 0;
        int aceEndIndex = 3;
        int faceStartIndex = 4;
        int faceEndIndex = 15;
        int numStartIndex = 16;
        int numEndIndex = 51;

        for (int i = aceStartIndex; i <= aceEndIndex; i++)
        {
            if (cardScore <= 10)
            {
                aceValue = 11;
            }
            else if (cardScore > 10)
            { 
                aceValue = 1;
            }

            foreach (Image card in cards)
            {
                if (card.sprite == deck[i])
                {
                    CardScore += aceValue;
                }
            }
        }


        for (int i = faceStartIndex; i <= faceEndIndex; i++)
        {
            foreach (Image card in cards)
            {
                if (card.sprite == deck[i])
                {
                    CardScore += 10;
                }
            }
        }

        for (int i = numStartIndex; i <= numEndIndex; i++)
        {
            if (i == 16 || i == 25 || i == 34 || i == 43)
            {
                numValue = 10;
            }

            else if (i == 17 || i == 26 || i == 35 || i == 44)
            {
                numValue = 9;
            }

            else if (i == 18 || i == 27 || i == 36 || i == 45)
            {
                numValue = 8;
            }

            else if (i == 19 || i == 28 || i == 37 || i == 46)
            {
                numValue = 7;
            }

            else if (i == 20 || i == 29 || i == 38 || i == 47)
            {
                numValue = 6;
            }

            else if (i == 21 || i == 30 || i == 39 || i == 48)
            {
                numValue = 5;
            }

            else if (i == 22 || i == 31 || i == 40 || i == 49)
            {
                numValue = 4;
            }

            else if (i == 23 || i == 32 || i == 41 || i == 50)
            {
                numValue = 3;
            }

            else if (i == 24 || i == 33 || i == 42 || i == 51)
            {
                numValue = 2;
            }

            foreach (Image card in cards)
            {
                if (card.sprite == deck[i])
                {
                    CardScore += numValue;
                }
            }
        }

        

        cardScoreText.text = cardScore.ToString();
        if (cardScore >= 22)
        {
            bust = true;
        }

    }

    void ResetCardScore()
    {
        cardScore = 0;
        cards.Clear();
    }


}
