using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnSoar : StateBehaviour
    {
        PlayerFSM player;
		
		void Awake()
		{
			player = fsm as PlayerFSM;
		}

        void OnEnable ()
        {
            player.animator.SetBool("Soaring", true);
        }

        void OnDisable ()
        {
            player.animator.SetBool("Soaring", false);
        }

        void Update ()
        {
            if (!enabled)
                return;

            player.UpdateHorizontal();
        }
        
        void OnCollisionEnter2D(Collision2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Bot))
            {
                Vector3 p1 = player.transform.position;
                Vector3 p2 = c.transform.position;

                Vector3 direction = (p1 - p2).normalized;
                float angle = Vector3.Angle(Vector3.up, direction);

                if (angle > 30f)
                    return;

                player.Jump(true);
            }
            
        }

        void OnCollisionStay2D(Collision2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Elevator))
            {
                player.DoTransition(Transition.OnElevator);
            }
            else if (c.gameObject.CompareTag(Tag.Platform))
            {
                float vy = player.rigidbody.velocity.y;
                if (Mathf.Abs(vy) < Mathf.Epsilon)
                {
                    player.DoTransition(Transition.OnGround);
                }
            }
            else if (c.gameObject.CompareTag(Tag.Bot))
            {
                Vector3 p1 = player.transform.position;
                Vector3 p2 = c.transform.position;

                Vector3 direction = (p1 - p2).normalized;
                float angle = Vector3.Angle(Vector3.up, direction);

                if (angle <= 30f)
                    return;

                // Die !!!
                player.rigidbody.velocity = Physics2D.gravity;
                player.DoTransition(Transition.OnFlop);
            }
        }

        void OnTriggerStay2D(Collider2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Ladder))
            {
                player.DoTransition(Transition.OnLadder);
            }
        }
        
    }
}
