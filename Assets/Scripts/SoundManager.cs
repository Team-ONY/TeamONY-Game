using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SoundManager instance created");
            Debug.Log("SoundManager initialized");
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("AudioSource added to SoundManager");
        }
    }

    public void PlayClickSound()
    {
        Debug.Log("PlayClickSound called");
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
            Debug.Log("Click sound played");
        }
        else
        {
            Debug.LogWarning("Click sound is not assigned!");
        }
    }
}