using Godot;
using System;

public class IdleState<T> : State_base<T> where T : Player
{
	public IdleState(T context, State_machine<T> SM) : base(context, SM) { } //SM = StateMachine

    public override void Start()
    {
        Context.animationPlayer.Play("Idle");
	}
	
	public override void PhysicsUpdate(double delta)
    {
        if (!Context.IsOnFloor())
        {
            SM.ChangeState(playerState.Fall);
            return;
        }

        float input = Input.GetAxis("move_left", "move_right");
        
        if (input != 0)
        {
            Context.facingRight = input > 0;
            if (Input.IsActionPressed("run"))
            {
                SM.ChangeState(playerState.Run);
            }
            else
            {
                SM.ChangeState(playerState.Walk);
            }
        }
        if (Input.IsActionJustPressed("jump")) // Salto
        {
            SM.ChangeState(playerState.Jump);
        }

        Context.Velocity = new Vector2(0, Context.Velocity.Y);
        Context.MoveAndSlide();
    }
}
