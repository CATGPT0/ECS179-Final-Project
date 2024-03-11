using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class DeathSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip deathSound;
    private bool isDead = false;
    private PlayerController playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        playerController.PlayerEvent.OnPlayerDeathEnter.AddListener(PlayDeathSound);
        playerController.PlayerEvent.OnPlayerRespawn.AddListener(ResetDeadProperty);
    }

    public void PlayDeathSound()
    {
        if (isDead)
        {
            return;
        }
        IEnumerator DeathSound()
        {
            isDead = true;
            audioSource.PlayOneShot(deathSound);
            yield return new WaitForSeconds(1.5f);
            isDead = false;
        }
        StartCoroutine(DeathSound());
    }

    public void ResetDeadProperty()
    {
        isDead = false;
    }
}
