using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private float secondaryAttackWindow = 0.2f;
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && player.currentMagicStateID == 1)
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Time.time <= player.lastTimeAttacked + secondaryAttackWindow && player.currentMagicStateID == 123)
        {
            stateMachine.ChangeState(player.secondaryAttackState);
        }

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        if (Input.GetKeyDown(KeyCode.W) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.currentMagicStateID == 3)
        {
            stateMachine.ChangeState(player.parryState);
        }
    }
}
