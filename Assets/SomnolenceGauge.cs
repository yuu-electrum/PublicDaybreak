using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomnolenceGauge : MonoBehaviour
{
	private float currentValue;
	private float scale;
	private Vector3 initialPosition;

	/// <summary>
	/// 眠気ゲージを初期化する
	/// </summary>
	/// <param name="scale">眠気ゲージの長さ</param>
	/// <param name="initValue">ゲージの初期値</param>
	public void Initialize(float scale, float initValue = 0.0f)
	{
		this.scale = scale;
		this.SetValue(initValue);
		this.initialPosition = this.transform.position;
	}

	/// <summary>
	/// ゲージの長さを更新する
	/// </summary>
	/// <param name="value"></param>
	public void SetValue(float value)
	{
		if(value >= 1.0f)
		{
			value = 1.0f;
		}

		if(value <= 0.0f)
		{
			value = 0.0f;
		}

		var localScale = this.transform.localScale;
		localScale.x = this.scale * value;
		this.transform.localScale = localScale;

		var posX = this.scale / 2.0f + localScale.x / 2.0f;
		var position = this.transform.position;
		position.x = posX;
		this.transform.position = position;
	}
}
