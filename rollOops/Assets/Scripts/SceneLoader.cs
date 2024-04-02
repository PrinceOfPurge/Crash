using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //this is the script for loading any scene and can be applied in many ways in the unity editor 
    public void LoadScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
    
}
