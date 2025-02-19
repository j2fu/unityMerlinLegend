using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirWalkState : PlayerState
{
    private float airWalkWindow = 2f;
    private float airWalkTimer;

    public PlayerAirWalkState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        airWalkTimer = airWalkWindow;
        rb.gravityScale = 0;
        player.setVelocity(0, 0);
        player.canAirAttack = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.canAirWalk = false;
        rb.gravityScale = 10;
        player.setVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();
        airWalkTimer -= Time.deltaTime;
        player.setVelocity(xInput * player.moveSpeed, 0);
        if (airWalkTimer <= 0)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (Input.GetKeyDown(KeyCode.Space) && player.currentMagicStateID == 23 && player.canAirAttack)
        {
            stateMachine.ChangeState(player.airAttackState);
        }
    }
}
