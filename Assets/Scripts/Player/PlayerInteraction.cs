using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask tileLayerMask;
    [SerializeField] TextMeshProUGUI rockCountText, lifeCountText;

    void Start()
    {
        GameData.rockCount = GameManager.Instance.rockCounts[SceneManager.GetActiveScene().buildIndex-1];
        rockCountText.text = GameData.rockCount.ToString();
        lifeCountText.text = GameData.lifeCount.ToString();

        EventManager.Instance.AddListener(GlobalEvent.PlayerRoasted, () => UpdateLife());
        EventManager.Instance.AddListener(GlobalEvent.AnyTileChangedByPlayer, () => rockCountText.text = GameData.rockCount.ToString());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameData.isRunningDFS) return;

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0f, tileLayerMask);

            if (hit.collider != null)
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null)
                {
                    EventManager.Instance.Invoke(GlobalEvent.AnyTileChangedByPlayer);
                    tile.OnTileClick();
                }
            }
        }
    }

    private void UpdateLife()
    {
        GameData.lifeCount -= 1;
        if (GameData.lifeCount == 0) GameData.lifeCount = 666;
        lifeCountText.text = GameData.lifeCount.ToString();
    }
}