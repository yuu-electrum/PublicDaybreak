/// <summary>
/// ゲームの設定ファイル
/// </summary>
public class Config
{
	/// <summary>
	/// 全般設定
	/// </summary>
	public class Global
	{
		public class SceneName
		{
			public const string StartScene = "StartScene";
			public const string GameScene  = "MainScene";
		}

		public const string SelectTagName = "SelectComponentSelect";

		public const int LeftClick  = 0;
		public const int RightClick = 1;
	}

	/// <summary>
	/// ゲームバランス調整
	/// </summary>
	public class GameBaseBalancing
	{
		// ゲームの時間（秒）
		public virtual float GameSpan { get { return 30.0f; } }

		// 効果決定時のゲージが1週する時間（フレーム）
		public virtual int DecisionGaugeSpan { get { return 720; } }

		// ゲージの加速率
		public virtual float GaugeAcceralation { get { return 0.1f; } }

		// 眠気の蓄積のインターバルフレーム数
		public virtual int SomnolenceFrameInterval { get { return 60; } }

		// 眠気の蓄積量
		public virtual float SomnolenceDelta { get { return 0.01f; } }

		// 眠気の下限
		public virtual float MinimumSomnolence { get { return 0.0f; } }

		// 眠気の上限
		public virtual float MaximumSomnolence { get { return 1.0f; } }

		// 効果決定時の結果による効果増量
		public virtual float EffectMultiplierWithNormal  { get { return 1.0f; } }
		public virtual float EffectMultiplierWithGood    { get { return 1.2f; } }
		public virtual float EffectMultiplierWithPerfect { get { return 1.5f; } }
		public virtual float EffectMultiplierWithBad     { get { return 0.25f; } }

		// コーヒー
		public virtual float SomnolenceSuppressionByCoffee             { get { return 0.1f; } }
		public virtual float SuppressionDecreasingRatePerUseWithCoffee { get { return 0.075f; } }
		public virtual float SuppressionDifficultyWithCoffee           { get { return 1.5f; } }

		// レモン
		public virtual float SomnolenceSuppressionByLemon             { get { return 0.05f; } }
		public virtual float SuppressionDecreasingRatePerUseWithLemon { get { return 0.05f; } }
		public virtual float SuppressionDifficultyWithLemon           { get { return 1.0f; } }

		// エナジードリンク
		public virtual float SomnolenceSuppressionByEnergyDrink             { get { return 0.2f; } }
		public virtual float SuppressionDecreasingRatePerUseWithEnergyDrink { get { return 0.15f; } }
		public virtual float SuppressionDifficultyWithEnergyDrink           { get { return 0.35f; } }

		// 効果決定時のゲージの厳しさ
		public virtual float PrecisionRatioForPerfectOnEffectDecision { get { return 1.5f; } }
		public virtual float PrecisionRatioForGoodOnEffectDecision    { get { return 2.5f; } }
		public virtual float PrecisionRatioForNormalOnEffectDecision  { get { return 3.0f; } }
		public virtual float PrecisionRatioForBadOnEffectDecision     { get { return 4.0f; } }
	}

	public class GameBalancingNormal: GameBaseBalancing
	{
		// ゲームの時間（秒）
		public override float GameSpan { get { return 45.0f; } }

		// 効果決定時のゲージが1週する時間（フレーム）
		public override int DecisionGaugeSpan { get { return 540; } }

		// ゲージの加速率
		public override float GaugeAcceralation { get { return 0.3f; } }

		// 眠気の蓄積のインターバルフレーム数
		public override int SomnolenceFrameInterval { get { return 45; } }

		// 眠気の蓄積量
		public override float SomnolenceDelta { get { return 0.02f; } }

		public override float PrecisionRatioForPerfectOnEffectDecision { get { return 0.75f; } }
		public override float PrecisionRatioForGoodOnEffectDecision    { get { return 1.25f; } }
		public override float PrecisionRatioForNormalOnEffectDecision  { get { return 3.0f; } }
		public override float PrecisionRatioForBadOnEffectDecision     { get { return 5.0f; } }

		// 効果決定時の結果による効果増量
		public override float EffectMultiplierWithNormal  { get { return 0.8f; } }
		public override float EffectMultiplierWithGood    { get { return 1.0f; } }
		public override float EffectMultiplierWithPerfect { get { return 1.25f; } }
		public override float EffectMultiplierWithBad     { get { return 0.05f; } }

		// コーヒー
		public override float SomnolenceSuppressionByCoffee             { get { return 0.35f; } }
		public override float SuppressionDecreasingRatePerUseWithCoffee { get { return 0.1f; } }
		public override float SuppressionDifficultyWithCoffee           { get { return 1.5f; } }

