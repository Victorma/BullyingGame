﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeUpManager : MonoBehaviour {

	public Mobile mobileObject;
	public Eyelids eyelidsObject;
	public CameraScroll cameraScroll;

	public float wakeUpSeconds = 6;
	public float sleepSeconds = 2;
	public float takeMobileSeconds = 2;
	public float hideMobileSeconds = 2;
	public float introTime = 5;

	public int sameScene;
	public int nextScene;

	private float offset = 1;

	private float dTime = 0;

	public UnityEngine.UI.Text introText;

	/// <summary>
	/// state = 0 --> Wake up
	/// state = 1 --> Take the mobile
	/// state = 2 --> Waiting
	/// state = 3 --> Hide the mobile
	/// state = 4 --> Go to sleep
	/// state = 5 --> Change Scene
	/// </summary>
	private int state;

	// Use this for initialization
	void Start () {
		cameraScroll.disableScroll(true);
		state = 0;
		Debug.Log(GlobalState.Day + "  -  " + GlobalState.Repeated);
		if (!GlobalState.Repeated) {
			if (GlobalState.Day == 0)
			{
				introText.enabled = true;
				offset = introTime;
			}
			GlobalState.Hour = 7;
			GlobalState.Minute = 30;
		} else {
			introText.enabled = false;
			initQuests();
		}
		mobileObject.updateHour();

	}
	
	void initQuests()
	{
		GlobalState.GuillermoQuest = 0;
		GlobalState.AlisonQuest = 0;
		GlobalState.JoseQuest = 0;
		GlobalState.MariaQuest = 0;
		GlobalState.AlejandroQuest = 0;
		GlobalState.AnaQuest = 0;
	}

	// Update is called once per frame
	void Update () {
		//Wake up (Move eyelids)
		if (state == 0 && dTime >= offset) {
			introText.enabled = false;
			state = 1;
			eyelidsObject.wakeUp (wakeUpSeconds);
			dTime = 0;
		}
		//Take the mobile
		else if (state == 1 && dTime >= wakeUpSeconds)
		{
			state = 2;
			mobileObject.takeMobile(takeMobileSeconds);
			dTime = 0;
		}
		//Wait a mobile interaction
		else if (state == 2 && dTime > takeMobileSeconds)
		{
			if (mobileObject.isSounding() && !cameraScroll.isDisabled())
			{
				cameraScroll.disableScroll(false);
			} else if (!mobileObject.isSounding()) {
				state = 3;
				dTime = 0;
			}
		}
		//Hide mobile
		else if (state == 3) {
			cameraScroll.disableScroll(true);
			state = 4;
			mobileObject.hideMobile(hideMobileSeconds);
			dTime = 0;
		}
		//Check if wake up or go to sleep
		else if (state == 4 && dTime >= hideMobileSeconds)
		{
			if (!mobileObject.isAlarmDelayed())
			{
				SceneManager.LoadScene(nextScene);
			} else
			{
				eyelidsObject.goToSleep(sleepSeconds);
				GlobalState.Hour = 8;
				GlobalState.Minute = 5;
				mobileObject.updateHour();
			}
			state = 5;
			dTime = 0;
		}
		//Go to sleep
		else if (state == 5 && dTime > sleepSeconds)
		{
			GlobalState.Repeated = true;
			SceneManager.LoadScene(sameScene);
		}
		dTime += Time.deltaTime;
	}
}
