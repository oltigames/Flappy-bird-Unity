using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public event Action OnJumpInput;
    public event Action OnPauseInput;
    public event Action OnUnPauseInput;
    
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    public static bool isPaused;
    [SerializeField] private GameObject pauseMenu;
    
    // Reference to the generated C# Input class with player controls
    private PlayerControls input;
    private AudioManager audioManager; 
    
    private void Awake()
    {
        isPaused = false;
        input = new PlayerControls();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        // Setup to allow other scripts to run their functions when an event happens
        input.FlappyBird.Jump.performed += x => OnJumpInput?.Invoke();
        input.FlappyBird.Pause.performed += x =>  OnPauseInput?.Invoke();
        input.UI.UnPause.performed += x =>  OnUnPauseInput?.Invoke();
        
        // Subscribing to events (Good practice is to have this separate from the Setup above)
        OnPauseInput += Pause;
        OnUnPauseInput += UnPause;
        
        input.FlappyBird.Enable();
        input.UI.Disable();
    }

    // To Prevent Memory Leaks and Errors
    private void OnDisable()
    {
        input.FlappyBird.Disable();
        input.UI.Disable();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        audioManager.PlaySFX(audioManager.scoreIncreaseSound);
    }
    
    public void GameOver()
    {
        // Debug.Log("Game Over");
    }

    public void Pause()
    {
        isPaused = true; 
        Time.timeScale = 0;
        
        input.FlappyBird.Disable();
        input.UI.Enable();
        
        pauseMenu.SetActive(true);
    }
    
    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1;
        
        input.UI.Disable();
        input.FlappyBird.Enable();
        
        pauseMenu.SetActive(false);
    }
}
