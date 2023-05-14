using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    protected Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void SetPlayerMovementAnimation(bool val)
    {
        playerAnimator.SetBool("Moving", val);
    }

    public void AnimatePlayer(float speed)
    {
        SetPlayerMovementAnimation(speed > 0);
    }
}
