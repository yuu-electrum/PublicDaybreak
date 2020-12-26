using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneSelectBehavior : MonoBehaviour, ISelectBehavior
{
    private TextMesh textMesh;

    private bool isLastTimeHovered;

    public void Start()
    {
        this.textMesh = this.GetComponent<TextMesh>();
        isLastTimeHovered = false;
    }

    public void Update()
    {
        
    }

    public void OnSelectHover()
	{
        this.textMesh.color = new Color
        (
            Config.StartScene.RColorOnSelectHovered,
            Config.StartScene.GColorOnSelectHovered,
            Config.StartScene.BColorOnSelectHovered
        );
        isLastTimeHovered = true;
	}

    public void OnSelectLeave()
	{
        if(!isLastTimeHovered)
        {
            return;
        }

        this.textMesh.color = new Color
        (
            Config.StartScene.RColorOnSelectLeft,
            Config.StartScene.GColorOnSelectLeft,
            Config.StartScene.BColorOnSelectLeft
        );
        isLastTimeHovered = false;
	}

    public void OnSelectClick()
	{

	}
}
