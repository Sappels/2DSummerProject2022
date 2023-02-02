using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    private AudioSource audioSource;

    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip coinSound;
    public AudioClip coolerCoinSound;
    public AudioClip jetPackSound;
    public AudioClip timeDecreaseSound;

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

    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
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
