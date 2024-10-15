using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadBlack()
    {
        SceneManager.LoadScene("BlackJack");
    }

    public void LoadBust()
    {
        SceneManager.LoadScene("BustLose");
    }

    public void LoadDBust()
    {
        SceneManager.LoadScene("DealerBust");
    }

    public void LoadLose()
    {
        SceneManager.LoadScene("Lose");
    }

    public void LoadWin()
    {
        SceneManager.LoadScene("Win");
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene("Help");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
