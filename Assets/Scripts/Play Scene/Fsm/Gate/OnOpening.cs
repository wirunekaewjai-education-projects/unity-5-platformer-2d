using UnityEngine;

namespace devdayo.Fsm.Gate.State
{
    public class OnOpening : StateBehaviour
    {
		GateFSM gate;

        void OnEnable()
        {
			gate = fsm as GateFSM;
        }

		void Update()
		{
			if (!enabled)
				return;

			int index = gate.tween.currentIndex;
			if (index == 1)
				return;

			gate.DoTransition(Transition.OnOpened);
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
