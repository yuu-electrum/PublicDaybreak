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

    public void Start()
    {
        this.selectBehaviorInterfaces = new Dictionary<string, ISelectBehavior>();

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
        var ray = sourceCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;

        if(Physics.Raycast(ray, out raycastHit) && raycastHit.collider.gameObject.tag == Config.SelectTagName)
		{
            var hitObject = raycastHit.collider.gameObject;

            // オブジェクトが選択された時の動作を実行する
            this.selectBehaviorInterfaces[hitObject.name]?.OnSelectHover();

            if(Input.GetMouseButtonDown(0))
			{
                // 左クリックされた時の動作を実行する
                this.selectBehaviorInterfaces[hitObject.name]?.OnSelectClick();
			}
		}
        else
		{
            foreach(var select in this.selects)
			{
                // マウスが離れた時の動作を実行する
                this.selectBehaviorInterfaces[select.name]?.OnSelectLeave();
			}
		}
    }
}
