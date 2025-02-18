using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirParryState : PlayerState
{
    public PlayerAirParryState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    private float parryDuration = 0.4f;
    private float parryTimer;


    public override void Enter()
    {
        base.Enter();
        parryTimer = parryDuration;
        player.setVelocity(0, 0);
        player.rb.gravityScale = 0;
        player.showAirParryHitBox = true;
        player.airParryHitBoxRadius = 4.0f;
    }

    public override void Exit()
    {
        base.Exit();

        //reset player color
        player.spriteRenderer.color = Color.white;
        player.showAirParryHitBox = false;
        player.rb.gravityScale = 10;
    }

    public override void Update()
    {
        base.Update();

        //prototype show parry invincible
        player.spriteRenderer.color = Color.yellow;

        parryTimer -= Time.deltaTime;
        if (parryTimer <= 0)
        {
            stateMachine.ChangeState(player.airState);  
        }
    }
}
