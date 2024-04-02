using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using File = System.IO.File;

public class HighScoreBoard : MonoBehaviour
{
    //placeholder for text 
    TMP_Text text; 
    
    //where the saved data file will be stored
    string filePath;

    //the placeholders for data that differs for name and score 
    [Serializable]
    public class HighScoreEntry
    {
        public string name;
        public int score; 
    }
    
    //an array for the entries of high scores 
    [Serializable]
    public class HighScoreData
    {
        public List<HighScoreEntry> entries;
        
        //sorting the highscores using a descending order to show highest score in respective order
        public void Sort()
        {
            entries = entries.OrderByDescending(entry => entry.score).ToList(); 
        }
    }
    
//placeholders for data and entry (not sure if entry is needed in this case)
    HighScoreData data; 

    HighScoreEntry entry; 
    
    //The data of highscores will be located, initiated and displayed on start 
    void Start()
    {
        //helps us find where our json file with the data is stored in case we need to remove it (currently has data on it) 
        Debug.Log("persistance data path:" + Application.persistentDataPath);
        filePath = Application.persistentDataPath + "/highscores.json";

        text = GetComponent<TMP_Text>();
        
        Load();
        DisplayText();
    }

    //Placeholder highscores to test arrangement, data and general display
    void CreateDummyData()
    {
        data = new HighScoreData();
        data.entries = new List<HighScoreEntry>
        {
            new HighScoreEntry { name = "Goober", score = 3 },
            new HighScoreEntry { name = "Jodie", score = 2 },
            new HighScoreEntry { name = "Krook", score = 0 },
        };
    }

    //when called will save and sort the data into jason format in our filepath 
    void Save()
    {
        data.Sort();
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json); 
    }
    
    //if the file exists, this function will read the file and display
    //if file doesn't exist the dummy data will display and give option to save new data 
    void Load()
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<HighScoreData>(json);
        }
        else
        {
            CreateDummyData();
            Save();
        }
    }

    //we want to read the json file including scores and push them straight to tmp for display
    //var i in this method helps order the scores and include a number prior to the results
    //if statement in case we want use our text without displaying anything
    //\n start a new line
    
    void DisplayText()
    {
        if (text == null )
        {
            return;
        }
        
        data.Sort();
        text.text = "";
        var i = 1;
        foreach (var entry in data.entries)
        {
            text.text += i + ".\t" + entry.name + "\t\t" + entry.score + "\n";
            i++;
        }
    }

    //Colects/saves to add place holders for name and score entries
    public void AddHighScore(string name, int score)
    {
        var entry = new HighScoreEntry { name = name, score = score };
        data.entries.Add(entry);
        Save();
        DisplayText();
    }
}
