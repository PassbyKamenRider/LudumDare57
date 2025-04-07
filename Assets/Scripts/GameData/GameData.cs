using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    // Tile Data
    public static Dictionary<Vector2Int, Tile> tileMap = new Dictionary<Vector2Int, Tile>();

    public static void RegisterTile(Vector2Int pos, Tile tile)
    {
        tileMap[pos] = tile;
        tile.UpdateTile();
    }

    public static Tile GetTile(Vector2Int pos)
    {
        tileMap.TryGetValue(pos, out Tile tile);
        return tile;
    }

    // Player data
    public static Vector2Int playerPosition = new Vector2Int();
    public static int keyCount = 0;
    public static bool isRunningDFS;
    public static int rockCount = 0;
    public static int lifeCount = 10;
    public static int levelReached = 0;

    public static void SetPlayerPosition(Vector2Int pos)
    {
        playerPosition = pos;
    }

    public static Vector2Int GetPlayerPosition()
    {
        return playerPosition;
    }

    public static Vector2 GetPlayerGlobalPosition()
    {
        return TilesManager.instance.GetTilePosition(playerPosition);
    }

    public static bool HasKey()
    {
        return keyCount > 0;
    }

    public static void GetKey()
    {
        keyCount += 1;
    }

    public static void UseKey()
    {
        keyCount -= 1;
    }
}