		// レモン
		public override float SomnolenceSuppressionByLemon             { get { return 0.25f; } }
		public override float SuppressionDecreasingRatePerUseWithLemon { get { return 0.05f; } }
		public override float SuppressionDifficultyWithLemon           { get { return 1f; } }

		// エナジードリンク
		public override float SomnolenceSuppressionByEnergyDrink             { get { return 0.5f; } }
		public override float SuppressionDecreasingRatePerUseWithEnergyDrink { get { return 0.3f; } }
		public override float SuppressionDifficultyWithEnergyDrink           { get { return 0.05f; } }
	}

	public class GameBalancingHard: GameBaseBalancing
	{
		// ゲームの時間（秒）
		public override float GameSpan { get { return 60.0f; } }

		// 効果決定時のゲージが1週する時間（フレーム）
		public override int DecisionGaugeSpan { get { return 360; } }

		// ゲージの加速率
		public override float GaugeAcceralation { get { return 0.5f; } }

		// 眠気の蓄積のインターバルフレーム数
		public override int SomnolenceFrameInterval { get { return 30; } }

		public override float PrecisionRatioForPerfectOnEffectDecision { get { return 0.5f; } }
		public override float PrecisionRatioForGoodOnEffectDecision    { get { return 2f; } }
		public override float PrecisionRatioForNormalOnEffectDecision  { get { return 4.0f; } }
		public override float PrecisionRatioForBadOnEffectDecision     { get { return 3.5f; } }

		// 効果決定時の結果による効果増量
		public override float EffectMultiplierWithNormal  { get { return 0.5f; } }
		public override float EffectMultiplierWithGood    { get { return 0.75f; } }
		public override float EffectMultiplierWithPerfect { get { return 1.0f; } }
		public override float EffectMultiplierWithBad     { get { return 0.00f; } }

		// コーヒー
		public override float SomnolenceSuppressionByCoffee             { get { return 0.5f; } }
		public override float SuppressionDecreasingRatePerUseWithCoffee { get { return 0.1f; } }
		public override float SuppressionDifficultyWithCoffee           { get { return 1.25f; } }

		// レモン
		public override float SomnolenceSuppressionByLemon             { get { return 0.35f; } }
		public override float SuppressionDecreasingRatePerUseWithLemon { get { return 0.05f; } }
		public override float SuppressionDifficultyWithLemon           { get { return 1f; } }

		// エナジードリンク
		public override float SomnolenceSuppressionByEnergyDrink             { get { return 0.6f; } }
		public override float SuppressionDecreasingRatePerUseWithEnergyDrink { get { return 0.25f; } }
		public override float SuppressionDifficultyWithEnergyDrink           { get { return 0.075f; } }
	}

	/// <summary>
	/// タイトルシーン
	/// </summary>
	public class StartScene
	{
		public const float RColorOnSelectHovered = 1.0f;
		public const float GColorOnSelectHovered = 0.0f;
		public const float BColorOnSelectHovered = 0.0f;

		public const float RColorOnSelectLeft    = 1.0f;
		public const float GColorOnSelectLeft    = 1.0f;
		public const float BColorOnSelectLeft    = 1.0f;

		public const string SelectStart     = "Start";
		public const string SelectHowToPlay = "HowToPlay";
	}

	/// <summary>
	/// メインゲーム
	/// </summary>
	public class MainScene
	{
		public const string SelectDifficultyEasy   = "Easy";
		public const string SelectDifficultyNormal = "Normal";
		public const string SelectDifficultyHard   = "Hard";

		public const string SpriteDifficultyEasy   = "JustAwaked";
		public const string SpriteDifficultyNormal = "MidnightMovieEditor";
		public const string SpriteDifficultyHard   = "MidnightEventRunner";

		public const string SelectCoffee      = "Coffee";
		public const string SelectLemontea    = "Lemontea";
		public const string SelectEnergyDrink = "EnergyDrink";
			
		public const int InitialCursorRange        = 0;
		public const int MaximumCursorRange        = 3;
		public const int MininmumCursorRange       = 0;
		public const int CursorRangeUpdateInterval = 30;

		public const float MinimumRColorOnSkyviewTransition = 0.098f;
		public const float MinimumGColorOnSkyviewTransition = 0.098f;
		public const float MinimumBColorOnSkyviewTransition = 0.098f;
		public const float MaximumRColorOnSkyviewTransition = 0.549f;
		public const float MaximumGColorOnSkyviewTransition = 0.784f;
		public const float MaximumBColorOnSkyviewTransition = 0.964f;
	}

	/// <summary>
	/// ゲームオーバーシーン
	/// </summary>
	public class GameOverScene
	{
		public const string ResultMessageFailed  = "夜が明ける前に寝てしまいました......";
		public const string ResultMessageCleared = "徹夜成功！";

		public const string SelectGoToTitle = "GoToTitle";
	}
}
