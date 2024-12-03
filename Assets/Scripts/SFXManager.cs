using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource soundOrigin;
    private Dictionary<string, AudioSource> activeLoopingSounds = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Create a temporary AudioSource for one-shot sounds
        AudioSource audioSource = Instantiate(soundOrigin, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        // Destroy the AudioSource after the sound finishes playing
        Destroy(audioSource.gameObject, audioClip.length);
    }

    public void PlayLoopingSFX(string soundKey, AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Check if the sound is already playing
        if (activeLoopingSounds.ContainsKey(soundKey))
        {
            return;
        }

        // Create a persistent AudioSource for looping sound
        AudioSource audioSource = Instantiate(soundOrigin, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();

        // Store the AudioSource reference
        activeLoopingSounds[soundKey] = audioSource;
    }

    public void StopLoopingSFX(string soundKey)
    {
        // Stop and remove the looping sound if it exists
        if (activeLoopingSounds.TryGetValue(soundKey, out AudioSource audioSource))
        {
            audioSource.Stop();
            Destroy(audioSource.gameObject);
            activeLoopingSounds.Remove(soundKey);
        }
    }
}
