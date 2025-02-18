using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleEnemy : Enemy
{

    #region States
    public CapsuleIdleState idleState { get; private set; }
    public CapsuleAttackState attackState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new CapsuleIdleState(this, stateMachine, "Idle", this);
        attackState = new CapsuleAttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
