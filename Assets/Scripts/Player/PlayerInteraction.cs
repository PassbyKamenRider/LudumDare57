using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask tileLayerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0f, tileLayerMask);

            if (hit.collider != null)
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null)
                {
                    tile.OnTileClick();
                }
            }
        }
    }
}