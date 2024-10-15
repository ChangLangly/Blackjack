using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Dealer : HouseMonitor
{
    public TMP_Text cardScoreText;
    public TMP_Text playerCardScoreText;
    public Sprite[] deck;
    public Image[] currentDealerCardSlot;
    public Image[] currentPlayerCardSlot;
    public int currentPlayerHitSlot = 2;
    public int currentDealerHitSlot = 1;
    public int aceValue;
    public int numValue;
    public Button deal;
    public Button hit;
    public Button stay;

    public List<Image> cards;

    // Start is called before the first frame update
    void Start()
    {
        hit.interactable = false;
        stay.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Deal()
    {
        
        ResetCardScore();
        currentDealerHitSlot = 1;
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                currentDealerCardSlot[i].sprite = deck[Random.Range(0, 51)];
            }
            else if (i == 1) 
            {
                currentDealerCardSlot[i].sprite = deck[52];
            }
            
            currentPlayerCardSlot[i].sprite = deck[Random.Range(0, 51)];   
        }
        deal.interactable = false;
        hit.interactable = true;
        stay.interactable = true;
        Invoke("CardCount", 1.2f);
    }

    public void HitIt()
    {
        ResetCardScore();
        
        currentDealerCardSlot[currentDealerHitSlot].sprite = deck[Random.Range(0, 51)];
        currentDealerHitSlot++;

        
        Invoke("CardCount", 1.2f);
    }

    public IEnumerator Hit()
    {
        while (true)
        {
            if (cardScore <= 16 && currentDealerHitSlot < 6)
            {
                HitIt();
                Debug.Log("Dealer Hitting");
            }
            if (cardScore >= 16)
            {
                Invoke("SeeWhoWon", 3);
            }
            yield return new WaitForSeconds(3);
        }
    }


    public void CardCount()
    {
        foreach (var cardSlot in currentDealerCardSlot)
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

    }

    void ResetCardScore()
    {
        cardScore = 0;
        cards.Clear();
    }

    public void isStaying()
    {
        StartCoroutine(Hit());
        stay.interactable = false;
        hit.interactable = false;
    }

    public void SeeWhoWon()
    {
        int playerScore;
        if (int.TryParse(playerCardScoreText.text, out playerScore))
        {
            Debug.Log("Player Score: " + playerScore + " Dealer Score: " + cardScore);

            if (playerScore > 21)
            {
                SceneManager.LoadScene("BustLose");
            }
            else if (cardScore > 21)
            {
                SceneManager.LoadScene("DealerBust");
            }
            else if (playerScore > cardScore)
            {
                SceneManager.LoadScene("Win");
            }
            else if (playerScore < cardScore)
            {
                SceneManager.LoadScene("Lose");
            }
            else
            {
                Debug.Log("It's a Push");
                SceneManager.LoadScene("Push");
            }
        }
        else
        {
            Debug.LogWarning("The text is not a valid integer.");
        }
    }

}


