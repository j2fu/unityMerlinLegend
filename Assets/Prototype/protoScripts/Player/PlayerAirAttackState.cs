using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    public PlayerAirAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    private float airAttackWindow = 0.5f;
    private float airAttackTimer;
    public override void Enter()
    {
        base.Enter();
        airAttackTimer = airAttackWindow;
        player.rb.gravityScale = 0;
        player.setVelocity(rb.velocity.x, player.airAttackJumpForce);
        player.showAirAttackHitBox = true;

        player.airAttackHitBoxSize.x = 6.0f;
        player.airAttackHitBoxSize.y = 20.0f;
        player.airAttackHitBoxCenterOffset.x =0.0f;
        player.airAttackHitBoxCenterOffset.y =-10.0f;
    }

    public override void Exit()
    {
        base.Exit();
        player.canAirAttack = false;
        player.rb.gravityScale = 10;
        player.showAirAttackHitBox = false;
    }

    public override void Update()
    {
        base.Update();
        airAttackTimer -= Time.deltaTime;
        if (airAttackTimer <= 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
