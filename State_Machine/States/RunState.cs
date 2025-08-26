using Godot;
using System;

public partial class RunState<T> : State_base<T> where T : Player
{
	private float Speed = 400f; // Velocidad de correr (mayor que walk)

    public RunState(T context, State_machine<T> SM) : base(context, SM) { }

    public override void Start()
    {
        Context.animationPlayer.Play("Run");
    }

    public override void PhysicsUpdate(double delta)
    {
        if (!Context.IsOnFloor())
        {
            SM.ChangeState(playerState.Fall);
            return;
        }

        float input = Input.GetAxis("move_left", "move_right");
        if (input == 0)
        {
            SM.ChangeState(playerState.Idle);
            return;
        }
        if (Input.IsActionJustPressed("jump"))
        {
            SM.ChangeState(playerState.Jump);
            return;
        }
        if (!Input.IsActionPressed("run"))
        {
            SM.ChangeState(playerState.Walk);
            return;
        }

        Context.facingRight = input > 0;
        Context.Velocity = new Vector2(input * Speed, Context.Velocity.Y);
        Context.MoveAndSlide();
    }
}
