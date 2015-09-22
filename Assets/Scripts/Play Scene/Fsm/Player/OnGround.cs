using UnityEngine;

namespace Devdayo.Platformer2D.Player
{
    public class OnGround : FsmStateBehaviour
    {
        PlayerFSM player;
        
        public override void OnEnter()
        {
            player = fsm.owner as PlayerFSM;
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            player.UpdateHorizontal();
            player.Jump(false);
        }

        public override void OnCollisionStay2D(Collision2D other)
        {
            base.OnCollisionStay2D(other);

            if (other.gameObject.CompareTag(Tag.Bot))
            {
                fsm.Go<OnFlop>();
            }
        }

        public override void OnCollisionExit2D(Collision2D other)
        {
            base.OnCollisionExit2D(other);

            if (other.gameObject.CompareTag(Tag.Platform))
            {
                fsm.Go<OnSoar>();
            }
        }

        public override void OnTriggerStay2D(Collider2D other)
        {
            base.OnTriggerStay2D(other);

            if (other.gameObject.CompareTag(Tag.Ladder))
            {
                fsm.Go<OnLadder>();
            }
        }

    }
}
