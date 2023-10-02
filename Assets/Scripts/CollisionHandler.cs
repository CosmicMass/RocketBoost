using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class CollisionHandler : MonoBehaviour
{
    public static CollisionHandler Instance;

    [SerializeField] float levelLoadDelay = 0.5f;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    bool isTransitioining = false;
    bool collisionDisabled = false;

    public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

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
        AudioManager.Instance.SuccessPlay();
        successParticles.Play();
        Movement.Instance.enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        
    }

    void StartCrashSequence()
    {
        isTransitioining = true;
        AudioManager.Instance.CrashPlay();
        crashParticles.Play();
        Movement.Instance.enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        virtualCamera.enabled = false;
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
