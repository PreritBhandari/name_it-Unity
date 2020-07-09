using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button StartButton;

    public Question[] Questions;
    // Start is called before the first frame update
    void Start()
    {
        StartButton?.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("You have clicked the button!");
        SceneManager.LoadScene("GameScene");
    }
}
