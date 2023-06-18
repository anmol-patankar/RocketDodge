using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    AudioSource audsrc;
    bool isTransitioning = false;
    bool collisionDisable=false;
     void Update() {
        RespondToDebugkeys();
    }
    void RespondToDebugkeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable=!collisionDisable;
        }

    }
    void Star()
    {
        audsrc = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable)
        {
            return;
        }
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
        isTransitioning = false;
        audsrc.Stop();
        audsrc.PlayOneShot(crash);
        crashParticles.Play();
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
        isTransitioning = true;
        audsrc.Stop();
        audsrc.PlayOneShot(success);
        successParticles.Play();
        DisableThings();
        Invoke("LoadNextLevel", delay);
    }
}
