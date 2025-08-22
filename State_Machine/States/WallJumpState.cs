using Godot;
using System;

public partial class WallJumpState<T> : State_base<T> where T : Player
{
	private float jumpVelocity = -400f;
    private float wallJumpHorizontal = 300f;  //Impulso horizontal opuesto a la pared
    private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public WallJumpState(T context, State_machine<T> SM) : base(context, SM) { } //SM = StateMachine

    public override void Start()
    {
		//variable que almacena la direccion de la pared
        float wallDir = Context.GetWallDirection();  //-1 izquierda, 1 derecha
        Context.Velocity = new Vector2(-wallDir * wallJumpHorizontal, jumpVelocity);  //Impulso opuesto
        Context.facingRight = !Context.facingRight;  //Flip dirección
        Context.animationPlayer.Play("DoubleJump");
        Context.jumpCount++;  //Cuenta como un salto
    }

    public override void PhysicsUpdate(double delta)
    {
        Context.Velocity += new Vector2(0, gravity * (float)delta);

		//si la velocidad en Y es mayor a 0, cambia al estado fall
        if (Context.Velocity.Y > 0)
		{
			SM.ChangeState(playerState.Fall);
		}

        float input = Input.GetAxis("move_left", "move_right");
        Context.facingRight = input > 0;
        float airSpeed = Input.IsActionPressed("run") ? 400f : 200f;
        Context.Velocity = new Vector2(input * airSpeed, Context.Velocity.Y);

        Context.MoveAndSlide();

		//si esta en el piso, cambia al estado idle
        if (Context.IsOnFloor())
		{
			SM.ChangeState(playerState.Idle);
		}
    }
}
