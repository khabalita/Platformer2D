using Godot;
using System;

//estados principales del player
public enum playerState
{
	Idle, //estatico
	Walk, //caminando
	Run, //corriendo
	Jump, //saltando
	Fall, //cayendo
	Hit, //golpe
	WallSlide, //deslizandose por pared
	WallJump, //saltando a la pared


}
