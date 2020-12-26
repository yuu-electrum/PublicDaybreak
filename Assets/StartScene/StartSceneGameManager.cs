using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneGameManager : MonoBehaviour
{
    [SerializeField]
    private SelectComponent selector;

    public void Start()
    {
        
    }

    public void Update()
    {
        if(string.IsNullOrEmpty(selector.LastClickedGameObjectName))
		{
            return;
		}

        switch(selector.LastClickedGameObjectName)
		{
            case Config.StartScene.SelectStart:
                SceneManager.LoadScene(Config.Global.SceneName.GameScene);
                break;

            case Config.StartScene.SelectHowToPlay:

                break;
		}
    }
}
