using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
    void Awake()
    {
        GamePauseEvent.Register(OnGamePause);
        GameResumeEvent.Register(OnGameResume);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGamePause()
    {
        gameObject.SetActive(false);
    }

    void OnGameResume()
    {
        gameObject.SetActive(true);
    }
}
