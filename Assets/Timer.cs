using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	private float elapsedTime = 0.0f;
	private float startedTime = 0.0f;
	private bool isTriggered = false;

	/// <summary>
	/// 時間計測を開始する
	/// </summary>
	public void Trigger()
	{
		this.isTriggered = true;
		this.startedTime = Time.time;
	}

	public void Update()
	{
		if(!this.isTriggered)
		{
			return;
		}

		this.elapsedTime = Time.time - this.startedTime;
	}

	/// <summary>
	/// 経過した時間を取得する
	/// </summary>
	public float ElapsedTime
	{
		get
		{
			return this.elapsedTime;
		}
	}
}
