using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

//This script is attached in Root of the Home Scene!

public class GameDataManager : MonoBehaviour
{
    public bool DeleteExistingData = false;

    public static GameDataManager Instance { get; private set; }

    private string _dataFilePath => Application.persistentDataPath + "/gamedata.json";

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

    public void Init()
    {
        if (DeleteExistingData)
        {
            if (File.Exists(_dataFilePath))
                File.Delete(_dataFilePath);
        }

        var existingDataFile = GetOfflineData();

        if (existingDataFile != null)
        {
            OfflineData migratedDataFile = StartMigrationExistingData(existingDataFile);
            if (migratedDataFile != null)
            {
                SaveDataFile(migratedDataFile);
            }
        }
        else
        {
            OfflineData newDataFile = GetCurrentLevelsDataShippedWithGame();
            if (newDataFile != null)
            {
                SaveDataFile(newDataFile);
            }
        }
    }

    private OfflineData StartMigrationExistingData(OfflineData existingDataFile)
    {
        OfflineData newData = GetCurrentLevelsDataShippedWithGame();

        newData.PlayerInfo.PlayerName = existingDataFile.PlayerInfo.PlayerName;
        newData.PlayerInfo.InstalledOn = existingDataFile.PlayerInfo.InstalledOn;

        List<Level> ExistingUserPlayedLevels = existingDataFile.GameLevels.Where(f => f.Score >= 0).ToList();

        foreach(var level in ExistingUserPlayedLevels)
        {
            var sameLevelInNewData = newData.GameLevels.Where(f => f.Question == level.Question && f.Category == level.Category).FirstOrDefault();
            if (sameLevelInNewData != null)
            {
                sameLevelInNewData.Score = level.Score;
            }
            else
            {
                //Do nothing; probably that level is now removed in new version!
            }
        }

        return newData;
    }

    private OfflineData GetCurrentLevelsDataShippedWithGame()
    {
        var _gameOfflineData = new OfflineData();

        _gameOfflineData.PlayerInfo = new Player()
        {
            InstalledOn = DateTimeOffset.UtcNow.ToString()
        };

        _gameOfflineData.GameLevels = new List<Level>();

        var lessons = AssetDatabase.GetAllAssetPaths().Where(f => f.Contains("/Lessons/")).ToList();
        foreach (var item in lessons)
        {
            //ITEM Ex : Assets/Resources/Lessons/Socialmedia/twitter.png

            string category = string.Empty;
            string question = string.Empty;

            try
            {
                category = item.Split('/')[3];
                question = item.Split('/')[4];

                Level newLevel = new Level()
                {
                    Category = category,
                    Question = question,
                };

                _gameOfflineData.GameLevels.Add(newLevel);
            }
            catch
            {
                //Debug.LogException(ex);
                //This comes here when we get ITEM as :
                //Assets/Resources/Lessons/Socialmedia
                //Which is actually the folder for Category.
                //Simply supress here :)
            }
        }

        return _gameOfflineData;
    }

    private void SaveDataFile(OfflineData offlineData)
    {
        string gameDataString = JsonUtility.ToJson(offlineData);
        File.WriteAllText(_dataFilePath, gameDataString);
    }

    private OfflineData GetOfflineData()
    {
        bool dataFileExists = File.Exists(_dataFilePath);
        if (!dataFileExists)
            return null;

        var offlineDataString = File.ReadAllText(_dataFilePath);
        OfflineData offlineData = JsonUtility.FromJson<OfflineData>(offlineDataString);
        return offlineData;
    }
}

[Serializable]
public class OfflineData
{
    [SerializeField] public Player PlayerInfo;
    [SerializeField] public List<Level> GameLevels;
}

[Serializable]
public class Player
{
    [SerializeField] public string PlayerName;
    [SerializeField] public string InstalledOn;
}

[Serializable]
public class Level
{
    [SerializeField] public string Category;
    [SerializeField] public string Question;
    [SerializeField] public int Score = -1;

    [SerializeField] public string KnowledgeBase;
}
