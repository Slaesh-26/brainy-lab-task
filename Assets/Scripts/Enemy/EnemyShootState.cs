using System.Collections;
using System.Collections.Generic;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class EnemyShootState : EnemyState
{
    private Weapon weapon;
    private float shotDelay;
    private float pursueRadius;
    
    private float lastShotTime;

    public EnemyShootState(Weapon weapon, float shotDelay, float pursueRadius)
    {
        this.weapon = weapon;
        this.shotDelay = shotDelay;
        this.pursueRadius = pursueRadius;
    }

    public override void Update(EnemyFSM enemyFsm)
    {
        Vector3 playerPos = enemyFsm.GetPlayerPos();

        if (Vector3.Distance(enemyFsm.transform.position, playerPos) > pursueRadius)
        {
            enemyFsm.MoveToState<EnemyPursuitState>();
            return;
        }
        
        if (Time.time - lastShotTime > shotDelay)
        {
            Shoot();
        }

        Vector3 toPlayer = playerPos - enemyFsm.transform.position;
        toPlayer.y = 0;
        toPlayer.Normalize();

        enemyFsm.transform.forward = toPlayer;
    }

    private void Shoot()
    {
        weapon.Fire();
        lastShotTime = Time.time;
    }
}
