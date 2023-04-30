using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audsrc;
    void Start()
    {
        audsrc=GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartNextSequence();

                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                CrashSeq();
                break;
        }
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void CrashSeq()
    {
        audsrc.PlayOneShot(crash);
        DisableThings();
        Invoke("ReloadLevel", delay);
    }
    void DisableThings()
    {
        GetComponent<Movement>().enabled = false;
        //GetComponent<AudioSource>().enabled = false;
    }
    void StartNextSequence()
    {
        audsrc.PlayOneShot(success);
        DisableThings();
        Invoke("LoadNextLevel", delay);
    }
}