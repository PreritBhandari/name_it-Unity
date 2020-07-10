using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

//This script is attached in Root of the Home Scene!

public class NameITGameManager : MonoBehaviour
{
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
        GameDataManager.Instance.Init();
    }
}
