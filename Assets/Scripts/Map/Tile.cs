using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject wallPrefab;
    private GameObject wall;
    private int tileType;

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

    public void SetType(int type)
    {
        tileType = type;
    }

    public bool IsValid()
    {
        // normal tile, trap tile, lock tile, and key tile are valid
        return (tileType == 0) || (tileType == 1) || (tileType == 2 && GameData.HasKey()) || (tileType == 3);
    }
}
