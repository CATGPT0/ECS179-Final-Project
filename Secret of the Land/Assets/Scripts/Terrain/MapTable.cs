using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation
{
    public Vector2 topLeft;
    public Vector2 bottomRight;
}

public static class MapTable
{
    public static Dictionary<string, MapLocation> locations = new Dictionary<string, MapLocation>()
    {
        { "Forest", new MapLocation() { topLeft = new Vector2(-157, 282), bottomRight = new Vector2(-104, 237) } },
        { "Forest2", new MapLocation() { topLeft = new Vector2(-21, 273), bottomRight = new Vector2(31, 200) } },
        { "SnowLand", new MapLocation() { topLeft = new Vector2(37, 272), bottomRight = new Vector2(189, 233) } },
        { "GrassLand2", new MapLocation() { topLeft = new Vector2(0, 1001), bottomRight = new Vector2(1000, 2000) } },
        { "Village", new MapLocation() { topLeft = new Vector2(40, 225), bottomRight = new Vector2(105, 193) } }
    };

}
