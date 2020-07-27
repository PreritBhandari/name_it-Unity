using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script is attached in Root of the Home Scene!

public class NameITGameManager : MonoBehaviour
{
    public Button PlayButton;

    public static NameITGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Warning: multiple instances of " + this.name + " in scene!");
        }
    }

    private void Start()
    {
        //Initialize Play Button Click:
        if (PlayButton != null)
        {
            Button startButton = PlayButton.GetComponent<Button>();
            startButton.onClick.AddListener(OpenLevelsScene);
        }

        GameDataManager.Instance.Init();
    }

    private void OpenLevelsScene()
    {
        SceneManager.LoadScene("LevelsScene");
    }
}
