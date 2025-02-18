using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleIdleState : EnemyState
{
    private CapsuleEnemy enemy;
    public CapsuleIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, CapsuleEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = Random.Range(2f, 3f);
        enemy.spriteRenderer.color = Color.green;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(enemy.attackState);
        }
    }
}
