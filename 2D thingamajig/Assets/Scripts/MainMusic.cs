using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{
    [SerializeField] AudioClip audioClip1;
    [SerializeField] AudioClip audioClip2;
    [SerializeField] AudioClip audioClip3;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1f;

        int _randomclip = Random.Range(1, 4);

        if (_randomclip == 1)
        {
            audioSource.clip = audioClip1;
        }
        else if (_randomclip == 2)
        {
            audioSource.clip = audioClip2;
        }
        else
        {
            audioSource.clip = audioClip3;
        }
    }

    private void Start()
    {
        audioSource.Play();
    }
    private void Update()
    {
        audioSource.pitch = GameManager.Instance.gameSpeed;
    }
}
