using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private static CharacterMovement _instance;

    public static CharacterMovement Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CharacterMovement>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("CharacterMovement");
                    _instance = go.AddComponent<CharacterMovement>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] BoardTile[] tiles;
    [SerializeField] List<BoardTile> resourceTiles;
    [SerializeField] string[] animateName = { "idling", "run", "stun", "skip_jump" };
    public bool isStunning = false, isReverse = false;
    private int currentPositionIndex = 0;
    private SkeletonAnimation skeAnimation;
    private List<string> animationName;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        skeAnimation = GetComponent<SkeletonAnimation>();
        tiles = FindObjectsOfType<BoardTile>();
        Array.Sort(tiles, (tile1, tile2) => tile1.name.CompareTo(tile2.name));
        transform.position = tiles[0].transform.position;
        resourceTiles = new List<BoardTile>();
        
        animationName = animateName.ToList();
        foreach (BoardTile tile in tiles)
        {
            if (tile.gameObject.GetComponent<Resource>() != null)
            {
                resourceTiles.Add(tile);
            }
        }
        skeAnimation.AnimationState.SetAnimation(0, animationName[0], true);
    }

    public void MovePlayer(int steps)
    {
        StartCoroutine(MovePlayerCoroutine(steps));
    }

    private IEnumerator MovePlayerCoroutine(int steps)
    {
        skeAnimation.AnimationState.SetAnimation(0, animationName[1], true);
        for (int i = 0; i < steps; i++)
        {
            currentPositionIndex = (currentPositionIndex + 1) % tiles.Length;
            float timer = 0f;
            float duration = .5f;
            
            while (timer < duration)
            {
                if(transform.position.x < tiles[currentPositionIndex].transform.position.x)
                {
                    transform.localScale = new Vector3(1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if(transform.position.x > tiles[currentPositionIndex].transform.position.x)
                {
                    transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                transform.position = Vector2.Lerp(transform.position, tiles[currentPositionIndex].transform.position, timer/duration);
                timer += Time.deltaTime;
                yield return null;
            }
            
        }
        isStunning = false;
        CheckEffect();
        DiceUI.Instance.DisplayDice();
        CheckIndleOrStunning();
    }
    public void CheckIndleOrStunning()
    {
        if (isStunning || isReverse)
        {
            skeAnimation.AnimationState.SetAnimation(0, animationName[2], true);
        }
        else
        {
            skeAnimation.AnimationState.SetAnimation(0, animationName[0], true);
        }
    }
    public void BackPlayer(int steps)
    {
        StartCoroutine(BackPlayerCoroutine(steps));
    }
    private IEnumerator BackPlayerCoroutine(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            currentPositionIndex = currentPositionIndex - 1;
            if(currentPositionIndex < 0)
            {
                currentPositionIndex += tiles.Length;
            }
            float timer = 0f;
            float duration = .2f;
            while (timer < duration)
            {
                transform.position = Vector2.Lerp(transform.position, tiles[currentPositionIndex].transform.position, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }
        }
        isStunning = false;
        isReverse = false;
        DiceController.Instance.canRoll = true;
        DiceUI.Instance.DisplayDice();
        CheckIndleOrStunning();
    }
    private void CheckEffect()
    {
        DiceController.Instance.canRoll = true;
        BoardTile currentTile = tiles[currentPositionIndex].GetComponent<BoardTile>();
        if (currentTile != null)
        {
            currentTile.TriggerEffect();
        }
    }

    public void TriggerRandomResourceTile()
    {
        int randomIndex = 0;
        
        randomIndex = UnityEngine.Random.Range(0, resourceTiles.Count);
        
        BoardTile currentTile = resourceTiles[randomIndex].GetComponent<BoardTile>();
        if (currentTile != null)
        {
            currentTile.TriggerEffect();
        }
    }

    public void ChangeTileLevel(bool isIncrese)
    {
        int randomIndex = 0;
        randomIndex = UnityEngine.Random.Range(0, resourceTiles.Count);
        BoardTile currentTile = resourceTiles[randomIndex].GetComponent<BoardTile>();
        if (currentTile != null)
        {
            Resource currResource = currentTile.GetComponent<Resource>();
            if (currResource != null)
            {
                currResource.ChangeLevel(isIncrese);
            }
        }
    }
    public void TeleportToStart()
    {
        currentPositionIndex = 0;
        transform.position = tiles[0].transform.position;
        StartCoroutine(TeleAnimate());
    }
    private IEnumerator TeleAnimate()
    {
        skeAnimation.AnimationState.SetAnimation(0, animationName[3], false);
        yield return new WaitForSeconds(.5f);
        CheckIndleOrStunning();
    }
}
