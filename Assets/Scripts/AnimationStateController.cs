using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isWalkbackHash;
    int isWalkingrightHash;
    int isWalkingleftHash;
    int isRunningHash;
    int isDoubleJumpingHash;

    public bool isDoubleJumping;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isWalkbackHash = Animator.StringToHash("isWalkback");
        isWalkingleftHash = Animator.StringToHash("isWalkingleft");
        isWalkingrightHash = Animator.StringToHash("isWalkingright");
        isRunningHash = Animator.StringToHash("isRunning");
        isDoubleJumpingHash = Animator.StringToHash("isDoubleJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isWalkback = animator.GetBool(isWalkbackHash);
        bool isWalkingleft = animator.GetBool(isWalkingleftHash);
        bool isWalkingright = animator.GetBool(isWalkingrightHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool forwardPressed = Input.GetKey("w");
        bool backPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetButtonDown("Jump");

        isDoubleJumping = animator.GetBool(isDoubleJumpingHash);

        //forward
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        //backward
        if (!isWalkback && backPressed)
        {
            animator.SetBool(isWalkbackHash, true);
        }

        if (isWalkback && !backPressed)
        {
            animator.SetBool(isWalkbackHash, false);
        }

        //left
        if (!isWalkingleft && leftPressed)
        {
            animator.SetBool(isWalkingleftHash, true);
        }

        if (isWalkingleft && !leftPressed)
        {
            animator.SetBool(isWalkingleftHash, false);
        }

        //right
        if (!isWalkingright && rightPressed)
        {
            animator.SetBool(isWalkingrightHash, true);
        }

        if (isWalkingright && !rightPressed)
        {
            animator.SetBool(isWalkingrightHash, false);
        }

        //running
        if (!isRunning && shiftPressed)
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && !shiftPressed)
        {
            animator.SetBool(isRunningHash, false);
        }

        //double jump
        if (!isDoubleJumping && jumpPressed)
        {
            animator.SetBool(isDoubleJumpingHash, true);
        }

        if (isDoubleJumping)
        {
            animator.SetBool(isDoubleJumpingHash, false);
        }
    }
}
