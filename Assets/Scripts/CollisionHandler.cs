using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float reloadDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip landingPad;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem landingPadParticles;

  
    AudioSource m_audioSource;
    
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start() 
    {
        m_audioSource = GetComponent<AudioSource>();
     
    }

    void Update() 
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggles variable on and off.
            
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) { return;} //if set true, just return and ignore the everything below.

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have hit a Friendly object");
                break;
            case "Finish":
                FinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        
        isTransitioning = true;
        m_audioSource.Stop();
        m_audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        //todo add particle sequence.
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", reloadDelay);
    }

    void FinishSequence()
    {
        isTransitioning = true;
        m_audioSource.Stop();
        m_audioSource.PlayOneShot(landingPad);
        landingPadParticles.Play();
        //todo add particle sequence.
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", reloadDelay);      
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


}
