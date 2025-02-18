using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefenseState : PlayerState
{
    public PlayerDefenseState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.setVelocity(0f, 0f);
        player.showDefenseHitBox = true;
        player.defenseHitBoxSize.x = 6.0f;
        player.defenseHitBoxSize.y = 6.0f;
        player.defenseHitBoxCenterOffset.x = 3.0f;
        player.defenseHitBoxCenterOffset.y = 2.0f;
    }

    public override void Exit()
    {
        base.Exit();
        //reset color
        player.spriteRenderer.color = Color.white;
        player.showDefenseHitBox = false;
    }

    public override void Update()
    {
        base.Update();

        //prototype show parry invincible
        player.spriteRenderer.color = Color.blue;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            stateMachine.ChangeState(player.idleState);  // Return to Grounded when space is released
        }
    }
}
