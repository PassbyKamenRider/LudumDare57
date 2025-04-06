using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Sprite[] tileSprites;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] GameObject wallPrefab;
    private GameObject wall;
    private int tileType; // -1: no tile, 0: normal, 1: trap, 2: lock, 3: key, 4: wall

    public void UpdateTile()
    {
        spr.sprite = tileSprites[tileType];
    }

    public void OnTileClick()
    {
        // cannot place on special tiles
        if (tileType != 0) return;

        // set tile type to wall
        tileType = 4;

        if (wall == null)
        {
            wall = Instantiate(wallPrefab, transform.position, Quaternion.identity, transform);
        }
        else
        {
            Destroy(wall);
            wall = null;
        }
    }

    public int GetTileType()
    {
        return tileType;
    }

    public void SetTileType(int type)
    {
        tileType = type;
    }

    public bool IsValid()
    {
        // normal tile, trap tile, lock tile, and key tile are valid
        return (tileType == 0) || (tileType == 1) || (tileType == 2 && GameData.HasKey()) || (tileType == 3);
    }
}
