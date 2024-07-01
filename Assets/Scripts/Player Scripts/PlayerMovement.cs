using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField]
    private float moveSpeed = 0f;

    [SerializeField]
    private float minBound_X = -71f, maxBound_X = 71f, minBound_Y = -0.6f, maxBound_Y = 2.2f;

    private Vector3 tempPos;

    private float xAxis, yAxis;

    private PlayerAnimations playerAnimations;

    [SerializeField]
    private float shootWaitTime = 0.5f;

    private float waitBeforeShooting;

    [SerializeField]
    private float moveWaitTime = 0.3f;

    private float waitBeforeMoving;

    private bool canMove = true;
    private PlayerShootingManager playerShootingManager;
    private bool playerDied;


    [SerializeField]
    private FixedJoystick joystick;


   


    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerShootingManager = GetComponent<PlayerShootingManager>();
    }

    void Update()
    {
        if (playerDied)
            return;


        HandleAnimation();
        HandleMovement();
        HandleFacingDirection();
        
        CheckIfCanMove();

    }

    void HandleMovement()
    {


        if (!canMove)
            return;



        xAxis = joystick.Horizontal;
        yAxis = joystick.Vertical;
        tempPos = transform.position;
        tempPos.x += xAxis * moveSpeed * Time.deltaTime;
        tempPos.y += yAxis * moveSpeed * Time.deltaTime;

        if (tempPos.x < minBound_X)
            tempPos.x = minBound_X;

        if (tempPos.x > maxBound_X)
            tempPos.x = maxBound_X;

        if (tempPos.y < minBound_Y)
            tempPos.y = minBound_Y;

        if (tempPos.y > maxBound_Y)
            tempPos.y = maxBound_Y;


        transform.position = tempPos;
    }


    void HandleAnimation()
    {
        if (!canMove)
            return;

        if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
        {
            playerAnimations.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
        }

        else
        {
            playerAnimations.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }

    }

    //Handle Shooting With Button:
    public void OnShootButtonClicked()
    {
        if (Time.time > waitBeforeShooting)
        {
            Shoot();
        }
    }


    void HandleFacingDirection()
    {
        if (xAxis > 0)
        {
            playerAnimations.SetfFacingDirection(true);
        }
        else if (xAxis < 0)
        {
            playerAnimations.SetfFacingDirection(false);
        }
    }

    void StopMovement()
    {
        canMove = false;
        waitBeforeMoving = Time.time + moveWaitTime;
    }

    void Shoot()
    {
        waitBeforeShooting = Time.time + shootWaitTime;
        StopMovement();
        playerAnimations.PlayAnimation(TagManager.SHOOT_ANIMATION_NAME);
        playerShootingManager.Shoot(transform.localScale.x);
    }


    void CheckIfCanMove()
    {
        if(Time.time > waitBeforeMoving)
        {
            canMove = true;
        }
    }

 


    public void PlayerDied()
    {
        playerDied = true;
        playerAnimations.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        Invoke("DestroyPlayerAfterDelay",2f);
    }


    void DestroyPlayerAfterDelay()
    {
        Destroy(gameObject);
    }



}
