using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneGameManager : MonoBehaviour
{
	[SerializeField]
	private SelectComponent difficultySelectComponent;

	[SerializeField]
	private StoryComponent storyComponent;

	[SerializeField]
	private SelectComponent somnolenceSuppressionSelectComponent;

	[SerializeField]
	private GameObject[] sprites;

	[SerializeField]
	private Timer timer;

	[SerializeField]
	private SkyviewAnimator skyviewAnimator;

	[SerializeField]
	private DecisionGauge decisionGauge;

	[SerializeField]
	private SomnolenceGauge somnolenceGauge;

	private enum State
	{
		DifficultySelection,
		Story,
		GameMain,
		GameEffectDecision,
		GameOver,
		GameClear
	}

	private State state;
	private Config.GameBaseBalancing gameBalancing;
	private string somnolenceSuppressionDrink = "";
	private float currentSomnolence = 0.0f;
	private float somnolenceSuppressionByCoffee;
	private float somnolenceSuppressionByLemontea;
	private float somnolenceSuppressionByEnergyDrink;

	public void Start()
	{
		state = State.DifficultySelection;
	}

	public void Update()
	{
		var isInGame = this.state == State.GameMain || this.state == State.GameEffectDecision;

		if(isInGame && this.timer.ElapsedTime >= this.gameBalancing.GameSpan)
		{
			// ゲームの制限時間を過ぎたらゲーム終了
			this.LoadGameOverScene();
			this.state = State.GameOver;
			return;
		}

		if(isInGame)
		{
			var somnolenceIncreases = this.timer.CurrentFrame % this.gameBalancing.SomnolenceFrameInterval == 0;

			// ゲーム中は常に眠気が溜まる
			this.currentSomnolence += somnolenceIncreases ? this.gameBalancing.SomnolenceDelta : 0.0f;
			this.somnolenceGauge.SetValue(this.currentSomnolence / this.gameBalancing.MaximumSomnolence);

			if(this.currentSomnolence >= this.gameBalancing.MaximumSomnolence)
			{
				// 眠気が限界に達したら終了
				this.LoadGameOverScene();
				this.state = State.GameOver;
				return;
			}
		}

		switch (this.state)
		{
			case State.DifficultySelection:
			{
				var lastClickedObjectName = difficultySelectComponent.LastClickedGameObjectName;
				if (string.IsNullOrEmpty(lastClickedObjectName))
				{
					// 難易度が選ばれるまで待機
					return;
				}

				// ストーリーを読み込む
				string[] story = null;
				var sprites = new List<GameObject>(this.sprites);
				switch (lastClickedObjectName)
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
							this.gameBalancing = new Config.GameBalancingNormal();
							break;
						}

					case Config.MainScene.SelectDifficultyHard:
						{
							story = StoryScript.StoryScriptHard;
							sprites.Find(s => s.name == Config.MainScene.SpriteDifficultyHard).SetActive(true);
							this.gameBalancing = new Config.GameBalancingHard();
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
				if (this.storyComponent.HasNext && Input.GetMouseButtonDown(Config.Global.LeftClick))
				{
					// 次の行があればストーリーを進める
					this.storyComponent.GoNextLine();
				}

				if (!this.storyComponent.HasNext)
				{
					// なければゲームに進む
					this.storyComponent.gameObject.SetActive(false);
					this.somnolenceSuppressionSelectComponent.gameObject.SetActive(true);
					this.state = State.GameMain;
					this.timer.Trigger();
					this.skyviewAnimator.Trigger(this.gameBalancing.GameSpan);
					this.somnolenceGauge.gameObject.SetActive(true);
					this.somnolenceGauge.Initialize(2.5f, 0.0f);
					this.somnolenceSuppressionByCoffee      = this.gameBalancing.SomnolenceSuppressionByCoffee;
					this.somnolenceSuppressionByLemontea    = this.gameBalancing.SomnolenceSuppressionByLemon;
					this.somnolenceSuppressionByEnergyDrink = this.gameBalancing.SomnolenceSuppressionByEnergyDrink;
				}
				break;
			}

			case State.GameMain:
			{
				if(!string.IsNullOrEmpty(this.somnolenceSuppressionSelectComponent.LastClickedGameObjectName))
				{
					// 飲む飲み物が決定されたら最終的な効果を決める段階に移行する
					this.somnolenceSuppressionSelectComponent.gameObject.SetActive(false);
					this.somnolenceSuppressionDrink = this.somnolenceSuppressionSelectComponent.LastClickedGameObjectName;

					var acceralation = this.gameBalancing.GaugeAcceralation * (this.timer.ElapsedTime / this.gameBalancing.GameSpan);
					var gaugeSpan = (float)this.gameBalancing.DecisionGaugeSpan / (1.0f + acceralation);
					switch(this.somnolenceSuppressionDrink)
					{
						case Config.MainScene.SelectCoffee:
						{
							gaugeSpan /= this.gameBalancing.SuppressionDifficultyWithCoffee;
							break;
						}

						case Config.MainScene.SelectLemontea:
						{
							gaugeSpan /= this.gameBalancing.SuppressionDifficultyWithLemon;
							break;
						}

						case Config.MainScene.SelectEnergyDrink:
						{
							gaugeSpan /= this.gameBalancing.SuppressionDifficultyWithEnergyDrink;
							break;
						}
					}

					this.decisionGauge.gameObject.SetActive(true);
					this.decisionGauge.Trigger
					(
						(int)gaugeSpan,
						this.gameBalancing.PrecisionRatioForPerfectOnEffectDecision,
						this.gameBalancing.PrecisionRatioForGoodOnEffectDecision,
						this.gameBalancing.PrecisionRatioForNormalOnEffectDecision,
						this.gameBalancing.PrecisionRatioForBadOnEffectDecision
					);
					this.state = State.GameEffectDecision;
					return;
				}

				break;
			}

			case State.GameEffectDecision:
			{
				if(Input.GetMouseButtonDown(Config.Global.LeftClick))
				{
					var result = this.decisionGauge.Stop();
					
					var multiplier = 1.0f;
					switch(result)
					{
						case DecisionGauge.GaugeResult.Perfect: multiplier = this.gameBalancing.EffectMultiplierWithPerfect; break;
						case DecisionGauge.GaugeResult.Good   : multiplier = this.gameBalancing.EffectMultiplierWithGood   ; break;
						case DecisionGauge.GaugeResult.Normal : multiplier = this.gameBalancing.EffectMultiplierWithNormal ; break;
						case DecisionGauge.GaugeResult.Bad    : multiplier = this.gameBalancing.EffectMultiplierWithBad    ; break;
					}

					var suppression = 0.0f;
					switch(this.somnolenceSuppressionDrink)
					{
						case Config.MainScene.SelectCoffee:
						{
							suppression = this.somnolenceSuppressionByCoffee * multiplier;
							this.somnolenceSuppressionByCoffee *= (1.0f - this.gameBalancing.SuppressionDecreasingRatePerUseWithCoffee);
							break;
						}

						case Config.MainScene.SelectLemontea:
						{
							suppression = this.somnolenceSuppressionByLemontea * multiplier;
							this.somnolenceSuppressionByLemontea *= (1.0f - this.gameBalancing.SuppressionDecreasingRatePerUseWithLemon);
							break;
						}

						case Config.MainScene.SelectEnergyDrink:
						{
							suppression = this.somnolenceSuppressionByEnergyDrink * multiplier;
							this.somnolenceSuppressionByEnergyDrink *= (1.0f - this.gameBalancing.SuppressionDecreasingRatePerUseWithEnergyDrink);
							break;
						}
					}

					this.currentSomnolence -= suppression;
					if(this.currentSomnolence < this.gameBalancing.MinimumSomnolence)
					{
						this.currentSomnolence = this.gameBalancing.MinimumSomnolence;
					}

					if(this.currentSomnolence > this.gameBalancing.MaximumSomnolence)
					{
						this.currentSomnolence = this.gameBalancing.MaximumSomnolence;
					}

					this.decisionGauge.gameObject.SetActive(false);
					this.decisionGauge.Reset();
					this.somnolenceSuppressionSelectComponent.gameObject.SetActive(true);
					this.state = State.GameMain;
					return;
				}
				break;
			}

			case State.GameOver:
			{
				break;
			}

			default:
			{
				break;
			}
		}
	}

	public void LoadGameOverScene()
	{
		SceneManager.sceneLoaded += this.SceneLoaded;
		SceneManager.LoadScene("GameOver");
	}

	public void SceneLoaded(Scene nextScene, LoadSceneMode loadSceneMode)
	{
		var gameManager = GameObject.FindObjectOfType<GameOverGameManager>();

		gameManager.Initialize
		(
			this.gameBalancing.GameSpan,
			this.timer.ElapsedTime,
			this.currentSomnolence >= 1.0f ? GameOverGameManager.GameClearStatus.Failed : GameOverGameManager.GameClearStatus.Cleared
		);

		SceneManager.sceneLoaded -= this.SceneLoaded;
	}
}
