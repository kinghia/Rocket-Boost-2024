using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    float loadSceneLevel = 2f;
    bool isControllable = true;
    bool isCollidable = true;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            DelayNextScene();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (!isControllable || !isCollidable)  {return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("May dang o vach xuat phat");
                break;
            case "Finish":
                DelayNextScene();
                break ;
            case "Fuel":
                Debug.Log("May vua cham vao tao kia`");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void DelayNextScene()
    {
        isControllable = false;
        successParticles.Play();
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", loadSceneLevel);
        audioSource.PlayOneShot(success);
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        crashParticles.Play();
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", loadSceneLevel);
        audioSource.PlayOneShot(crashSFX);
    }

    void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
