using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	private float elapsedTime = 0.0f;
	private float startedTime = 0.0f;
	private bool isTriggered = false;
	private int currentFrame = 0;

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
		this.currentFrame++;
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

	/// <summary>
	/// 経過したフレームを取得する
	/// </summary>
	public int CurrentFrame
	{
		get
		{
			return this.currentFrame;
		}
	}
}
