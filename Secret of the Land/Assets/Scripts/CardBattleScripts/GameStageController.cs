using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CardBattle;


namespace CardBattle
{

    public enum GameStage
    {
        beforePlayerRound,
        playerRound,
        afterPlayerRound,
        beforeEnemyRound,
        enemyRound,
        afterEnemyRound,
    }
    public class GameStageController
    {
        // This will move the stage to next stage
        public static void MoveToNextStage(ref GameStage gameStage)
        {
            switch (gameStage)
            {
                case GameStage.beforePlayerRound:
                    gameStage = GameStage.playerRound;
                    break;
                case GameStage.playerRound:
                    gameStage = GameStage.afterPlayerRound;
                    break;
                case GameStage.afterPlayerRound:
                    gameStage = GameStage.beforeEnemyRound;
                    break;
                case GameStage.beforeEnemyRound:
                    gameStage = GameStage.enemyRound;
                    break;
                case GameStage.enemyRound:
                    gameStage = GameStage.afterEnemyRound;
                    break;
                case GameStage.afterEnemyRound:
                    gameStage = GameStage.beforePlayerRound;
                    break;
            }
        }

    }
}
