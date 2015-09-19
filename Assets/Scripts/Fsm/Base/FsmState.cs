using UnityEngine;
using System.Collections.Generic;

public class FsmState : FsmAction
{
	private readonly List<FsmAction> _actions = new List<FsmAction>();

	public string name;

	public void Add(FsmAction action)
	{
		_actions.Add (action);
	}

	public virtual void OnStart ()
	{

	}
	
	public override void OnEnter (Fsm fsm)
	{
		base.OnEnter (fsm);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnEnter(fsm);
		}
	}
	
	public override void OnUpdate (Fsm fsm)
	{
		base.OnUpdate (fsm);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnUpdate(fsm);
		}
	}
	
	public override void OnFixedUpdate (Fsm fsm)
	{
		base.OnFixedUpdate (fsm);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnFixedUpdate(fsm);
		}
	}
	
	public override void OnLateUpdate (Fsm fsm)
	{
		base.OnLateUpdate (fsm);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnLateUpdate(fsm);
		}
	}
	
	public override void OnExit (Fsm fsm)
	{
		base.OnExit (fsm);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnExit(fsm);
		}
	}
	
	public override void OnCollisionEnter2D (Fsm fsm, Collision2D other)
	{
		base.OnCollisionEnter2D (fsm, other);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnCollisionEnter2D(fsm, other);
		}
	}
	
	public override void OnCollisionStay2D (Fsm fsm, Collision2D other)
	{
		base.OnCollisionStay2D (fsm, other);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnCollisionStay2D(fsm, other);
		}
	}
	
	public override void OnCollisionExit2D (Fsm fsm, Collision2D other)
	{
		base.OnCollisionExit2D (fsm, other);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnCollisionExit2D(fsm, other);
		}
	}
	
	public override void OnTriggerEnter2D (Fsm fsm, Collider2D other)
	{
		base.OnTriggerEnter2D (fsm, other);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnTriggerEnter2D(fsm, other);
		}
	}
	
	public override void OnTriggerStay2D (Fsm fsm, Collider2D other)
	{
		base.OnTriggerStay2D (fsm, other);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnTriggerStay2D(fsm, other);
		}
	}
	
	public override void OnTriggerExit2D (Fsm fsm, Collider2D other)
	{
		base.OnTriggerExit2D (fsm, other);
		
		foreach (FsmAction action in _actions) 
		{
			action.OnTriggerExit2D(fsm, other);
		}
	}

	public override string ToString ()
	{
		return name;
	}
}