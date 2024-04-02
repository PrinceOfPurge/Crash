using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class CollectablePlacement : MonoBehaviour
{
    public GameObject[] collectableParents; // Array of game object parents containing collectables for each letter
    public TMP_InputField letterInputField; // Reference to the TMP input field
    public Button submitButton; // Reference to the Button component
    public TMP_Text errorMessageText; // Reference to the TMP text element for displaying error messages
    public string[] allowedLetters = { "A", "B", "C" }; // Allowed letters for activation
    public Color errorColor = Color.red;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject parent in collectableParents)
        {
            parent.SetActive(false); // Deactivate all collectable parents at the start
        }
    }

    public void ActivateCollectables()
    {
        string inputLetter = letterInputField.text.ToUpper(); // Get the input letter and convert to uppercase
        bool validLetter = false;
        foreach (string letter in allowedLetters)
        {
            if (inputLetter == letter)
            {
                ActivateParentByLetter(inputLetter); // Activate the collectable parent corresponding to the input letter
                validLetter = true;
                break;
            }
        }
        if (!validLetter)
        {
            Debug.Log("Invalid letter input. Please enter A, B, or C.");
            errorMessageText.text = "Invalid letter input. Please enter A, B, or C."; // Update error message text on UI
            errorMessageText.color = errorColor; // Set error message text color to errorColor
        }
        else
        {
            letterInputField.gameObject.SetActive(false); // Hide the input field after correct input
            submitButton.gameObject.SetActive(false); // Hide the submit button after correct input
            playerController.Resume();
            
        }
    }

    void ActivateParentByLetter(string letter)
    {
        foreach (GameObject parent in collectableParents)
        {
            if (parent.name.Contains(letter)) // Check if the parent's name contains the input letter
            {
                parent.SetActive(true); // Activate the collectable parent corresponding to the input letter
            }
        }
    }
}
