using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private bool DebugMode = false; //ON 이면 기즈모 켤시 범위 보임
    [Header("뷰 설정")]
    [Range(0f, 360f)]  //아래 파랑색 선 범위 최소~최대값 설정
    [SerializeField] private float horizontalViewAngle = 0f; //부채꼴 범위 내부 파랑색 선 2개 각도조절
    [SerializeField] private float ViewRadius = 1f; //시야범위
    [Range(-180f, 180f)] //아래 뷰 회전각 최소~최대 범위 설정
    [SerializeField] private float ViewRotateZ = 0f; //뷰 부채꼴 범위 회전용
    [SerializeField] private LayerMask TargetMask; //부채꼴 내부 추적할 대상 레이어 설정
    [SerializeField] private LayerMask ObstacleMask; //부채꼴 내부 가려지는 레이어 설정

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();
    private float horizontalViewHalfAngle = 0f;

    public Enemy_Chase Chase; //적 인공지능 체이스

    public Transform target = null;
    public bool Attention = false; //t면 플에이어 바라봄





    private void Awake()
    {
        horizontalViewHalfAngle = horizontalViewAngle * 0.5f;
    }

    public void Update()
    {
        if(Attention == true) // 발각 시 플레이어 바라보기
        {
            Vector2 diretion = new Vector2(transform.position.x - target.position.x,
            transform.position.y - target.position.y);

            float angle = Mathf.Atan2(diretion.y, diretion.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 270f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 3 * Time.deltaTime);
            transform.rotation = rotation;
        }
        
    }

    public void TargetAttention()
    {
        Attention = true;

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
