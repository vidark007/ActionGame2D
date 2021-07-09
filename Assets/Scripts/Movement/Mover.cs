using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    [SerializeField] float speed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveAmout;

    private Animator animator;
    CharacterIdentifier identifier;

    bool enemyIsMovingAnimation = false;

    enum MoverAnimation
    {
        isRunnging,
        isIdle
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        identifier = GetComponent<CharacterIdentifier>();
    }

    private void Update()
    {

        
    }

    public void Move(Vector2 moveTo, float factor)
    {
        if (identifier.IsPlayer())
        {
            moveAmout = moveTo.normalized * (speed/factor);
        }
        else if(!identifier.IsPlayer())
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTo, (speed / factor) * Time.deltaTime);
            enemyIsMovingAnimation = true;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmout * Time.fixedDeltaTime);

        if (moveAmout != Vector2.zero || enemyIsMovingAnimation)
        {
            PlayerAnimationController(MoverAnimation.isRunnging, true);
        }
        else
        {
            PlayerAnimationController(MoverAnimation.isRunnging, false);
        }
    }

    private void PlayerAnimationController(MoverAnimation animation, bool isMoving)
    {
        int animationId = Animator.StringToHash(animation.ToString());

        animator.SetBool(animationId, isMoving);
    }

    public void StopEnemyMovingAnimation()
    {
        enemyIsMovingAnimation = false;
    }

}