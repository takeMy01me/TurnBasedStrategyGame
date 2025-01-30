using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    #region Parameters
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Transform gridDebugObjPrefab;

    public static LevelGrid Instance { get; private set; }
    private GridSystem gridSystem;
    #endregion


    #region 生命周期
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelGrid");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(width, height, cellSize);
        gridSystem.CreateDebugObjects(gridDebugObjPrefab);
    }

    void Start()
    {
        
    }

    #endregion


    #region 对外接口
    public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObj = gridSystem.GetGridObject(gridPosition);
        if (gridObj != null)
        {
            gridObj.SetUnit(unit);
        }
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        Unit unit = null; 
        GridObject gridObj = gridSystem.GetGridObject(gridPosition);
        if (gridObj != null)
        {
            unit = gridObj.GetUnit();
        }
        return unit;
    }

    public void ClearUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObj = gridSystem.GetGridObject(gridPosition);
        gridObj.SetUnit(null);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }
    #endregion
}
