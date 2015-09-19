using UnityEngine;
using System.Collections;

public partial class PlayerFsm : Fsm
{
	public float moveSpeed = 3f;
	public float jumpPower = 6f;

	[HideInInspector] public Animator animator;
	[HideInInspector] public Rigidbody2D rigidbody;

	protected override void OnAwake ()
	{
		base.OnAwake ();

		// Init Variables
		animator = GetComponent<Animator> ();
		rigidbody = GetComponent<Rigidbody2D> ();

		/*
		this ["rigidbody"] = GetComponent<Rigidbody2D> ();
		this ["animator"] = GetComponent<Animator> ();

		this ["moveSpeed"] = 3f;
		this ["jumpPower"] = 6f;
		*/

		// Init Simple Actions
		FsmAction jumpInput = new JumpInput ();
		FsmAction horizontalInput = new HorizontalInput ();
		FsmAction verticalInput = new VerticalInput ();
		FsmAction faceDetection = new FaceDetection ();

		// Init States
		FsmState airState 	 = new FsmState ("Air State");
		FsmState jumpState 	 = new FsmState ("Jump State");
		FsmState groundState  = new FsmState ("Ground State");
		FsmState ladderState  = new FsmState ("Ladder State");
		FsmState flopState 	 = new FsmState ("Flop State");
		FsmState diedState	 = new FsmState ("Died State");

		// Add Actions
		airState.Add (horizontalInput);
		airState.Add (faceDetection);
		airState.Add (new LadderDetection (ladderState, null));
		airState.Add (new PlatformDetection (groundState, null));
		airState.Add (new BotDetection(jumpState, flopState));

		jumpState.Add (new JumpAction (airState));
		
		groundState.Add (horizontalInput);
		groundState.Add (jumpInput);
		groundState.Add (faceDetection);
		groundState.Add (new LadderDetection (ladderState, null));
		groundState.Add (new PlatformDetection (null, airState));
		groundState.Add (new BotDetection(null, flopState));

		ladderState.Add (horizontalInput);
		ladderState.Add (verticalInput);
		ladderState.Add (new LadderDetection (null, airState));
		ladderState.Add (new BotDetection(null, flopState));

		flopState.Add (new FlopAction (diedState));

		diedState.Add (new RestartLevel (3));

		// Begin FSM
		this.Change (airState);
	}

	protected override void OnUpdate ()
	{
		base.OnUpdate ();

		//print (currentState.name);
	}
}
