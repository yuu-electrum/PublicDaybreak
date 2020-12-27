using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverGameManager : MonoBehaviour
{
    [SerializeField]
    private SelectComponent goToStartMenuSelectComponent;

    [SerializeField]
    private TextMesh targetTextMesh;

    [SerializeField]
    private SkyviewAnimator skyviewAnimator;

    [SerializeField]
    private Timer sourceTimer;

    [SerializeField]
    private GameObject justAwaked;

    [SerializeField]
    private GameObject sleepless;

    public enum GameClearStatus
    {
        Cleared,
        Failed
	}
    
    private GameClearStatus gameClearStatus;

    public void Initialize(float timeSpan, float currentSkyviewTime, GameClearStatus gameClearStatus)
	{
        this.gameClearStatus = gameClearStatus;

        this.skyviewAnimator.Trigger(timeSpan, currentSkyviewTime);
        this.sourceTimer.Trigger();

        switch(this.gameClearStatus)
		{
            case GameClearStatus.Cleared: this.targetTextMesh.text = Config.GameOverScene.ResultMessageCleared; break;
            case GameClearStatus.Failed : this.targetTextMesh.text = Config.GameOverScene.ResultMessageFailed ; break;
		}
	}

	public void Start()
	{
		switch(this.gameClearStatus)
		{
            case GameClearStatus.Cleared: this.sleepless.SetActive(true) ; break;
            case GameClearStatus.Failed : this.justAwaked.SetActive(true); break;
		}
	}

	public void Update()
    {
        if(!string.IsNullOrEmpty(this.goToStartMenuSelectComponent.LastClickedGameObjectName))
		{
            SceneManager.LoadScene("StartScene");
		}
    }
}
