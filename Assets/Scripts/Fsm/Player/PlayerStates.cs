using UnityEngine;

// States
public partial class PlayerFsm
{
	public static class State
	{
		public static readonly FsmState Air		= new AirState();
		public static readonly FsmState Jump 	= new JumpState();
		public static readonly FsmState Ground  = new GroundState();
		public static readonly FsmState Ladder	= new LadderState();
		public static readonly FsmState Flop 	= new FlopState();
		public static readonly FsmState Died 	= new DiedState();
	}

	public class AirState : FsmState
	{
		public override void OnStart ()
		{
			base.OnStart ();
			name = "Air State";

			Add (new HorizontalInput ());
			Add (new FaceDetection ());

			Add (new LadderDetection(State.Ladder, null));
			Add (new PlatformDetection(State.Ground, null));
			Add (new BotDetection(State.Jump, State.Flop));
		}
	}

	public class GroundState : FsmState
	{
		public override void OnStart ()
		{
			base.OnStart ();
			name = "Ground State";

			Add (new HorizontalInput ());
			Add (new JumpInput ());
			Add (new FaceDetection ());
			
			Add (new LadderDetection(State.Ladder, null));
			Add (new PlatformDetection(null, State.Air));
			Add (new BotDetection(null, State.Flop));
		}
	}

	public class JumpState : FsmState
	{
		public override void OnStart ()
		{
			base.OnStart ();
			name = "Jump State";

			Add (new JumpAction (State.Ground));
		}
	}

	public class LadderState : FsmState
	{
		public override void OnStart ()
		{
			base.OnStart ();
			name = "Ladder State";

			Add (new HorizontalInput ());
			Add (new VerticalInput ());

			Add (new LadderDetection(null, State.Air));
			Add (new BotDetection(null, State.Flop));
		}
	}

	public class FlopState : FsmState
	{
		public override void OnStart ()
		{
			base.OnStart ();
			name = "Flop State";

			Add (new FlopAction (State.Died));
		}
	}

	public class DiedState : FsmState
	{
		public override void OnStart ()
		{
			base.OnStart ();
			name = "Died State";

			Add (new RestartLevel (3));
		}
	}
}