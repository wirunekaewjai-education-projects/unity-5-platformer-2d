using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnAir : StateBehaviour
    {
        PlayerFSM player;
        
        void OnEnable ()
        {
            player = fsm as PlayerFSM;
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
                Vector3 direction = c.relativeVelocity.normalized;
                float angle = Vector3.Angle(Vector3.down, direction);

                // Die || Bounce Up (Jump)
                if (angle >= 45f)
                {
                    player.DoTransition(Transition.OnFlop);
                }
                else
                {
                    player.Jump(true);
                }
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
            else if(c.gameObject.CompareTag(Tag.Platform))
            {
                float vy = player.rigidbody.velocity.y;
                if (Mathf.Abs(vy) < Mathf.Epsilon)
                {
                    player.DoTransition(Transition.OnGround);
                }
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
