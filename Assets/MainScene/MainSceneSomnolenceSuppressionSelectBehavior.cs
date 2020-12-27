using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneSomnolenceSuppressionSelectBehavior : MonoBehaviour, ISelectBehavior
{
    [SerializeField]
    private GameObject cursor;

    private bool isLastTimeHovered;

    public void Start()
    {
        isLastTimeHovered = false;
    }

    public void Update()
    {
        
    }

    public void OnSelectHover()
	{
        if(isLastTimeHovered)
		{
            return;
		}

        cursor.transform.position = this.gameObject.transform.position;
        cursor.SetActive(true);

        isLastTimeHovered = true;
	}

    public void OnSelectLeave()
	{
        if(!isLastTimeHovered)
        {
            return;
        }

        cursor.SetActive(false);

        isLastTimeHovered = false;
	}

    public void OnSelectClick()
	{

	}

	public void OnDisable()
	{
        if(cursor != null)
        {
		    cursor.SetActive(false);
        }
	}
}
