using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EventManager : MonoBehaviour
{
    public FadeInOut Fadeinout;
    public GameObject FadeObj;
    public CinemachineVirtualCamera vcam;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Event()
    {
        Instantiate(FadeObj, transform.position, Quaternion.identity);
        StartCoroutine(FadeInEvent());
    }


    public IEnumerator FadeInEvent()
    {

        yield return new WaitForSeconds(1f);
        transform.Translate(Vector2.up * 64f * Time.deltaTime);

    }
}
