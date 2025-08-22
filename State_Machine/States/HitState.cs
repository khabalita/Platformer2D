using Godot;
using System;

public partial class HitState<T> : State_base<T> where T : Player
{
	private float hitTimer = 0.5f;  //Duración del stun al ser golpeado
    private float knockbackSpeed = 300f;  //Fuerza de knockback (retroceso)

    public HitState(T context, State_machine<T> SM) : base(context, SM) { }

    public override void Start()
    {
        Context.animationPlayer.Play("Hit");

        //Aplicar retroceso (ej: basado en dirección del daño, aquí asumo izquierda)
        Context.Velocity = new Vector2(-knockbackSpeed * (Context.facingRight ? 1 : -1), Context.Velocity.Y - 200f);  //Empuje horizontal y un poco arriba
        hitTimer = 0.5f;
    }

    public override void PhysicsUpdate(double delta)
    {
        //Aplicar gravedad
        float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
        Context.Velocity += new Vector2(0, gravity * (float)delta);

        Context.MoveAndSlide();

        hitTimer -= (float)delta;
        if (hitTimer <= 0)
        {
            if (Context.IsOnFloor())
            {
                SM.ChangeState(playerState.Idle);
            }
            else
            {
                SM.ChangeState(playerState.Fall);
            }
        }
    }

    public override void Stop()
    {
        //Detiene la animacion
    }
}
