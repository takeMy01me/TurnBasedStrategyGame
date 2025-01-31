using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    #region 参数
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;
    #endregion

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPos = new GridPosition(x, z);
                Debug.DrawLine(GetWorldPosition(gridPos), GetWorldPosition(gridPos) 
                    + Vector3.right * 0.2f, Color.white, 10000f);
                gridObjectArray[x,z] = new GridObject(this, gridPos);
            }
        }
    }

    private Vector3 GetWorldPosition(GridPosition gridPos)
    {
        return new Vector3(gridPos.x, 0, gridPos.z) * cellSize;
    }

    /// <summary>
    /// 创建Debug物体（测试用）
    /// </summary>
    /// <param name="debugPrefab"></param>
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPos = new GridPosition(x, z);
                Transform obj = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPos), Quaternion.identity);
                GridDebugObject gridDebugObject = obj.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPos));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPos)
    {
        return gridObjectArray[gridPos.x, gridPos.z];
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize),
           Mathf.RoundToInt(worldPosition.z / cellSize));
    }
}
