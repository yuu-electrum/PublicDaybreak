using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectComponent : MonoBehaviour
{
    [SerializeField]
    private Camera sourceCamera;

    [SerializeField]
    private GameObject[] selects;

    private Dictionary<string, ISelectBehavior> selectBehaviorInterfaces;

    private string lastClickedGameObjectName;

    public void Start()
    {
        this.selectBehaviorInterfaces  = new Dictionary<string, ISelectBehavior>();
        this.lastClickedGameObjectName = "";

        foreach(var select in this.selects)
		{
            var selectBehaviorInterface = select.GetComponent<ISelectBehavior>();
            if(selectBehaviorInterface != null)
            {
                this.selectBehaviorInterfaces.Add(select.name, selectBehaviorInterface);
            }
		}
    }

    public void Update()
    {
        if(this.selectBehaviorInterfaces.Count <= 0)
		{
            return;
		}

        var ray = sourceCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;

        if(Physics.Raycast(ray, out raycastHit) && raycastHit.collider.gameObject.tag == Config.Global.SelectTagName)
		{
            var hitObject = raycastHit.collider.gameObject;

            // オブジェクトが選択された時の動作を実行する
            this.selectBehaviorInterfaces[hitObject.name].OnSelectHover();

            if(Input.GetMouseButtonDown(Config.Global.LeftClick))
			{
                // 左クリックされた時の動作を実行する
                this.selectBehaviorInterfaces[hitObject.name].OnSelectClick();
                this.lastClickedGameObjectName = hitObject.name;
			}
            else
			{
                this.lastClickedGameObjectName = "";
			}
		}
        else
		{
            foreach(var select in this.selects)
			{
                // マウスが離れた時の動作を実行する
                this.selectBehaviorInterfaces[select.name].OnSelectLeave();
			}
            this.lastClickedGameObjectName = "";
		}
    }

    /// <summary>
    /// 最後にクリックされたオブジェクトの名前を取得する
    /// </summary>
    public string LastClickedGameObjectName
	{
        get
		{
            return this.lastClickedGameObjectName;
		}
	}
}
