using Godot;
using System;

public partial class FallState<T> : State_base<T> where T : Player
{
	private float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public FallState(T context, State_machine<T> SM) : base(context, SM) { }

    public override void Start()
    {
        Context.animationPlayer.Play("Fall");
    }

    public override void PhysicsUpdate(double delta)
    {
        Context.Velocity += new Vector2(0, Gravity * (float)delta);

        if (Context.IsOnWall())
        {
            SM.ChangeState(playerState.WallSlide);
            return;
        }

        float input = Input.GetAxis("move_left", "move_right");
        Context.facingRight = input > 0;
        float airSpeed = Input.IsActionPressed("run") ? 400f : 200f;
        Context.Velocity = new Vector2(input * airSpeed, Context.Velocity.Y);

        Context.MoveAndSlide();

        if (Context.IsOnFloor())
        {
            Context.jumpCount = 0;
            float currentInput = Input.GetAxis("move_left", "move_right");
            if (currentInput != 0)
            {
                if (Input.IsActionPressed("run"))
                {
                    SM.ChangeState(playerState.Run);
                }
                else
                {
                    SM.ChangeState(playerState.Walk);
                }
            }
            else
            {
                SM.ChangeState(playerState.Idle);
            }
        }
    }
}
