using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BootyEngine
{
    private static Dictionary<EntityType.Type, int> monsterXp = new Dictionary<EntityType.Type, int>()
    {
        { EntityType.Type.Slime, 1 },
        { EntityType.Type.Goblin, 2 },
        { EntityType.Type.Orc, 3 },
        { EntityType.Type.Troll, 4 },
        { EntityType.Type.Dragon, 5 },
        { EntityType.Type.Skeleton, 30 }
    };

    private static Dictionary<int, float> levelDifferenceModifier = new Dictionary<int, float>()
    {
        { -4, 0.6f },
        { -3, 0.7f },
        { -2, 0.8f },
        { -1, 0.9f },
        { 0, 1f },
        { 1, 1.1f },
        { 2, 1.2f },
        { 3, 1.3f },
        { 4, 1.4f }
    };

    private static Dictionary<int, float> playerLevelModifier = new Dictionary<int, float>()
    {
        { 1, 1f },
        { 2, 1.1f },
        { 3, 1.2f },
        { 4, 1.3f },
        { 5, 1.4f },
        { 6, 1.5f },
        { 7, 1.6f },
        { 8, 1.7f },
        { 9, 1.8f },
        { 10, 1.9f }
    };
    public static int CalculateXP(int monsterLevel, int playerLevel, EntityType.Type monsterType)
    {
        int levelDifference = monsterLevel - playerLevel;

        if (levelDifference > 4)
        {
            levelDifference = 4;
        }
        else if (levelDifference < -4)
        {
            return 0;
        }
        return Mathf.RoundToInt(monsterXp[monsterType] * levelDifferenceModifier[levelDifference] * playerLevelModifier[playerLevel]);
    }
}
