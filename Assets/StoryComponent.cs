using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryComponent : MonoBehaviour
{
    [SerializeField]
    private TextMesh targetTextMesh;

    private string[] scripts;
    private int currentLine = -1;

    /// <summary>
    /// ストーリーを表示する準備をする
    /// </summary>
    /// <param name="storyScripts"></param>
    public void InitializeStory(string[] storyScripts)
	{
        this.scripts     = storyScripts;
        this.currentLine = -1;
	}

    /// <summary>
    /// 次の行に進む
    /// </summary>
    public void GoNextLine()
	{
        this.currentLine++;

        if(!this.HasNext || this.targetTextMesh == null)
		{
            return;
		}

        this.targetTextMesh.text = this.scripts[this.currentLine];
	}

    public bool HasNext
	{
        get
		{
            return this.scripts.Length > this.currentLine;
		}
	}

    public int CurrentLine
	{
        get
		{
            return this.currentLine;
		}
	}
}
