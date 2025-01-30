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

    #region ��������

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
                return; // ��ֹ��ѡ�е�unit��ѡ�е���һִ֡���ƶ��Ĳ���
            }

            curSelectedUnit?.Move(MouseWorld.Instance.GetPosition());
        }
    }
    #endregion

    #region ����
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

    #region ����ӿ�
    public Unit GetSelectedUnit()
    {
        return curSelectedUnit;
    }
    #endregion
}
