using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float xInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;
    protected bool followUpEnabled;

    //for GUI TESTING
    public string AnimBoolNameGUI => animBoolName;


    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.animBoolName = _animBoolName;  
        this.stateMachine = _stateMachine;
        this.player = _player;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
        followUpEnabled = false;
    }
    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit() 
    {
        player.anim.SetBool(animBoolName, false);
        followUpEnabled = false;
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

    public virtual void attackEnableTrigger()
    {
        followUpEnabled = true;
    }
}
