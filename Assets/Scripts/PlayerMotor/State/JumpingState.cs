using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : BaseState
{
    public float jumpForce = 7.0f;
    public override void Construct()
    {
        motor.verticalVelocity = jumpForce;
        motor.anim?.SetTrigger("Jump"); // anim? -> null이면 여기서 중지하지만 null이 아니라면 계속하고 설정 트리거를 수행.

    }

    public override Vector3 ProcessMotion()
    {
        motor.ApplyGravity();


        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }

        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }

        if (motor.verticalVelocity < 0)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }
    }
}
