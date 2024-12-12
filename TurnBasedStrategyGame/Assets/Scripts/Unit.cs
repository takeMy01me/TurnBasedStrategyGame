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
    #endregion


    #region 生命周期
    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Start()
    {
        
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
    }
    #endregion


    #region 对外接口
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
    #endregion
}
