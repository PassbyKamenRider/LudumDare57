using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] AudioSource wallAudio;
    [SerializeField] Sprite[] tileSprites;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] GameObject wallPrefab;
    private GameObject wall;
    private bool isTarget;
    private int tileType; // -1: no tile, 0: normal, 1: trap, 2: lock, 3: key, 4: wall

    // void Start()
    // {
    //     EventManager.Instance.AddListener(GlobalEvent.AnyTileChangedByPlayer, AnyTileChanged);
    // }

    public void UpdateTile()
    {
        spr.sprite = tileSprites[tileType];
        if (isTarget) spr.sprite = tileSprites[tileSprites.Length-1];
    }

    public void SetTarget()
    {
        isTarget = true;
    }

    // public void AnyTileChanged()
    // {
    //     Debug.Log(111111111);
    // }

    public void OnTileClick()
    {
        // cannot place on special tiles
        if (tileType == 0)
        {
            if (GameData.rockCount == 0) return;
            // if is normal grid, set type to wall
            wallAudio.Play();
            tileType = 4;
            GameData.rockCount -= 1;
            wall = Instantiate(wallPrefab, transform.position, Quaternion.identity, transform);
        }
        else if (tileType == 4)
        {
            // if is wall grid, set type to normal, remove wall
            wallAudio.Play();
            tileType = 0;
            GameData.rockCount += 1;
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
