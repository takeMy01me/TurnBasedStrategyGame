using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    #region Parameters
    [SerializeField] private LayerMask mousePlaneLayerMask;
    [SerializeField] private Transform mouseTarget;

    private static MouseWorld m_instance;
    public static MouseWorld Instance
    {
        get
        {
            return m_instance;
        }
    }
    #endregion


    #region 生命周期
    void Awake()
    {
        m_instance = this;
    }

    void Update()
    {
        this.mouseTarget.position = Input.mousePosition;
    }
    #endregion


    #region 对外接口
    public Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, this.mousePlaneLayerMask);

        return raycastHit.point;
    }

    #endregion
}
