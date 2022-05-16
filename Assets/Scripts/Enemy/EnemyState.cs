using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public virtual void Enter(EnemyFSM enemyFsm) { }
    public virtual void Update(EnemyFSM enemyFsm) { }
    public virtual void Exit(EnemyFSM enemyFsm) { }
}
