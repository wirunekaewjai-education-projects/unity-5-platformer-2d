using UnityEngine;

namespace devdayo.Fsm.Gate.State
{
	public class OnOpened : StateBehaviour
	{
		GateFSM gate;
		
		void OnEnable()
		{
			gate = fsm as GateFSM;
		}

		void OnTriggerExit2D(Collider2D c)
		{
			if (!enabled)
				return;
			
			if (!c.CompareTag (Tag.Player))
				return;

			gate.tween.MoveTo(1);
			gate.DoTransition(Transition.OnClosing);
		}
	}
}
