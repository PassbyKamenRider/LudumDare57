using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    // Tile Data
    public static Dictionary<Vector2Int, TileData> tileMap = new Dictionary<Vector2Int, TileData>();

    public static void RegisterTile(TileData tile)
    {
        tileMap[tile.position] = tile;
    }

    public static TileData GetTileData(Vector2Int pos)
    {
        tileMap.TryGetValue(pos, out TileData data);
        return data;
    }

    // Player data
    public static Vector2Int playerPosition = new Vector2Int();
    public static int keyCount = 0;

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
        return GridsManager.instance.GetTilePosition(playerPosition);
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