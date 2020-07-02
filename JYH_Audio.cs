using UnityEngine;

public class JYH_Audio : MonoBehaviour
{
    public bool playAudio = false;

    public AudioSource audioSource;
    public AudioClip clip;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
    }
    
    public void Update()
    {
        
        if(playAudio == true)
        {
            audioSource.Play();
            playAudio = false;
        }
    }
}
