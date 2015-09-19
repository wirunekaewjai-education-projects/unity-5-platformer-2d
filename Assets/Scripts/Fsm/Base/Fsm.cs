using UnityEngine;
using System.Collections.Generic;

public class Fsm : MonoBehaviour
{
	// States
	//private readonly List<FsmState> _states = new List<FsmState>();

	// Variables
	private readonly Dictionary<string, object> _vars = new Dictionary<string, object>();

	public object this[string name]
	{
		get
		{
			return _vars[name];
		}
		set
		{
			_vars[name] = value;
		}
	}

	// States
	private FsmState _currentState;
	public FsmState currentState { get { return _currentState; } }
	
	public void Change(FsmState newState)
	{
		if(null != _currentState)
			_currentState.OnExit (this);
		
		_currentState = newState;
		
		if(null != _currentState)
			_currentState.OnEnter (this);
	}

	/*
	public void Add(FsmState state)
	{
		_states.Add(state);
	}
	*/

	private void Awake()
	{
		OnAwake ();
	}
	
	private void Start()
	{
		OnStart ();
	}
	
	private void Update()
	{
		OnUpdate ();
	}
	
	private void FixedUpdate()
	{
		OnFixedUpdate ();
	}
	
	private void LateUpdate()
	{
		OnLateUpdate ();
	}
	
	protected virtual void OnAwake()
	{

	}
	
	protected virtual void OnStart()
	{
		
	}
	
	protected virtual void OnUpdate()
	{
		if (null != _currentState)
			_currentState.OnUpdate (this);
	}
	
	protected virtual void OnFixedUpdate()
	{
		if (null != _currentState)
			_currentState.OnFixedUpdate (this);
	}
	
	protected virtual void OnLateUpdate()
	{
		if (null != _currentState)
			_currentState.OnLateUpdate (this);
	}
	
	protected virtual void OnCollisionEnter2D(Collision2D other)
	{
		if (null != _currentState)
			_currentState.OnCollisionEnter2D (this, other);
	}
	
	protected virtual void OnCollisionStay2D(Collision2D other)
	{
		if (null != _currentState)
			_currentState.OnCollisionStay2D (this, other);
	}
	
	protected virtual void OnCollisionExit2D(Collision2D other)
	{
		if (null != _currentState)
			_currentState.OnCollisionExit2D (this, other);
	}
	
	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (null != _currentState)
			_currentState.OnTriggerEnter2D (this, other);
	}
	
	protected virtual void OnTriggerStay2D(Collider2D other)
	{
		if (null != _currentState)
			_currentState.OnTriggerStay2D (this, other);
	}
	
	protected virtual void OnTriggerExit2D(Collider2D other)
	{
		if (null != _currentState)
			_currentState.OnTriggerExit2D (this, other);
	}
}