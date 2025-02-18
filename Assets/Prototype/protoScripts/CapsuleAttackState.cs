using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleAttackState : EnemyState
{
    private CapsuleEnemy enemy;
    public CapsuleAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, CapsuleEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.5f;
        enemy.spriteRenderer.color = Color.red;
        enemy.showAttackHitBox = true;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.showAttackHitBox = false;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
