using UnityEngine;

namespace devdayo.Fsm.Player.State
{
	public class OnLoad : StateBehaviour
	{
		PlayerFSM player;
		
		void Awake()
		{
			player = fsm as PlayerFSM;
		}

		void Start()
		{
			int saved = PlayerPrefs.GetInt("Saved", 0);

			if (saved == 1)
			{
				Vector3 position = player.transform.position;

				position.x = PlayerPrefs.GetFloat("CheckpointX", position.x);
				position.y = PlayerPrefs.GetFloat("CheckpointY", position.y);

				player.transform.position = position;
			}

			player.DoTransition(Transition.OnGround);
		}
	}
}
