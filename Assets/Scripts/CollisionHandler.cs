using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f; 
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    bool sequenceStarted = false;
    
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is looking good");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("Why did ytou pick me up, I'm not in this game");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        if (sequenceStarted == false)
        {
            audioSource.Stop();

            audioSource.PlayOneShot(success);
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", levelLoadDelay);

            sequenceStarted = true;
        }

    }

    void StartCrashSequence()
    {
        if (sequenceStarted == false)
        {
            audioSource.Stop();
            
            audioSource.PlayOneShot(crash);
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", levelLoadDelay);

            sequenceStarted = true;
        }

    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
