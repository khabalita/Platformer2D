using Godot;
using System;

//clase abstracta con un generico para pasar el tipo de nodo, donde T es de tipo nodo
public abstract class State_base<T> where T : Node
{
	//variable protegida para crear el contexto, basado en el generico
	protected T Context { get; private set; }
	//variable protegida para la maquina de estados, basado en el generico
	protected State_machine<T> SM { get; private set; } //SM = StateMachine

	//constructor del estado base, le paso el contexto
	public State_base(T context, State_machine<T> sm)
	{
		Context = context;
		SM = sm;
	}

	//declaracion de metodos de la maquina de estados
	public virtual void Start() { } //inicia el estado
	public virtual void Stop() { } //frena el estado
	public virtual void Update(double delta) { } //actualiza el estado
	public virtual void PhysicsUpdate(double delta) { } //actualiza las fisicas
	public virtual void HandleInput(InputEvent @event) { } //maneja los errores
}
