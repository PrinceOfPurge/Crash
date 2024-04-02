using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //components
    private Rigidbody rb;
    public GameObject explosion;
    //private CountDownTimer countdownTimer;
    private SceneLoader sceneLoader;

    //Scene assets 
    public SceneAsset gameOverScene;
    public FloatSO playerScore; 
    
    //collectables
    private int count;
    private int maxcount; 
    
    //movement
    private float movementX;
    private float movementY;
    public float speed = 0;
    public bool IsOnTheGround = true; 
    
    //UI
    public TextMeshProUGUI countText;
    public GameObject PauseMenuPanel; 
    private bool isPaused = true; 
    
    
    //Initiate the needed components as well as the count logics
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sceneLoader = GetComponent<SceneLoader>();
        Pause();
        
        SetCountText();
        Debug.Log("Initial maxcount: " + maxcount);
    }

    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
        maxcount = CollectableObject.coins;
    }


    //movment logic using vector 2 
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }
    
    //this function is the placeholder for what is displayed, in this case: count: x and max count y
    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString() + "/" + maxcount;

    }
    

    private void Update()
    {
        if (isPaused)
            return; // If paused, don't do any further updates
        
        if (HasGameEnded())
        {
            LoadGameOverScene();
        }
        
        if (Input.GetButtonDown("Jump") && IsOnTheGround)
        {
            rb.AddForce(new Vector3(0,8,0),ForceMode.Impulse);
            IsOnTheGround = false; 
        }
    }

    //used for physics, in this scenario for player movements
    private void FixedUpdate() 
    {
        if (isPaused)
            return; // If paused, don't do any physics calculations
        
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
    
    //players can only jump if the player is on platforms with 'floor' tags
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            IsOnTheGround = true;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            Instantiate(explosion, transform.position, Quaternion.identity); 
            SoundManager.instance.coinsource.PlayOneShot(SoundManager.instance.coinSound);
        }
    }
    
    
    //conditions if the game ended; if the player won or lost  
    bool HasGameEnded()
    {
        return HasPlayerWon() || HasPlayerLost();
    }
    
    bool HasPlayerWon()
    {
        return count >= maxcount; 
    }
    
    bool HasPlayerLost()
    {
        return transform.position.y < -90 ;
    }

    void LoadGameOverScene()
    {
        float score = count;
        playerScore.value = score; 
        sceneLoader.LoadScene(gameOverScene);
    }
}
