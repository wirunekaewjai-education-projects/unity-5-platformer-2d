using UnityEngine;

public abstract class FsmAction
{
	public virtual void OnEnter(Fsm fsm)
	{
		
	}
	
	public virtual void OnUpdate(Fsm fsm)
	{
		
	}
	
	public virtual void OnFixedUpdate(Fsm fsm)
	{
		
	}
	
	public virtual void OnLateUpdate(Fsm fsm)
	{
		
	}
	
	public virtual void OnExit(Fsm fsm)
	{
		
	}
	
	public virtual void OnCollisionEnter2D(Fsm fsm, Collision2D other)
	{
		
	}
	
	public virtual void OnCollisionStay2D(Fsm fsm, Collision2D other)
	{
		
	}
	
	public virtual void OnCollisionExit2D(Fsm fsm, Collision2D other)
	{
		
	}
	
	public virtual void OnTriggerEnter2D(Fsm fsm, Collider2D other)
	{
		
	}
	
	public virtual void OnTriggerStay2D(Fsm fsm, Collider2D other)
	{
		
	}
	
	public virtual void OnTriggerExit2D(Fsm fsm, Collider2D other)
	{
		
	}
	
}