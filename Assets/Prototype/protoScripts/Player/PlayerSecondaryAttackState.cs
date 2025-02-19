using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondaryAttackState : PlayerState
{
    private int secondaryAttackCombo;
    public PlayerSecondaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        secondaryAttackCombo = player.comboCounter;
        player.anim.SetInteger("FollowUpCounter", secondaryAttackCombo);

        

        if (secondaryAttackCombo == 0)
        {
            player.followUpHitBoxSize.x = 12.0f;
            player.followUpHitBoxSize.y = 8.0f;
            player.followUpHitBoxCenterOffset.x = 4.0f;
            player.followUpHitBoxCenterOffset.y = 1.5f;
        }
        else if (secondaryAttackCombo == 1)
        {
            player.followUpHitBoxSize.x = 30.0f;
            player.followUpHitBoxSize.y = 3.0f;
            player.followUpHitBoxCenterOffset.x = 15.0f;
            player.followUpHitBoxCenterOffset.y = 0.0f;
        }
        else if (secondaryAttackCombo == 2)
        {
            player.followUpHitBoxSize.x = 10.0f;
            player.followUpHitBoxSize.y = 30.0f;
            player.followUpHitBoxCenterOffset.x = 10.0f;
            player.followUpHitBoxCenterOffset.y = 7.0f;
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.showFollowUpHitBox = false;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if(followUpEnabled)
        {
            player.showFollowUpHitBox = true;
        }

        //follow up attackBox Construction
        float followUpBoxCenterX;
        float followUpBoxCenterY;
        if (player.facingRight)
        {
            followUpBoxCenterX = player.transform.position.x + player.followUpHitBoxCenterOffset.x;
            followUpBoxCenterY = player.transform.position.y + player.followUpHitBoxCenterOffset.y;
        }
        else
        {
            followUpBoxCenterX = player.transform.position.x - player.followUpHitBoxCenterOffset.x;
            followUpBoxCenterY = player.transform.position.y + player.followUpHitBoxCenterOffset.y;
        }
        Vector2 followUpBoxCenter = new Vector2(followUpBoxCenterX, followUpBoxCenterY);
        Vector2 followUpBoxBottomLeft = new Vector2(followUpBoxCenter.x - player.followUpHitBoxSize.x / 2, followUpBoxCenter.y - player.followUpHitBoxSize.y / 2);
        Vector2 followUpBoxTopRight = new Vector2(followUpBoxCenter.x + player.followUpHitBoxSize.x / 2, followUpBoxCenter.y + player.followUpHitBoxSize.y / 2);
        Collider2D[] detectedObjects = Physics2D.OverlapAreaAll(followUpBoxBottomLeft, followUpBoxTopRight, player.whatIsDamageable);
        foreach (Collider2D collider in detectedObjects)
        {
            Debug.Log("Follow Up Hit: " + collider.name);
        }
    }
}
