using UnityEngine;

public class TileData
{
    public Vector2Int position;
    public int tileType; // -1: no tile, 0: normal, 1: trap, 2: lock, 3: key, 4: wall
    public TileData(Vector2Int position_, int tileType_)
    {
        position = position_;
        tileType = tileType_;
    }
}