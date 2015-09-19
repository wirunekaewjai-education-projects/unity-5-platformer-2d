using UnityEngine;

// Actions
public partial class PlayerFsm
{
	public class HorizontalInput : FsmAction
	{
		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);

			Rigidbody2D rb = fsm [Var.Rigidbody] as Rigidbody2D;
			Animator anim = fsm [Var.Animator] as Animator;
			float moveSpeed = (float)fsm [Var.MoveSpeed];

			float h = Input.GetAxisRaw ("Horizontal");
			
			// Calculate Move Velocity
			Vector3 velocity = rb.velocity;
			velocity.x = moveSpeed * h;
			
			// Apply Move Velocity
			rb.velocity = velocity;

			// Apply Running Animation | Idle
			anim.SetBool ("Running", h != 0);
		}
	}

	public class VerticalInput : FsmAction
	{
		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);

			Rigidbody2D rb = fsm [Var.Rigidbody] as Rigidbody2D;
			float moveSpeed = (float)fsm [Var.MoveSpeed];

			float v = Input.GetAxisRaw ("Vertical");
			
			// Calculate Move Velocity
			Vector3 velocity = rb.velocity;
			velocity.y = moveSpeed * v;
			
			// Apply Move Velocity
			rb.velocity = velocity;
		}
	}
	
	public class JumpInput : FsmAction
	{
		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);
			
			if(Input.GetKeyDown(KeyCode.Space))
			{
				Rigidbody2D rb = fsm [Var.Rigidbody] as Rigidbody2D;
				float jumpPower = (float)fsm [Var.JumpPower];

				// Calculate Move Velocity
				Vector3 velocity = rb.velocity;
				velocity.y = jumpPower;
				
				// Apply Move Velocity
				rb.velocity = velocity;
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

			Rigidbody2D rb = fsm [Var.Rigidbody] as Rigidbody2D;
			float jumpPower = (float)fsm [Var.JumpPower];

			// Calculate Move Velocity
			Vector3 velocity = rb.velocity;
			velocity.y = jumpPower;
			
			// Apply Move Velocity
			rb.velocity = velocity;

			if (null == finishedState)
				return;

			fsm.Change (finishedState);
		}
	}

	public class FlopAction : FsmAction
	{
		private FsmState finishedState;

		private Collider2D c1, c2;
		private Rigidbody2D rb;
		private Animator anim;

		public FlopAction (FsmState finishedState)
		{
			this.finishedState = finishedState;
		}

		public override void OnEnter (Fsm fsm)
		{
			base.OnEnter (fsm);

			rb = fsm [Var.Rigidbody] as Rigidbody2D;

			anim = fsm [Var.Animator] as Animator;
			anim.SetTrigger ("Flopping");
			
			c1 = fsm.GetComponent<BoxCollider2D>();
			c2 = fsm.GetComponent<PolygonCollider2D>();
			
			c1.enabled = false;
			c2.enabled = true;
		}

		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);

			if (rb.velocity.y != 0)
				return;
			
			rb.gravityScale = 0;
			rb.Sleep ();

			c1.enabled = false;
			c2.enabled = false;

			if (null == finishedState)
				return;

			fsm.Change (finishedState);
		}
	}
	
	public class FaceDetection : FsmAction
	{
		private Rigidbody2D rb;
		
		public override void OnEnter (Fsm fsm)
		{
			base.OnEnter (fsm);
			rb = fsm [Var.Rigidbody] as Rigidbody2D;
		}

		public override void OnUpdate (Fsm fsm)
		{
			base.OnUpdate (fsm);

			float vx = rb.velocity.x;
			
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

			Rigidbody2D rb = fsm [Var.Rigidbody] as Rigidbody2D;
			float vy = rb.velocity.y;

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
