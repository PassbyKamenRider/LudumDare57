using UnityEngine;

public class TilesManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] string[] mapRows;
    [HideInInspector] public int gridSize;
    [HideInInspector] public float tileSize = 2.0f;
    [HideInInspector] public Vector2Int startPos;
    [HideInInspector] public Vector2Int targetPos;
    [Header("Grid Prefabs")]
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject tileParent;
    [SerializeField] GameObject playerPrefab;
    public static TilesManager instance;
    private Tile[,] tiles;

    void Awake()
    {
        if (!instance) instance = this;
    }

    void Start()
    {
        GenerateGrid();
        InitializePlayer();
    }
    
    private void GenerateGrid()
    {
        int height = mapRows.Length;
        int width = mapRows[0].Length;

        gridSize = Mathf.Max(height, width);
        tiles = new Tile[gridSize, gridSize];

        for (int j = 0; j < height; j++)
        {
            string row = mapRows[j];
            for (int i = 0; i < row.Length; i++)
            {
                char c = row[i];
                int tileType;

                switch (c)
                {
                    case 'O': tileType = 0; break; // normal
                    case 'S': tileType = 0; startPos = new Vector2Int(i, j); break; // start pos, normal
                    case 'G': tileType = 0; targetPos = new Vector2Int(i, j); break; // goal pos, normal
                    case '!': tileType = 1; break; // trap
                    case 'B': tileType = 2; break; // broken wall
                    case 'L': tileType = 3; break; // laser gun
                    case '#': default: tileType = -1; break; // no tile
                }

                if (tileType == -1)
                    continue;

                Vector3 pos = GetTilePosition(new Vector2Int(i,j));
                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity, tileParent.transform);
                tile.name = $"Tile ({i},{j})";
                Tile tileComp = tile.GetComponent<Tile>();
                if (c == 'G') tileComp.SetTarget();
                tiles[i, j] = tileComp;
                tileComp.SetTileType(tileType);
                GameData.RegisterTile(new Vector2Int(i,j), tileComp);
            }
        }
    }

    private void InitializePlayer()
    {
        Vector3 spawnPos = GetTilePosition(startPos) + new Vector3(0, 0.3f, 0);
        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        Debug.Log($"Spawn player at {startPos}");
        Debug.Log($"Target position at {targetPos}");
    }

    public Vector3 GetTilePosition(Vector2Int pos)
    {
        float offset = (gridSize - 1) * tileSize * 0.5f;

        float x = pos.x * tileSize - offset;
        float y = (gridSize - 1 - pos.y) * tileSize - offset;

        return new Vector3(x, y, 0.0f);
    }

    public bool IsValid(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x >= gridSize || pos.y < 0 || pos.y >= gridSize) return false;

        Tile tile = tiles[pos.x, pos.y];
        return tile != null && tile.IsValid();
    }
}
