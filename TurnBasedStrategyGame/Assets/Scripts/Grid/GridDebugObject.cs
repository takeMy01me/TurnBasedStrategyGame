using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    #region Parameters
    [SerializeField] TextMeshPro cordText;

    private GridObject gridObj;
    #endregion

    private void Update()
    {
        if ( gridObj != null)
        {
            cordText.text = gridObj.ToString();
        }
    }

    #region ����
    private void Refresh()
    {
        GridPosition gridPos = gridObj.GetGridPosition();
        this.cordText.text = $"{gridPos.x},{gridPos.z}";
    }
    #endregion

    #region ����ӿ�
    public void SetGridObject(GridObject gridObj)
    {
        this.gridObj = gridObj;
        Refresh();
    }
    #endregion
}
