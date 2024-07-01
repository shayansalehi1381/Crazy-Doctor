using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTarget;


    [SerializeField]
    private float smoothSpeed = 2f;

    [SerializeField]
    private float playerBoundMin_Y = -1f, playerBoundMin_X = -65, playerBoundMax_X = 65F;


    [SerializeField]
    private float Y_Gap = 2f;


    private Vector3 temPos;


    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void Update()
    {
        if (!playerTarget)
        {
            return;
        }

        temPos = transform.position;

        if (playerTarget.position.y <= playerBoundMin_Y)
        {
            temPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x,
                playerTarget.position.y, -10f), Time.deltaTime * smoothSpeed);
        }

        else
        {
            temPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x,
              playerTarget.position.y + Y_Gap, -10f), Time.deltaTime * smoothSpeed);
        }

        if (temPos.x > playerBoundMax_X)
        {
            temPos.x = playerBoundMax_X;
        }


        if (temPos.x < playerBoundMin_X)
        {
            temPos.x = playerBoundMin_X;
        }

        transform.position = temPos;
    }
}
