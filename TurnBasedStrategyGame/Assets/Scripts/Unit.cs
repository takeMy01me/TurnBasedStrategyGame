using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Parameters
    [SerializeField] private Animator unitAnimator;

    private Vector3 targetPosition;
    private float stoppingDistance = 0.1f;
    private float moveSpeed = 4f;
    private float moveRotateSpeed = 10f;

    private GridPosition gridPos;
    #endregion


    #region 生命周期
    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Start()
    {
        gridPos = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPos, this);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * moveRotateSpeed);

            unitAnimator.SetBool("IsWalking", true);

        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }

        GridPosition newGridPos = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPos != gridPos) // Unit changed GridPosition
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPos, newGridPos);
            gridPos = newGridPos;
        }
    }
    #endregion


    #region 对外接口
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
    #endregion
}
