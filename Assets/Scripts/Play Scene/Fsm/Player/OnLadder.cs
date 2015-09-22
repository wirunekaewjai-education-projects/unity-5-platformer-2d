using UnityEngine;

namespace Devdayo.Platformer2D.Player
{
    public class OnLadder : FsmStateBehaviour
    {
        PlayerFSM player;
        float gravityScale;

        public override void OnEnter()
        {
            player = fsm.owner as PlayerFSM;

            gravityScale = player.rigidbody.gravityScale;
            player.rigidbody.gravityScale = 0;
        }

        public override void OnExit()
        {
            player.rigidbody.gravityScale = gravityScale;
            gravityScale = 0;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            player.UpdateHorizontal();
            player.UpdateVertical();
            player.Jump(false);
        }

        public override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);

            if (other.gameObject.CompareTag(Tag.Ladder))
            {
                fsm.Go<OnSoar>();
            }
        }
    }
}
