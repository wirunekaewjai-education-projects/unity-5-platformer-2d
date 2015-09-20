using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnGround : StateBehaviour
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
                player.DoTransition(Transition.OnFlop);
            }
        }

        void OnCollisionExit2D(Collision2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Platform))
            {
                player.DoTransition(Transition.OnAir);
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
