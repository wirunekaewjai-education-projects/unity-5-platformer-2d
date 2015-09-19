using UnityEngine;

public class RestartLevel : FsmAction
{
	private float delay;
	private float timeCount;

	public RestartLevel (float delay = 0)
	{
		this.delay = delay;
	}

	public override void OnEnter (Fsm fsm)
	{
		base.OnEnter (fsm);

		if (delay != 0)
			return;

		int level = Application.loadedLevel;
		Application.LoadLevel (level);
	}

	public override void OnUpdate (Fsm fsm)
	{
		base.OnUpdate (fsm);

		if (delay == 0)
			return;

		timeCount += Time.deltaTime;
		if (timeCount < delay)
			return;

		timeCount = 0;

		int level = Application.loadedLevel;
		Application.LoadLevel (level);
	}
}