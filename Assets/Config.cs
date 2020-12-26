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
		// ゲームの時間
		public virtual float GameSpan { get { return 120.0f; } }

		// コーヒー
		public virtual float SomnolenceSuppressionByCoffee             { get { return 5.0f;  } }
		public virtual float SuppressionDecreasingRatePerUseWithCoffee { get { return 0.05f; } }
		public virtual float SuppressionDifficultyWithCoffee           { get { return 1.0f;  } }

		// レモン
		public virtual float SomnolenceSuppressionByLemon             { get { return 2.5f;   } }
		public virtual float SuppressionDecreasingRatePerUseWithLemon { get { return 0.025f; } }
		public virtual float SuppressionDifficultyWithLemon           { get { return 3.0f;   } }

		// エナジードリンク
		public virtual float SomnolenceSuppressionByEnergyDrink             { get { return 7.5f;   } }
		public virtual float SuppressionDecreasingRatePerUseWithEnergyDrink { get { return 0.075f; } }
		public virtual float SuppressionDifficultyWithEnergyDrink           { get { return 0.5f;   } }
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
	}
}
