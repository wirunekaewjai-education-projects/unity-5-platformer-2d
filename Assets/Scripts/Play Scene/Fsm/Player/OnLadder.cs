using UnityEngine;

namespace devdayo.Fsm.Player.State
{
    public class OnLadder : StateBehaviour
    {
        PlayerFSM player;
        float gravityScale;
		
		void Awake()
		{
			player = fsm as PlayerFSM;
		}

        void OnEnable()
        {
            gravityScale = player.rigidbody.gravityScale;
            player.rigidbody.gravityScale = 0;
        }

        void OnDisable()
        {
            player.rigidbody.gravityScale = gravityScale;
            gravityScale = 0;
        }

        void Update()
        {
            if (!enabled)
                return;

            player.UpdateHorizontal();
            player.UpdateVertical();
            player.Jump(false);
        }
        
        void OnTriggerExit2D(Collider2D c)
        {
            if (!enabled)
                return;

            if (c.gameObject.CompareTag(Tag.Ladder))
            {
                player.DoTransition(Transition.OnSoar);
            }
        }
    }
}
