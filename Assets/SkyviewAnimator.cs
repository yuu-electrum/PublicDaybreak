using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyviewAnimator : MonoBehaviour
{
    [SerializeField]
    private Timer sourceTimer;

    [SerializeField]
    private MeshRenderer targetMeshRenderer;

    private bool isTriggered = false;
    private Color delta;
    private float timeSpan;
    private float offset = 0.0f;

	public void Start()
	{
		this.targetMeshRenderer.material.EnableKeyword("_EMISSION");
            
        this.targetMeshRenderer.material.SetColor(
            "_EmissionColor",
            new Color
            (
                Config.MainScene.MinimumRColorOnSkyviewTransition,
                Config.MainScene.MinimumGColorOnSkyviewTransition,
                Config.MainScene.MinimumBColorOnSkyviewTransition
            )
        );
	}

    /// <summary>
    /// 空の色の遷移を開始する
    /// </summary>
    /// <param name="timeSpan"></param>
	public void Trigger(float timeSpan)
    {
        var rDelta = Config.MainScene.MaximumRColorOnSkyviewTransition - Config.MainScene.MinimumRColorOnSkyviewTransition;
        var gDelta = Config.MainScene.MaximumGColorOnSkyviewTransition - Config.MainScene.MinimumGColorOnSkyviewTransition;
        var bDelta = Config.MainScene.MaximumBColorOnSkyviewTransition - Config.MainScene.MinimumBColorOnSkyviewTransition;

        this.delta = new Color(rDelta, gDelta, bDelta);
        this.timeSpan = timeSpan;
        this.isTriggered = true;
    }

    /// <summary>
    /// 指定した時間から遷移を開始する
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <param name="currentTime"></param>
    public void Trigger(float timeSpan, float currentTime)
	{
        this.offset = currentTime;
        this.Trigger(timeSpan);
	}

    public void Update()
    {
        if(!this.isTriggered)
		{
            return;
		}

        var progression  = (this.offset + this.sourceTimer.ElapsedTime) / this.timeSpan;
        if(progression >= 1.0f)
		{
            progression = 1.0f;
		}

        this.targetMeshRenderer.material.SetColor
        (
            "_EmissionColor",
            new Color
            (
                Config.MainScene.MinimumRColorOnSkyviewTransition + (this.delta.r * progression),
                Config.MainScene.MinimumGColorOnSkyviewTransition + (this.delta.g * progression),
                Config.MainScene.MinimumBColorOnSkyviewTransition + (this.delta.b * progression)
            )
        );
    }
}
