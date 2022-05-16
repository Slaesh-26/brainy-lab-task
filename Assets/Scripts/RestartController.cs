using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartController : MonoBehaviour
{
	public static RestartController instance;

	private List<IRestartable> restartables;
	private List<Damager> damagers;

	public void AddListener(IRestartable restartable)
	{
		restartables ??= new List<IRestartable>();
		restartables.Add(restartable);
	}
	
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;

		damagers = new List<Damager>(FindObjectsOfType<Damager>());

		foreach (Damager damager in damagers)
		{
			damager.hitOther += OnAnyHit;
		}
	}

	private void OnAnyHit(int score)
	{
		foreach (IRestartable restartable in restartables)
		{
			restartable.OnRestart();
		}
	}
}
