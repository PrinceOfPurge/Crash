using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class HighScoreSaver : MonoBehaviour
{
//Highscores placeholder    
    public HighScoreBoard highScoreBoard;
    
//Text placeholders    
    public TMP_Text playerScoreText;
    public TMP_InputField playerNameInput;
    public Button saveScoreButton;
    
//Data that saves using scriptable objects(it keeps the previous value saved)
    public FloatSO playerScore;
    
//boolean to ensure player cannot saved more than once for each score  
    bool hasSaved = false;

    void Start()
    {
        //sends players score as a int (forced due to logics) to playerscore text placeholder
        playerScoreText.text = "score: " + (int)playerScore.value; 
        
        //executes our OnSave function and sets the button off to make another submission
        saveScoreButton.onClick.AddListener(OnSaveClicked);
        saveScoreButton.interactable = false;
        
        //limits the input to 3 and executes our OnNameChanged function
        playerNameInput.characterLimit = 4; 
        playerNameInput.onValueChanged.AddListener(OnNameChanged);
    }

    void OnSaveClicked()
    {
        //bool turns true in this case to show that the save has been completed
        hasSaved = true;
        saveScoreButton.interactable = false; 
        //adds the new highscore with all the gathered data 
        highScoreBoard.AddHighScore(playerNameInput.text, (int)playerScore.value);
    }

    void OnNameChanged(string name)
    {
        //!hassaved is equal to true meaning the player is succesful in saving and the length is less than 0 to execute
        saveScoreButton.interactable = !hasSaved && name.Length > 0; 
    }
}
