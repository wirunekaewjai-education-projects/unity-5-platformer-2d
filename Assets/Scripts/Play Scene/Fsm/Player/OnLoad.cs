using UnityEngine;

namespace Devdayo.Platformer2D.Player
{
	public class OnLoad : FsmState
	{
        public override void OnEnter()
        {
            PlayerFSM player = fsm.owner as PlayerFSM;

            int saved = PlayerPrefs.GetInt("Saved", 0);

            if (saved == 1)
            {
                Vector3 position = player.transform.position;

                position.x = PlayerPrefs.GetFloat("CheckpointX", position.x);
                position.y = PlayerPrefs.GetFloat("CheckpointY", position.y);

                player.transform.position = position;
            }

            fsm.Go<OnGround>();
        }

        public override void OnExit()
        {
            
        }
        
	}
}
