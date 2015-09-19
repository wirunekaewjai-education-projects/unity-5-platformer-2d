using UnityEngine;
using System.Collections;

public partial class PlayerFsm : Fsm
{
	public static class Var
	{
		public static readonly string Rigidbody = "rigidbody";
		public static readonly string Animator  = "animator";

		public static readonly string MoveSpeed  = "moveSpeed";
		public static readonly string JumpPower  = "jumpPower";
	}

	protected override void OnAwake ()
	{
		base.OnAwake ();

		// Add Variables
		this [Var.Rigidbody] = GetComponent<Rigidbody2D> ();
		this [Var.Animator] = GetComponent<Animator> ();

		this [Var.MoveSpeed] = 3f;
		this [Var.JumpPower] = 6f;

		// Add States
		this.Add (State.Air);
		this.Add (State.Jump);
		this.Add (State.Ground);
		this.Add (State.Ladder);
		this.Add (State.Flop);
		this.Add (State.Died);
	}

	protected override void OnStart ()
	{
		base.OnStart ();
		
		// Begin FSM
		this.Change (State.Air);
	}
}
