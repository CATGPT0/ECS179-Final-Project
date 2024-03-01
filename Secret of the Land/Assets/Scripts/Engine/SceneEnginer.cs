using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
        public static class SceneEngine
    {
        public enum Scene
        {
            MainMenu,
            World,
            GameOver
        }

        public static void LoadGameOverScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Scene.GameOver.ToString());
        }
    }
}
