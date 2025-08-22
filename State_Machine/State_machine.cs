using Godot;
using System;
using System.Collections.Generic;
using System.Data;

public partial class State_machine<T> where T : Node
{
	private T Context; //agrego el contexto
	private State_base<T> CurrentState; //agrego el estado actual

	//diccionario que contiene los estados del player y los estados base
	private Dictionary<playerState, State_base<T>> States = new Dictionary<playerState, State_base<T>>();

	//constructor para el contexto
	public State_machine(T context)
	{
		Context = context;
	}

	//funcion que agrega un estado
	public void AddState(playerState stateType, State_base<T> state)
	{
		States[stateType] = state;
	}

	//funcion para la transicion de estados
	public void ChangeState(playerState newState)
	{
		//si el estado ya se encuentra, retorna
		if (!States.ContainsKey(newState)) return;

		CurrentState?.Stop(); //para el estado actual
		CurrentState = States[newState]; //guarda el nuevo estado en estado actual
		CurrentState.Start(); //inicia el estado
	}

	//funcion que actualiza el estado
	public void Update(double delta)
	{
		CurrentState?.Update(delta);
	}

	//funcion que actualiza las fisicas
	public void PhysicsUpdate(double delta)
	{
		CurrentState?.PhysicsUpdate(delta);
	}

	//funcion para el manejo de eventos
	public void HandleInput(InputEvent @event)
	{
		CurrentState?.HandleInput(@event);
	}
}
