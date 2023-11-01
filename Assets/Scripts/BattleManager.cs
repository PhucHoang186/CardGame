using System.Collections;
using System.Collections.Generic;
using Entity;
using Grid;
using NaughtyAttributes;
using UnityEngine;


public enum BattleState
{
    PlayerTurn,
    EnemyTurn,
    Pause,
    Start_Battle,
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    [SerializeField] GridManager grid;
    // temporary
    [SerializeField] EntityCard playerCardPrefab;
    [SerializeField] EntityCard enemyCardPrefab;

    private BattleState battleState = BattleState.Start_Battle;

    [Button]
    public void CheckFunction()
    {
        grid.GetCardinalNeighborNodes(grid.Nodes[Vector2Int.one * 3], 2);
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        HandleState();
    }


    private void HandleState()
    {
        switch (battleState)
        {
            case BattleState.Start_Battle:
                InitEntitiesCard();
                break;
            case BattleState.PlayerTurn:
                PLayerTurn();
                break;
            case BattleState.EnemyTurn:
                EnemyTurn();
                break;
            case BattleState.Pause:
                break;
            default:
                break;
        }
    }

    public void ChangeBattleState(BattleState newState)
    {
        if (newState == battleState)
            return;
        battleState = newState;
    }

    private void PLayerTurn()
    {
        // BattleUIManager.Instance.BlockInput(false);
    }

    private void EnemyTurn()
    {
        // BattleUIManager.Instance.BlockInput(true);
    }

    private void InitEntitiesCard()
    {
        grid.Init();
        InitEntityCard();
        ChangeBattleState(BattleState.PlayerTurn);
    }

    // temporary
    private void InitEntityCard()
    {
        var nodes = grid.Nodes;
        var player = Instantiate(playerCardPrefab, transform);
        // player.PlaceToNode(nodes[0]);
        var enemy = Instantiate(enemyCardPrefab, transform);
        // enemy.PlaceToNode(nodes[^1]);
    }
}
