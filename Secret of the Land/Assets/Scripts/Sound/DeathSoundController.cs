using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class DeathSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip deathSound;
    private PlayerController playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        playerController.PlayerEvent.OnPlayerDeathEnter.AddListener(PlayDeathSound);
    }

    public void PlayDeathSound()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        IEnumerator DeathSound()
        {
            audioSource.PlayOneShot(deathSound);
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(DeathSound());
    }

}
