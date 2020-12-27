using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimator : MonoBehaviour
{
    [SerializeField]
    private Light targetLight;

    private int delta = 1;
    private int frame = -1;

    public void Start()
    {
        this.targetLight.range = Config.MainScene.InitialCursorRange;
    }

    public void Update()
    {
        frame++;
        if(frame % Config.MainScene.CursorRangeUpdateInterval != 0)
		{
            return;
		}

        if(this.targetLight.range <= Config.MainScene.MininmumCursorRange || this.targetLight.range >= Config.MainScene.MaximumCursorRange)
		{
            delta *= -1;
		}

        this.targetLight.range += delta;
    }
}
