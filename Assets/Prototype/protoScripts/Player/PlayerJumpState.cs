using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(player.currentMagicStateID == 32)
        {
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce * 1.5f);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.currentMagicStateID == 2)
        {
            stateMachine.ChangeState(player.airParryState);
        }
    }
}
