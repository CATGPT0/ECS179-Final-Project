using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class LevelingSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip getXPSound;
    [SerializeField]
    private AudioClip levelUpSound;
    private PlayerController playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        playerController.PlayerEvent.OnPlayerGetXPSound += PlayXPSound;
        playerController.PlayerEvent.OnPlayerLevelUpSound += PlayLevelSound;
    }

    public void PlayXPSound()
    {

        IEnumerator XPSound()
        {

            audioSource.PlayOneShot(getXPSound);
            yield return new WaitForSeconds(1.5f);

        }
        StartCoroutine(XPSound());
    }

    public void PlayLevelSound()
    {
        IEnumerator LevelSound()
        {
 
            audioSource.PlayOneShot(levelUpSound);
            yield return new WaitForSeconds(1.5f);
        }
        StartCoroutine(LevelSound());
    }
}
