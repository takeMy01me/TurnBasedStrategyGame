using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    #region Parameters
    [SerializeField] private LayerMask unitsLayerMask;

    private static UnitActionSystem instance;
    public static UnitActionSystem Instance { get { return instance; } }

    private Unit curSelectedUnit;

    public event EventHandler OnSelectedUnitChanged;
    #endregion

    #region 生命周期

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (HandleUnitSelection())
            {
                return; // 防止新选中的unit在选中的这一帧执行移动的操作
            }

            curSelectedUnit?.Move(MouseWorld.Instance.GetPosition());
        }
    }
    #endregion

    #region 方法
    private bool HandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitsLayerMask))
        {
            if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        curSelectedUnit = unit;

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region 对外接口
    public Unit GetSelectedUnit()
    {
        return curSelectedUnit;
    }
    #endregion
}
