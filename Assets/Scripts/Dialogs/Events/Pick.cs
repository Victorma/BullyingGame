﻿using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : EventManager {

	public IState state;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if(ev.Name.Replace("\"", "") == "pick")
		{
			string var = ((String) ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD)).Replace("\"","");

			var value = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);
			this.PickObject(var, value);
		}
	}

	public override void Tick()
	{
		//throw new NotImplementedException();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Set the value varValue to the variable varName and hide the object
	/// </summary>
	/// <param name="varName"></param>
	/// <param name="varValue"></param>
	private void PickObject(string varName, System.Object varValue)
	{
		Type t = state.GetType();
		t.GetProperty(varName).SetValue(state, varValue, null);
		/*
		 * Throw exception in IsoUnityDialogs
		 * this.gameObject.SetActive(false);
		 */
		this.GetComponent<SpriteRenderer>().enabled = false;
		this.GetComponent<Collider2D>().enabled = false;
	}
}
