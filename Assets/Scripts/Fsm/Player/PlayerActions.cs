using UnityEngine;

// Actions
public partial class PlayerFsm
{
	public class HorizontalInput : FsmAction
	{
		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);

			PlayerFsm player = fsm as PlayerFsm;

			float h = Input.GetAxisRaw ("Horizontal");
			
			// Calculate Move Velocity
			Vector3 velocity = player.rigidbody.velocity;
			velocity.x = player.moveSpeed * h;
			
			// Apply Move Velocity
			player.rigidbody.velocity = velocity;

			// Apply Running Animation | Idle
			player.animator.SetBool ("Running", h != 0);
		}
	}

	public class VerticalInput : FsmAction
	{
		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);

			PlayerFsm player = fsm as PlayerFsm;

			float v = Input.GetAxisRaw ("Vertical");
			
			// Calculate Move Velocity
			Vector3 velocity = player.rigidbody.velocity;
			velocity.y = player.moveSpeed * v;
			
			// Apply Move Velocity
			player.rigidbody.velocity = velocity;
		}
	}
	
	public class JumpInput : FsmAction
	{
		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);
			
			if(Input.GetKeyDown(KeyCode.Space))
			{
				PlayerFsm player = fsm as PlayerFsm;

				// Calculate Move Velocity
				Vector3 velocity = player.rigidbody.velocity;
				velocity.y = player.jumpPower;
				
				// Apply Move Velocity
				player.rigidbody.velocity = velocity;
			}
		}
	}

	public class JumpAction : FsmAction
	{
		private FsmState finishedState;

		public JumpAction (FsmState finishedState)
		{
			this.finishedState = finishedState;
		}

		public override void OnEnter (Fsm fsm)
		{
			base.OnEnter (fsm);

			PlayerFsm player = fsm as PlayerFsm;

			// Calculate Move Velocity
			Vector3 velocity = player.rigidbody.velocity;
			velocity.y = player.jumpPower;
			
			// Apply Move Velocity
			player.rigidbody.velocity = velocity;

			if (null == finishedState)
				return;

			fsm.Change (finishedState);
		}
	}

	public class FlopAction : FsmAction
	{
		private FsmState finishedState;

		private Collider2D c1, c2;

		public FlopAction (FsmState finishedState)
		{
			this.finishedState = finishedState;
		}

		public override void OnEnter (Fsm fsm)
		{
			base.OnEnter (fsm);

			PlayerFsm player = fsm as PlayerFsm;
			player.animator.SetTrigger ("Flopping");
			
			c1 = fsm.GetComponent<BoxCollider2D>();
			c2 = fsm.GetComponent<PolygonCollider2D>();
			
			c1.enabled = false;
			c2.enabled = true;
		}

		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);

			PlayerFsm player = fsm as PlayerFsm;

			if (player.rigidbody.velocity.y != 0)
				return;
			
			player.rigidbody.gravityScale = 0;
			player.rigidbody.Sleep ();

			c1.enabled = false;
			c2.enabled = false;

			if (null == finishedState)
				return;

			fsm.Change (finishedState);
		}
	}
	
	public class FaceDetection : FsmAction
	{
		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);

			PlayerFsm player = fsm as PlayerFsm;
			float vx = player.rigidbody.velocity.x;
			
			// Moving ?
			if (vx == 0)
				return;
			
			// Calculate Face Direction
			Vector3 scale = fsm.transform.localScale;
			scale.x = Mathf.Abs (scale.x) * Mathf.Sign (vx);
			
			// Flip Face Direction
			fsm.transform.localScale = scale;
		}
	}


	public class PlatformDetection : FsmAction
	{
		private FsmState enter, exit;

		public PlatformDetection (FsmState enter, FsmState exit)
		{
			this.enter = enter;
			this.exit = exit;
		}

		public override void OnCollisionStay2D (Fsm fsm, Collision2D other)
		{
			base.OnCollisionStay2D (fsm, other);

			if (!other.gameObject.CompareTag (Tag.Platform))
				return;

			PlayerFsm player = fsm as PlayerFsm;
			float vy = player.rigidbody.velocity.y;
			
			if (Mathf.Abs (vy) > Mathf.Epsilon)
				return;
			
			if (null == enter)
				return;
			
			fsm.Change(enter);
		}

		public override void OnCollisionExit2D (Fsm fsm, Collision2D other)
		{
			base.OnCollisionExit2D (fsm, other);
			
			if (!other.gameObject.CompareTag (Tag.Platform))
				return;

			if (null == exit)
				return;

			fsm.Change(exit);
		}
	}

	public class LadderDetection : FsmAction
	{
		private FsmState enter, exit;

		public LadderDetection(FsmState enter, FsmState exit)
		{
			this.enter = enter;
			this.exit = exit;
		}

		public override void OnTriggerEnter2D (Fsm fsm, Collider2D other)
		{
			base.OnTriggerEnter2D (fsm, other);

			if (!other.gameObject.CompareTag (Tag.Ladder))
				return;

			if (null == enter)
				return;

			fsm.Change (enter);
		}

		public override void OnTriggerExit2D (Fsm fsm, Collider2D other)
		{
			base.OnTriggerExit2D (fsm, other);
			
			if (!other.gameObject.CompareTag (Tag.Ladder))
				return;

			if (null == exit)
				return;
			
			fsm.Change (exit);
		}
	}

	public class BotDetection : FsmAction
	{
		private FsmState headEvent, bodyEvent;

		public BotDetection (FsmState headEvent, FsmState bodyEvent)
		{
			this.headEvent = headEvent;
			this.bodyEvent = bodyEvent;
		}

		public override void OnCollisionEnter2D (Fsm fsm, Collision2D other)
		{
			base.OnCollisionEnter2D (fsm, other);

			if(!other.gameObject.CompareTag(Tag.Bot))
				return;
			
			Vector3 direction = other.relativeVelocity.normalized;
			float angle = Vector3.Angle (Vector3.down, direction);

			// Bounce Up (Jump) or Die
			if (angle < 45f)
			{
				if(null != headEvent)
					fsm.Change(headEvent);
			}
			else if(null != bodyEvent)
			{
				fsm.Change(bodyEvent);
			}
		}
	}
}
