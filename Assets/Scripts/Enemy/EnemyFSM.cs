using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour, IRestartable
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Weapon weapon;
    [SerializeField] private float followRadius = 6f;
    [SerializeField] private float shotDelay = 1f;
    
    private EnemyState current;
    private List<EnemyState> availableStates;
    private Transform player;

    private Vector3 initialPos;
    
    public void MoveToState<T>() where T : EnemyState
    {
        EnemyState next = availableStates.Find(s => s is T);

        if (next == null)
        {
            Debug.LogError($"State {typeof(T)} not found");
            return;
        }
        
        current?.Exit(this);
        current = next;
        current.Enter(this);
    }

    public Vector3 GetPlayerPos()
    {
        return player.position;
    }

    private void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("Player not found");
            return;
        }

        player = playerMovement.transform;

        EnemyPursuitState pursuitState = new EnemyPursuitState(followRadius, navMeshAgent);
        EnemyShootState shootState = new EnemyShootState(weapon, shotDelay, followRadius);

        availableStates = new List<EnemyState>()
        {
            pursuitState, shootState
        };
        
        RestartController.instance.AddListener(this);
        initialPos = transform.position;
        
        MoveToState<EnemyPursuitState>();
    }

    private void Update()
    {
        current?.Update(this);
    }

    public void OnRestart()
    {
        transform.position = initialPos;
        MoveToState<EnemyPursuitState>();
    }
}
