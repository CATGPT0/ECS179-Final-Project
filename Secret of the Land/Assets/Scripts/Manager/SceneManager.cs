using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SceneManager : MonoBehaviour
    {
        private PlayerEvent playerEvent;
        public SceneManager Instance { get; private set; }
        public enum Scene
        {
            MainMenu,
            World,
            GameOver,
            CardBattleScene
        }
        void Awake()
        {
            Instance = this;
            playerEvent = FindFirstObjectByType<PlayerEvent>();
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            //playerEvent.OnPlayerDeathExit.AddListener(LoadGameOverScene);
            //playerEvent.OnPlayerRespawn.AddListener(LoadWorldScene);
        }

        public static void LoadScene(Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
        }

        public static void LoadSceneAsync(Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

        public static void UnloadScene(Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene.ToString());
        }

        public static void LoadMainMenuScene()
        {
            LoadScene(Scene.MainMenu);
        }

        public static void LoadWorldScene()
        {
            LoadScene(Scene.World);
        }

        public static void LoadGameOverScene()
        {
            LoadScene(Scene.GameOver);
        }

        public static void LoadCardBattleScene()
        {
            LoadScene(Scene.CardBattleScene);
        }
    }
}
