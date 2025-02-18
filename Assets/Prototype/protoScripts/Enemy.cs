using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    public GameObject attackHitBox;
    public Vector2 attackHitBoxCenterOffset;
    public Vector2 attackHitBoxSize;
    public bool showAttackHitBox;


    public int facingDir { get; private set; } = -1;
    public bool facingRight { get; private set; } = false;
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    #endregion

    #region States
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion

    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        stateMachine.currentState.Update();
    }

    #region Velocity
    public void setVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);
    #endregion

    #region Collision
    public bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        //Ground Check
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        //attack box check
        float attackBoxCenterX;
        float attackBoxCenterY;
        if (showAttackHitBox)
        {
            if (facingRight)
            {
                attackBoxCenterX = transform.position.x + attackHitBoxCenterOffset.x;
                attackBoxCenterY = transform.position.y + attackHitBoxCenterOffset.y;

            }
            else
            {
                attackBoxCenterX = transform.position.x - attackHitBoxCenterOffset.x;
                attackBoxCenterY = transform.position.y + attackHitBoxCenterOffset.y;

            }
            Vector2 attackBoxCenter = new Vector2(attackBoxCenterX, attackBoxCenterY);
            Gizmos.DrawWireCube((Vector2)attackBoxCenter, attackHitBoxSize);

        }
    }
    #endregion

    #region Filp
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void FlipController(float _x)
    {
        if (facingRight && _x < 0)
        {
            Flip();
        }
        else if (!facingRight && _x > 0)
        {
            Flip();
        }
    }
    #endregion
}
