using Godot;
using System;

public partial class JumpState<T> : State_base<T> where T : Player
{
	private float JumpVelocity = -400f;
    private float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public JumpState(T context, State_machine<T> SM) : base(context, SM) { }

    public override void Start()
    {
        Context.Velocity = new Vector2(Context.Velocity.X, JumpVelocity);
        Context.jumpCount++;
        if (Context.jumpCount == 2) Context.animationPlayer.Play("DoubleJump");
        Context.animationPlayer.Play("Jump");
    }

    public override void PhysicsUpdate(double delta)
    {   

        Context.Velocity += new Vector2(0, Gravity * (float)delta);

        if (Context.IsOnWall())
        {
            SM.ChangeState(playerState.WallSlide);
            return;
        }
        
        //si la velocidad en Y es mayora 0, el estado cambia a fall
        if (Context.Velocity.Y > 0)
        {
            SM.ChangeState(playerState.Fall);
        }

        //si al precionar una tecla y la cantidad de saltos es menor a la cantidad maxima de saltos
        if (Input.IsActionJustPressed("jump") && Context.jumpCount < Context.maxJumps)
        {
            //le añade una velocidad
            Context.Velocity = new Vector2(Context.Velocity.X, JumpVelocity);
            //suma un salto al conteo
            Context.jumpCount++;
            Context.animationPlayer.Play("DoubleJump");
            return;
        }

        float input = Input.GetAxis("move_left", "move_right");
        Context.facingRight = input > 0;
        float airSpeed = Input.IsActionPressed("run") ? 400f : 200f;
        Context.Velocity = new Vector2(input * airSpeed, Context.Velocity.Y);

        Context.MoveAndSlide();

        if (Context.IsOnFloor())
        {
            SM.ChangeState(playerState.Idle);
        }
    }
}
