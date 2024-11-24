using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource soundOrigin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //Create Temporary GameObject
        AudioSource audioSource = Instantiate(soundOrigin, spawnTransform.position, Quaternion.identity);

        //Add AudioClip Component
        audioSource.clip = audioClip;

        //Configure Volume
        audioSource.volume = volume;

        //Play Sound
        audioSource.Play();

        //Get Length of SFX Clip
        float soundLength = audioSource.clip.length;

        //Destroy Clip after finished playing
        Destroy(audioSource.gameObject, soundLength);

    }

}
