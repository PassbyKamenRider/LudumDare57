using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DfsAgent : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] Vector3 offset = new Vector3(0, 0.3f, 0);
    [SerializeField] GlobalEvent runAgentEvent;
    [Header("Audio Settings")]
    [SerializeField] AudioSource walkAudio, tileAudio, bumpAudio;
    [SerializeField] AudioClip pickupClip, laserClip, trapClip;
    private bool[,] isVisited;
    private Vector2Int lastDirection = Vector2Int.zero;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        EventManager.Instance.AddListener(GlobalEvent.RunAgent, () => RunDFS());
        EventManager.Instance.AddListener(GlobalEvent.PlayerRoasted, () => ProcessDeath());
    }

    public void RunDFS()
    {
        if (GameData.isRunningDFS) return;

        GameData.isRunningDFS = true;
        int size = TilesManager.instance.gridSize;
        isVisited = new bool[size, size];
        StartCoroutine(DFS(TilesManager.instance.startPos));
    }

    private IEnumerator DFS(Vector2Int currPos)
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(currPos);

        while (stack.Count > 0)
        {
            Vector2Int pos = stack.Pop();

            if (!IsValid(pos) || isVisited[pos.x, pos.y])
            {
                continue;
            }

            // Visit this node
            isVisited[pos.x, pos.y] = true;

            // record previous position
            Vector2Int prevPosition = GameData.GetPlayerPosition();
            // update player position
            GameData.SetPlayerPosition(pos);

            // load current tile
            Tile currTile = GameData.GetTile(pos);

            switch(currTile.GetTileType())
            {
                // 1: trap, process death, now it just reloads the scene
                case 1:
                    Debug.Log("You died");
                    tileAudio.clip = trapClip;
                    tileAudio.Play();
                    GameData.isRunningDFS = false;
                    EventManager.Instance.Invoke(GlobalEvent.PlayerRoasted);
                    break;
                // 2: lock, possibly bugged, become normal after unlocked
                case 2:
                    Debug.Log($"Used key, key left {GameData.keyCount}");
                    // use key
                    GameData.UseKey();
                    // update tile type
                    currTile.SetTileType(0);
                    GameData.RegisterTile(pos, currTile);
                    // play laser sound
                    tileAudio.clip = laserClip;
                    tileAudio.Play();
                    break;
                // 3: key, become normal tile after picked up
                case 3:
                    Debug.Log($"Got key, key left {GameData.keyCount}");
                    // get key
                    GameData.GetKey();
                    // u0pdate tile type
                    currTile.SetTileType(0);
                    GameData.RegisterTile(pos, currTile);
                    // play pickup sound
                    tileAudio.clip = pickupClip;
                    tileAudio.Play();
                    break;

                default:
                    break;
            }

            Vector2Int moveDirection = pos - prevPosition;
            Debug.Log($"Player moves towards direction {moveDirection}");
            if (moveDirection != lastDirection)
            {
                if (lastDirection == Vector2Int.zero)
                {
                    lastDirection = moveDirection;
                }
                else
                {
                    bumpAudio.Play();
                }
            }

            lastDirection = moveDirection;

            // Move player to this node
            Vector3 targetPos = TilesManager.instance.GetTilePosition(pos) + offset;
            yield return StartCoroutine(MoveTo(targetPos));

            if (pos == TilesManager.instance.targetPos)
            {
                GameData.isRunningDFS = false;
                EventManager.Instance.Invoke(GlobalEvent.PlayerReachedTarget);
                yield break;
            }

            Vector2Int[] directions = {
                new Vector2Int(pos.x, pos.y - 1),
                new Vector2Int(pos.x - 1, pos.y),
                new Vector2Int(pos.x, pos.y + 1),
                new Vector2Int(pos.x + 1, pos.y)
            };

            bool hasValidNeighbor = false;

            foreach (var neighbor in directions)
            {
                if (IsValid(neighbor) && !isVisited[neighbor.x, neighbor.y])
                {
                    stack.Push(neighbor);
                    hasValidNeighbor = true;
                }
            }

            if (!hasValidNeighbor)
            {
                Debug.Log($"Dead end at {pos}. Searching for next unvisited branch...");

                yield return new WaitForSeconds(1.0f);

                Vector2Int? nextBranch = null;
                foreach (var tile in stack)
                {
                    if (IsValid(tile) && !isVisited[tile.x, tile.y])
                    {
                        nextBranch = tile;
                        break;
                    }
                }

                if (nextBranch.HasValue)
                {
                    Debug.Log($"Teleporting to {nextBranch.Value}");
                    transform.position = TilesManager.instance.GetTilePosition(nextBranch.Value) + offset;
                    yield return new WaitForSeconds(1.0f);
                }
            }

            yield return null;
        }

        Debug.Log("Reach dead end, stack is empty, process death");
        tileAudio.clip = trapClip;
        tileAudio.Play();
        GameData.isRunningDFS = false;
        EventManager.Instance.Invoke(GlobalEvent.PlayerReachedDeadEnd);
    }

    private IEnumerator MoveTo(Vector3 targetPos)
    {
        if (animator != null)
        {
            animator.SetFloat("MoveX", lastDirection.x);
            animator.SetFloat("MoveY", lastDirection.y);
            animator.SetBool("isMoving", true);
        }

        if (walkAudio != null && !walkAudio.isPlaying)
        {
            walkAudio.Play();
        }

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;

        // if (animator != null)
        // {
        //     animator.SetBool("isMoving", false);
        // }

        if (walkAudio != null && walkAudio.isPlaying)
        {
            walkAudio.Stop();
        }
    }

    private bool IsValid(Vector2Int pos)
    {
        return TilesManager.instance.IsValid(pos);
    }

    private void ProcessDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
