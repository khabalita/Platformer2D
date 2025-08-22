using Godot;
using System;

public partial class WallSlideState<T> : State_base<T> where T : Player
{
	private float slideSpeed = 100f;  // Velocidad de deslizamiento lenta

    public WallSlideState(T context, State_machine<T> SM) : base(context, SM) { }

    public override void Start()
    {
        float wallDir = Context.GetWallDirection();
        Context.facingRight = wallDir < 0;
        Context.animationPlayer.Play("WallJump");
        Context.jumpCount = 0; //Resetea los saltos
    }

    public override void PhysicsUpdate(double delta)
    {
		//Si no esta en la pared, cambia al estado fall
        if (!Context.IsOnWall())
		{
			SM.ChangeState(playerState.Fall);
			return;
		}
		//si esta en el piso, cambia al estado idle
        if (Context.IsOnFloor())
		{
			SM.ChangeState(playerState.Idle);
			return;
		}
		//si se apreta espacio para saltar estando en una pared, cambia al estado wallJuamp
        if (Input.IsActionJustPressed("jump"))  //Salto desde pared
        {
            SM.ChangeState(playerState.WallJump);
            return;
        }

        //Deslizamiento lento por pared
        Context.Velocity = new Vector2(Context.Velocity.X, slideSpeed);
        Context.MoveAndSlide();
    }

    public override void Stop()
    {
        //Detener animación
    }
}
