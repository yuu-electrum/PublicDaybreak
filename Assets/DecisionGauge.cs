using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionGauge : MonoBehaviour
{
    public enum GaugeResult
	{
        Bad,
        Normal,
        Good,
        Perfect
	}

    [SerializeField]
    private GameObject[] gauges;

    [SerializeField]
    private GameObject[] gaugeDescriptions;

    [SerializeField]
    private GameObject currentGaugePosition;

    private bool isTriggered = false;
    private int elapsedFrame = 0;
    private int timeSpan;
    private float gaugeLength;
    private float gaugeOriginX;
    private Dictionary<GaugeResult, float> frameBorders;

	public void Update()
    {
        if(!this.isTriggered)
		{
            return;
		}

        var progression = this.CurrentGaugePosition / this.timeSpan;
        var localPosition = this.currentGaugePosition.transform.localPosition;
        localPosition.x = this.gaugeOriginX + (this.gaugeLength * progression);
        this.currentGaugePosition.transform.localPosition = localPosition;

        // 現在のゲージの効果を取得する
        foreach(var g in this.gaugeDescriptions)
		{
            if(g.activeSelf)
            { 
                g.SetActive(false);
            }
		}
        switch(this.CurrentPrecision)
		{
            case GaugeResult.Normal : gaugeDescriptions[0].SetActive(true); break;
            case GaugeResult.Good   : gaugeDescriptions[1].SetActive(true); break;
            case GaugeResult.Perfect: gaugeDescriptions[2].SetActive(true); break;
            case GaugeResult.Bad    : gaugeDescriptions[3].SetActive(true); break;
		}

        elapsedFrame++;
    }

    /// <summary>
    /// ゲージを開始する
    /// </summary>
    /// <param name="timeSpan">ゲージが1週するフレーム数</param>
    /// <param name="ratioForPerfect">PERFECTのフレーム数の割合</param>
    /// <param name="ratioForGood">GOODのフレーム数の割合</param>
    /// <param name="ratioForNormal">NORMALのフレーム数の割合</param>
    /// <param name="ratioForBad">BADのフレーム数の割合</param>
    public void Trigger(int timeSpan, float ratioForPerfect, float ratioForGood, float ratioForNormal, float ratioForBad)
	{
        this.isTriggered = true;
        this.timeSpan = timeSpan;

        // 各ゲージのフレーム数を計算する
        var frameForPerfect       = this.timeSpan * (ratioForPerfect / 10.0f);
        var frameForExceptPerfect = this.timeSpan - frameForPerfect;

        var ratioTotal = ratioForBad + ratioForNormal + ratioForGood;

        var frameForGood   = frameForExceptPerfect * (ratioForGood   / ratioTotal);
        var frameForNormal = frameForExceptPerfect * (ratioForNormal / ratioTotal);
        var frameForBad    = frameForExceptPerfect * (ratioForBad    / ratioTotal);

        this.frameBorders = new Dictionary<GaugeResult, float>();
        this.frameBorders.Add(GaugeResult.Normal , frameForNormal);
        this.frameBorders.Add(GaugeResult.Good   , frameForGood);
        this.frameBorders.Add(GaugeResult.Perfect, frameForPerfect);
        this.frameBorders.Add(GaugeResult.Bad    , frameForBad);

        // ゲージの表示の調整を行う
        var gaugeObjects = new Dictionary<GameObject, float>();
        gaugeObjects.Add(this.gauges[0], FrameRatioForNormal);
        gaugeObjects.Add(this.gauges[1], FrameRatioForGood);
        gaugeObjects.Add(this.gauges[2], FrameRatioForPerfect);
        gaugeObjects.Add(this.gauges[3], FrameRatioForBad);

        var leftPadding = 0.5f;
        var idx = 0;
        foreach(var g in gaugeObjects)
		{
            var localScale = g.Key.transform.localScale;
            localScale.x = 1.0f * g.Value;
            g.Key.transform.localScale = localScale;

            var localPosition = g.Key.transform.localPosition;
            localPosition.x = (leftPadding - g.Key.transform.localScale.x / 2.0f) * -1;
            leftPadding -= localScale.x;
            g.Key.transform.localPosition = localPosition;
		}

        this.gaugeOriginX = -0.5f + this.currentGaugePosition.transform.localScale.x / 2.0f;
        this.gaugeLength  = 1.0f - this.currentGaugePosition.transform.localScale.x;

        var currentGaugeLocalPosition = this.currentGaugePosition.transform.localPosition;
        currentGaugeLocalPosition.x = this.gaugeOriginX;
        this.currentGaugePosition.transform.localPosition = currentGaugeLocalPosition;
	}

    /// <summary>
    /// ゲージを止める
    /// </summary>
    /// <returns></returns>
    public GaugeResult Stop()
	{
        this.isTriggered = false;
        return CurrentPrecision;
	}

	/// <summary>
    /// ゲージをリセットする
    /// </summary>
	public void Reset()
	{
		this.elapsedFrame = 0;
	}

    /// <summary>
    /// 現在のゲージの位置を取得する
    /// </summary>
    public float CurrentGaugePosition
	{
        get
		{
            return this.elapsedFrame % this.timeSpan;
		}
	}

    /// <summary>
    /// 現在のゲージが指しているものを取得する
    /// </summary>
    public GaugeResult CurrentPrecision
	{
        get
		{
            var lastFrame = 0.0f;
            foreach(var border in this.frameBorders)
			{
                if(this.CurrentGaugePosition < lastFrame + border.Value)
				{
                    return border.Key;
				}
                lastFrame += border.Value;
			}

            return GaugeResult.Perfect;
		}
	}

    public float FrameRatioForPerfect
	{
        get
		{
            if(!this.isTriggered)
			{
                return 0.0f;
			}

            return this.frameBorders[GaugeResult.Perfect] / this.timeSpan;
		}
	}

    public float FrameRatioForGood
	{
        get
		{
            if(!this.isTriggered)
			{
                return 0.0f;
			}

            return this.frameBorders[GaugeResult.Good] / this.timeSpan;
		}
	}

    public float FrameRatioForNormal
	{
        get
		{
            if(!this.isTriggered)
			{
                return 0.0f;
			}

            return this.frameBorders[GaugeResult.Normal] / this.timeSpan;
		}
	}

    public float FrameRatioForBad
	{
        get
		{
            if(!this.isTriggered)
			{
                return 0.0f;
			}

            return this.frameBorders[GaugeResult.Bad] / this.timeSpan;
		}
	}
}
