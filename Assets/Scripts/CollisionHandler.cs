using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 0.5f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioining = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    //void Update()
    //{
    //    RespondToDebugKeys();
    //}

    //void RespondToDebugKeys()
    //{
    //    if(Input.GetKeyDown(KeyCode.L))
    //    {
    //        LoadNextLevel();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        collisionDisabled = !collisionDisabled; // toggle collision
    //    }
    //}
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioining || collisionDisabled)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;                        
        }
    }

    void StartSuccessSequence()
    {
        isTransitioining = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Movement.Instance.enabled = false;
        //GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);

    }

    void StartCrashSequence()
    {
        isTransitioining = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        Movement.Instance.enabled = false;
        //GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }

}
