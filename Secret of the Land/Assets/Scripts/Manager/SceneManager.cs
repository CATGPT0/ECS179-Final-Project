using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SceneManager : MonoBehaviour
    {
        private PlayerEvent playerEvent;
        public enum Scene
        {
            MainMenu,
            World,
            GameOver
        }
        void Awake()
        {
            playerEvent = FindFirstObjectByType<PlayerEvent>();
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            //playerEvent.OnPlayerDeathExit.AddListener(LoadGameOverScene);
            //playerEvent.OnPlayerRespawn.AddListener(LoadWorldScene);
        }

        public void LoadScene(Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
        }

        public void LoadMainMenuScene()
        {
            LoadScene(Scene.MainMenu);
        }

        public void LoadWorldScene()
        {
            LoadScene(Scene.World);
        }

        public void LoadGameOverScene()
        {
            LoadScene(Scene.GameOver);
        }

        public void Test()
        {
            Debug.Log("Test");
        }
    }
}
