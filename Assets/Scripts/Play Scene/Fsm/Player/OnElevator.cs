using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnElevator : StateBehaviour
    {
        PlayerFSM player;

        void OnEnable()
        {
            player = fsm as PlayerFSM;
        }
        
        void Update()
        {
            if (!enabled)
                return;

            player.UpdateHorizontal();
            player.Jump(false);
        }

        void OnCollisionEnter2D(Collision2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Bot))
            {
                Vector3 direction = c.relativeVelocity.normalized;
                float angle = Vector3.Angle(Vector3.down, direction);

                // Bounce Up (Jump) or Die
                if (angle >= 45f)
                {
                    player.DoTransition(Transition.OnFlop);
                }
            }
        }

        void OnTriggerExit2D(Collider2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Elevator))
            {
                player.DoTransition(Transition.OnAir);
            }
        }
    }
}
