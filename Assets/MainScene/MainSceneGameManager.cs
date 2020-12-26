using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneGameManager : MonoBehaviour
{
	[SerializeField]
	private SelectComponent difficultySelectComponent;

    [SerializeField]
    private StoryComponent storyComponent;

	[SerializeField]
	private GameObject[] sprites;

	[SerializeField]
	private Timer timer;

    private enum State
	{
        DifficultySelection,
        Story,
        Game,
        Result
	}

    private State state;
	private Config.GameBaseBalancing gameBalancing;

	public void Start()
	{
		state = State.DifficultySelection;
	}

	public void Update()
	{
		switch(this.state)
		{
			case State.DifficultySelection:
			{
				var lastClickedObjectName = difficultySelectComponent.LastClickedGameObjectName;
				if(string.IsNullOrEmpty(lastClickedObjectName))
				{
					// 難易度が選ばれるまで待機
					return;
				}

				// ストーリーを読み込む
				string[] story = null;
				var sprites = new List<GameObject>(this.sprites);
				switch(lastClickedObjectName)
				{
					case Config.MainScene.SelectDifficultyEasy:
					{
						story = StoryScript.StoryScriptEasy;
						sprites.Find(s => s.name == Config.MainScene.SpriteDifficultyEasy).SetActive(true);
						this.gameBalancing = new Config.GameBaseBalancing();
						break;
					}

					case Config.MainScene.SelectDifficultyNormal:
					{ 
						story = StoryScript.StoryScriptNormal;
						sprites.Find(s => s.name == Config.MainScene.SpriteDifficultyNormal).SetActive(true);
						this.gameBalancing = new Config.GameBaseBalancing();
						break;
					}

					case Config.MainScene.SelectDifficultyHard:
					{
						story = StoryScript.StoryScriptHard;
						sprites.Find(s => s.name == Config.MainScene.SpriteDifficultyHard).SetActive(true);
						this.gameBalancing = new Config.GameBaseBalancing();
						break;
					}
				}

				this.storyComponent.InitializeStory(story);
				this.storyComponent.GoNextLine();
				this.difficultySelectComponent.gameObject.SetActive(false);
				this.storyComponent.gameObject.SetActive(true);
				this.state = State.Story;
				break;
			 }

			case State.Story:
			{
				if(this.storyComponent.HasNext && Input.GetMouseButtonDown(Config.Global.LeftClick))
				{
					// 次の行があればストーリーを進める
					this.storyComponent.GoNextLine();
				}

				if(!this.storyComponent.HasNext)
				{
					this.storyComponent.gameObject.SetActive(false);
					this.state = State.Game;
					this.timer.Trigger();
				}
				break;
			}

			case State.Game:
			{

				break;
			}

			default:
			{
				break;
			}
		}
	}
}
