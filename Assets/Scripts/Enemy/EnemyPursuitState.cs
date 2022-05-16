using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPursuitState : EnemyState
{
	private float followRadius;
	private NavMeshAgent navMeshAgent;
	private float followRadiusWidth = 0.5f;

	private Vector3 lastPlayerPos;
	
	public EnemyPursuitState(float followRadius, NavMeshAgent navMeshAgent)
	{
		this.followRadius = followRadius;
		this.navMeshAgent = navMeshAgent;
	}
	
	public override void Enter(EnemyFSM enemyFsm)
	{
		lastPlayerPos = Vector3.positiveInfinity;
	}

	public override void Update(EnemyFSM enemyFsm)
	{
		Vector3 playerPos = enemyFsm.GetPlayerPos();
		
		if (Vector3.Distance(playerPos, lastPlayerPos) > 1f)
		{
			navMeshAgent.SetDestination(playerPos);
			lastPlayerPos = playerPos;
		}

		if (Vector3.Distance(enemyFsm.transform.position, playerPos) < followRadius)
		{
			enemyFsm.MoveToState<EnemyShootState>();
		}
	}

	public override void Exit(EnemyFSM enemyFsm)
	{
		navMeshAgent.ResetPath();
	}
}
