using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked; 
    private float comboWindow = 1f;
   

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }
        player.comboCounter = comboCounter;
        player.anim.SetInteger("comboCounter", comboCounter);

        float attackDir = player.facingDir;
        if(xInput != 0)
        {
            attackDir = xInput;
        }
        player.setVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        //update player attackHitBox
        player.showAttackHitBox = true;
        if (comboCounter == 0)
        {
            player.attackHitBoxSize.x = 8.0f;
            player.attackHitBoxSize.y = 10.0f;
            player.attackHitBoxCenterOffset.x = 2f;
            player.attackHitBoxCenterOffset.y = 3.0f;
        }
        else if (comboCounter == 1)
        {
            player.attackHitBoxSize.x = 15.0f;
            player.attackHitBoxSize.y = 8.0f;
            player.attackHitBoxCenterOffset.x = 4.0f;
            player.attackHitBoxCenterOffset.y = 0.0f;
        }
        else if (comboCounter == 2)
        {
            player.attackHitBoxSize.x = 10.0f;
            player.attackHitBoxSize.y = 10.0f;
            player.attackHitBoxCenterOffset.x = 4.0f;
            player.attackHitBoxCenterOffset.y = 4.0f;
        }


    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine(player.Busyfor(0.1f));
        comboCounter++;
        lastTimeAttacked = Time.time;
        player.lastTimeAttacked = lastTimeAttacked;

        //reset attackbox size
        player.attackHitBoxSize.x = 6.0f;
        player.attackHitBoxSize.y = 6.0f;
        player.attackHitBoxCenterOffset.x = 2.0f;
        player.attackHitBoxCenterOffset.y = 0.0f;
        player.showAttackHitBox = false;

    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.ZeroVelocity();
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }


        //attack box contruction
        float attackBoxCenterX;
        float attackBoxCenterY;

        if (player.facingRight)
        {
            attackBoxCenterX = player.transform.position.x + player.attackHitBoxCenterOffset.x;
            attackBoxCenterY = player.transform.position.y + player.attackHitBoxCenterOffset.y;

        }
        else
        {
            attackBoxCenterX = player.transform.position.x - player.attackHitBoxCenterOffset.x;
            attackBoxCenterY = player.transform.position.y + player.attackHitBoxCenterOffset.y;

        }
        Vector2 attackBoxCenter = new Vector2(attackBoxCenterX, attackBoxCenterY);
        Vector2 attackBoxBottomLeft = new Vector2(attackBoxCenter.x - player.attackHitBoxSize.x / 2, attackBoxCenter.y - player.attackHitBoxSize.y / 2);
        Vector2 attackBoxTopRight = new Vector2(attackBoxCenter.x + player.attackHitBoxSize.x / 2, attackBoxCenter.y + player.attackHitBoxSize.y / 2);

    }

        
}
