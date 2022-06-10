using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }


    private AudioSource audioSource;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip dashSound;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip jetPackSound;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayDashSound()
    {
        audioSource.PlayOneShot(dashSound);
    }

    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }


    public void PlayJetPackSound()
    {
        audioSource.Play();
    }
    
    public void StopJetPackSound()
    {
        audioSource.Stop();
    }

}
