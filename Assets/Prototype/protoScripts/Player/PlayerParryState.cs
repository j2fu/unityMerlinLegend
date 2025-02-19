using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    public PlayerParryState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    private float parryDuration = 0.2f;
    private float parryTimer;

    public override void Enter()
    {
        base.Enter();

        parryTimer = parryDuration;
        player.setVelocity(0, 0);
        player.showParryHitBox = true;
        player.parryHitBoxSize.x = 9.0f;
        player.parryHitBoxSize.y = 9.0f;
        player.parryHitBoxCenterOffset.x = 3.0f;
        player.parryHitBoxCenterOffset.y = 3.0f;
    }

    public override void Exit()
    {
        base.Exit();
        //reset player color
        player.spriteRenderer.color = Color.white;
        player.showParryHitBox = false;
    }

    public override void Update()
    {
        base.Update();
        //prototype show parry invincible
        player.spriteRenderer.color = Color.yellow;


        parryTimer -= Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (parryTimer <= 0)
        {
            if (Input.GetKey(KeyCode.Space) && player.currentMagicStateID == 3)
            {
                stateMachine.ChangeState(player.defenseState);  
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }

        //Wind Shield parry hitbox contruction


    }
}
