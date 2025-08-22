using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private State_machine<Player> SM; //SM = StateMachine
    [Export]
    public RayCast2D LeftWallRay; //rayCast para la pared izquierda
    [Export]
    public RayCast2D RightWallRay; //rayCast para la pared derecha
    [Export]
    public AnimationPlayer animationPlayer;
    [Export]
    public Sprite2D Sprite;
    public int jumpCount = 0; //Conteo de saltos, inicia en 0
    public int maxJumps = 2; //Maximo de saltos que puede dar antes de caer al piso (salto y salto doble)
    public bool facingRight = true; //Direccion del personaje, true = izquierda, false = derecha

    public override void _Ready()
    {
        SM = new State_machine<Player>(this);
        SM.AddState(playerState.Idle, new IdleState<Player>(this, SM));
        SM.AddState(playerState.Walk, new WalkState<Player>(this, SM));
        SM.AddState(playerState.Run, new RunState<Player>(this, SM));
        SM.AddState(playerState.Jump, new JumpState<Player>(this, SM));
        SM.AddState(playerState.Fall, new FallState<Player>(this, SM));
        SM.AddState(playerState.Hit, new HitState<Player>(this, SM));
        SM.AddState(playerState.WallSlide, new WallSlideState<Player>(this, SM));
        SM.AddState(playerState.WallJump, new WallJumpState<Player>(this, SM));
        SM.ChangeState(playerState.Idle);
    }

    public override void _Input(InputEvent @event)
    {
        SM.HandleInput(@event);

    }

    public override void _Process(double delta)
    {
        SM.Update(delta);
        flipPlayer();
    }

    public override void _PhysicsProcess(double delta)
    {
        SM.PhysicsUpdate(delta);
    }

    //metodo para detectar si esta tocando la pared
    public bool IsOnTheWall()
    {
        return (LeftWallRay.IsColliding() || RightWallRay.IsColliding()) && !IsOnFloor();
    }

    //metodo para detectar la pared (izquierda o derecha)
    public float GetWallDirection()
    {
        if (LeftWallRay.IsColliding()) return -1f; //pared izquierda
        if (RightWallRay.IsColliding()) return 1f; //pared derecha
        return 0f;
    }

    public void flipPlayer()
    {
        if (Sprite != null)
        {
            Sprite.FlipH = !facingRight;
        }
    }


}
