using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SceneManager : MonoBehaviour
    {
        public enum Scene
        {
            MainMenu,
            World,
            GameOver
        }
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
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
