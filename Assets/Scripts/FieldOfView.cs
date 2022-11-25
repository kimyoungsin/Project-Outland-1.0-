using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private bool DebugMode = false;
    [Header("View Config")]
    [Range(0f, 360f)] 
    [SerializeField] private float horizontalViewAngle = 0f;
    [SerializeField] private float ViewRadius = 1f;
    [Range(-180f, 180f)]
    [SerializeField] private float ViewRotateZ = 0f;
    [SerializeField] private LayerMask TargetMask;
    [SerializeField] private LayerMask ObstacleMask;

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();
    private float horizontalViewHalfAngle = 0f;

    public Enemy_Chase Chase; //적 인공지능 체이스

    public Transform target = null;

    


    private void Awake()
    {
        horizontalViewHalfAngle = horizontalViewAngle * 0.5f;
    }

    public void Update()
    {
        if(Chase.Warning == true) // 발각 시 플레이어 바라보기
        {
            Vector2 diretion = new Vector2(transform.position.x - target.position.x,
            transform.position.y - target.position.y);

            float angle = Mathf.Atan2(diretion.y, diretion.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 3 * Time.deltaTime);
            transform.rotation = rotation;
        }
        
    }

    private void OnDrawGizmos()
    {
        
        if(DebugMode)
        {
            horizontalViewHalfAngle = horizontalViewAngle * 0.5f;
            Vector3 originPos = transform.position;
            Gizmos.DrawWireSphere(originPos, ViewRadius);

            
            Vector3 horizontalRightDir = AngleToDirZ(horizontalViewHalfAngle + ViewRotateZ);
            Vector3 horiaontalLeftDir = AngleToDirZ(-horizontalViewHalfAngle + ViewRotateZ);
            Vector3 lookDir = AngleToDirZ(ViewRotateZ);

            Debug.DrawRay(originPos, horiaontalLeftDir * ViewRadius, Color.cyan);
            Debug.DrawRay(originPos, lookDir * ViewRadius, Color.green);
            Debug.DrawRay(originPos, horizontalRightDir * ViewRadius, Color.cyan);
        
            FindViewTargets();
        }
        
    }

    public Collider2D[] FindViewTargets()
    {
        hitedTargetContainer.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, ViewRadius, TargetMask);

        foreach(Collider2D hitedTarget in hitedTargets)
        {
            
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(ViewRotateZ);

            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if(angle <= horizontalViewHalfAngle)
            {
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, ViewRadius, ObstacleMask);
                if(rayHitedTarget)
                {
                    if (DebugMode)
                    {
                        Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                    }
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget);
                    Chase.WarningOn(hitedTarget);


                    if (DebugMode)
                    {
                        Debug.DrawLine(originPos, targetPos, Color.red);
                        
                    }
                }
            }
        }

        if(hitedTargetContainer.Count > 0)
        {
            return hitedTargetContainer.ToArray();
        }
        else
        {
            //Chase.WarningOff();
            return null;
        }
    }

    public Vector3 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }

}
