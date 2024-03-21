using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Table
{
    public static Dictionary<EntityType.Type, Dictionary<int, int>> healthTable = new Dictionary<EntityType.Type, Dictionary<int, int>>()
    {
        { EntityType.Type.Slime, new Dictionary<int, int>() { { 1, 30 }, { 2, 120 }, { 3, 140 }, { 4, 160 }, { 5, 180 }, { 6, 200 }, { 7, 220 }, { 8, 240 }, { 9, 260 }, { 10, 300 } } },
        { EntityType.Type.Goblin, new Dictionary<int, int>() { { 1, 100 }, { 2, 120 }, { 3, 140 }, { 4, 160 }, { 5, 180 }, { 6, 200 }, { 7, 220 }, { 8, 240 }, { 9, 260 }, { 10, 300 } } },
        { EntityType.Type.Orc, new Dictionary<int, int>() { { 1, 100 }, { 2, 120 }, { 3, 140 }, { 4, 160 }, { 5, 180 }, { 6, 200 }, { 7, 220 }, { 8, 240 }, { 9, 260 }, { 10, 300 } } },
        { EntityType.Type.Troll, new Dictionary<int, int>() { { 1, 100 }, { 2, 120 }, { 3, 140 }, { 4, 160 }, { 5, 180 }, { 6, 200 }, { 7, 220 }, { 8, 240 }, { 9, 260 }, { 10, 300 } } },
        { EntityType.Type.Dragon, new Dictionary<int, int>() { { 1, 100 }, { 2, 120 }, { 3, 140 }, { 4, 160 }, { 5, 180 }, { 6, 200 }, { 7, 220 }, { 8, 240 }, { 9, 260 }, { 10, 300 } } },
        { EntityType.Type.Skeleton, new Dictionary<int, int>() { { 1, 40 }, { 2, 50 }, { 3, 60 }, { 4, 70 }, { 5, 80 }, { 6, 120 }, { 7, 140 }, { 8, 160 }, { 9, 180 }, { 10, 200 } } },
        { EntityType.Type.Player, new Dictionary<int, int>() { { 1, 100 }, { 2, 120 }, { 3, 140 }, { 4, 160 }, { 5, 180 }, { 6, 200 }, { 7, 220 }, { 8, 240 }, { 9, 260 }, { 10, 300 } } }
    };

    public static Dictionary<EntityType.Type, Dictionary<int, int>> attackPowerTable = new Dictionary<EntityType.Type, Dictionary<int, int>>()
    {
        { EntityType.Type.Slime, new Dictionary<int, int>() { { 1, 10 }, { 2, 12 }, { 3, 14 }, { 4, 16 }, { 5, 18 }, { 6, 20 }, { 7, 22 }, { 8, 24 }, { 9, 26 }, { 10, 30 } } },
        { EntityType.Type.Goblin, new Dictionary<int, int>() { { 1, 10 }, { 2, 12 }, { 3, 14 }, { 4, 16 }, { 5, 18 }, { 6, 20 }, { 7, 22 }, { 8, 24 }, { 9, 26 }, { 10, 30 } } },
        { EntityType.Type.Orc, new Dictionary<int, int>() { { 1, 10 }, { 2, 12 }, { 3, 14 }, { 4, 16 }, { 5, 18 }, { 6, 20 }, { 7, 22 }, { 8, 24 }, { 9, 26 }, { 10, 30 } } },
        { EntityType.Type.Troll, new Dictionary<int, int>() { { 1, 10 }, { 2, 12 }, { 3, 14 }, { 4, 16 }, { 5, 18 }, { 6, 20 }, { 7, 22 }, { 8, 24 }, { 9, 26 }, { 10, 30 } } },
        { EntityType.Type.Dragon, new Dictionary<int, int>() { { 1, 10 }, { 2, 12 }, { 3, 14 }, { 4, 16 }, { 5, 18 }, { 6, 20 }, { 7, 22 }, { 8, 24 }, { 9, 26 }, { 10, 30 } } },
        { EntityType.Type.Skeleton, new Dictionary<int, int>() { { 1, 14 }, { 2, 16 }, { 3, 18 }, { 4, 20 }, { 5, 22 }, { 6, 24 }, { 7, 26 }, { 8, 28 }, { 9, 30 }, { 10, 32 } } },
        { EntityType.Type.Player, new Dictionary<int, int>() { { 1, 10 }, { 2, 12 }, { 3, 14 }, { 4, 16 }, { 5, 18 }, { 6, 20 }, { 7, 22 }, { 8, 24 }, { 9, 26 }, { 10, 30 } } }
    };

    public static Dictionary<EntityType.Type, Dictionary<int, int>> armorTable = new Dictionary<EntityType.Type, Dictionary<int, int>>()
    {
        { EntityType.Type.Slime, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Goblin, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Orc, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Troll, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Dragon, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Skeleton, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Player, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } }
    };

    public static Dictionary<EntityType.Type, Dictionary<int, int>> magicResistTable = new Dictionary<EntityType.Type, Dictionary<int, int>>()
    {
        { EntityType.Type.Slime, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Goblin, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Orc, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Troll, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Dragon, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Skeleton, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } },
        { EntityType.Type.Player, new Dictionary<int, int>() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 4 }, { 6, 5 }, { 7, 6 }, { 8, 7 }, { 9, 8 }, { 10, 10 } } }
    };

    public static Dictionary<EntityType.Type, Dictionary<int, float>> speedTable = new Dictionary<EntityType.Type, Dictionary<int, float>>()
    {
        { EntityType.Type.Slime, new Dictionary<int, float>() { { 1, 5 }, { 2, 5.5f }, { 3, 6 }, { 4, 6.5f }, { 5, 7 }, { 6, 7.5f }, { 7, 8 }, { 8, 8.5f }, { 9, 9 }, { 10, 10 } } },
        { EntityType.Type.Goblin, new Dictionary<int, float>() { { 1, 5 }, { 2, 5.5f }, { 3, 6 }, { 4, 6.5f }, { 5, 7 }, { 6, 7.5f }, { 7, 8 }, { 8, 8.5f }, { 9, 9 }, { 10, 10 } } },
        { EntityType.Type.Orc, new Dictionary<int, float>() { { 1, 5 }, { 2, 5.5f }, { 3, 6 }, { 4, 6.5f }, { 5, 7 }, { 6, 7.5f }, { 7, 8 }, { 8, 8.5f }, { 9, 9 }, { 10, 10 } } },
        { EntityType.Type.Troll, new Dictionary<int, float>() { { 1, 5 }, { 2, 5.5f }, { 3, 6 }, { 4, 6.5f }, { 5, 7 }, { 6, 7.5f }, { 7, 8 }, { 8, 8.5f }, { 9, 9 }, { 10, 10 } } },
        { EntityType.Type.Dragon, new Dictionary<int, float>() { { 1, 5 }, { 2, 5.5f }, { 3, 6 }, { 4, 6.5f }, { 5, 7 }, { 6, 7.5f }, { 7, 8 }, { 8, 8.5f }, { 9, 9 }, { 10, 10 } } },
        { EntityType.Type.Skeleton, new Dictionary<int, float>() { { 1, 2 }, { 2, 2.2f }, { 3, 2.4f }, { 4, 2.6f }, { 5, 2.8f }, { 6, 3.2f }, { 7, 3.6f }, { 8, 4f }, { 9, 4.5f }, { 10, 5f } } },
        { EntityType.Type.Player, new Dictionary<int, float>() { { 1, 5 }, { 2, 5.5f }, { 3, 6 }, { 4, 6.5f }, { 5, 7 }, { 6, 7.5f }, { 7, 8 }, { 8, 8.5f }, { 9, 9 }, { 10, 10 } } }
    };

    public static Dictionary<EntityType.Type, AttackType> attackTypeTable = new Dictionary<EntityType.Type, AttackType>()
    {
        { EntityType.Type.Slime, AttackType.Physical },
        { EntityType.Type.Goblin, AttackType.Physical },
        { EntityType.Type.Orc, AttackType.Physical },
        { EntityType.Type.Troll, AttackType.Physical },
        { EntityType.Type.Dragon, AttackType.Magical },
        { EntityType.Type.Skeleton, AttackType.Physical },
        { EntityType.Type.Player, AttackType.Physical }
    };


    public static Dictionary<int, int> levelThresholds = new Dictionary<int, int>()
    {
        {1, 100},
        {2, 250},
        {3, 500},
        {4, 800},
        {5, 1200},
        {6, 1800},
        {7, 2500},
        {8, 3500},
        {9, 5000},
        {10, 7000}
    };

     private static Dictionary<EntityType.Type, int> monsterXp = new Dictionary<EntityType.Type, int>()
    {
        { EntityType.Type.Slime, 20 },
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
        Debug.Log("Monster Level: " + monsterLevel);
        Debug.Log("Player Level: " + playerLevel);
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