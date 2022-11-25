using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cast : MonoBehaviour
{
    public LayerMask isLayer;
    private RaycastHit2D hitInfo;
    private void FixedUpdate()
    {
        hitInfo = Physics2D.CircleCast(transform.position, 1f, Vector2.up, 0f, isLayer);
        if (hitInfo.collider.gameObject.CompareTag("Item"))
        {
            Debug.Log(hitInfo.collider.name);
        }
    }



    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
