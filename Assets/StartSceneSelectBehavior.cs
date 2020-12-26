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
        this.textMesh.color = new Color(1.0f, 0.0f, 0.0f);
        isLastTimeHovered = true;
	}

    public void OnSelectLeave()
	{
        if(!isLastTimeHovered)
        {
            return;
        }

        this.textMesh.color = new Color(1.0f, 1.0f, 1.0f);
        isLastTimeHovered = false;
	}

    public void OnSelectClick()
	{
        Debug.Log("Clicked");
	}
}
