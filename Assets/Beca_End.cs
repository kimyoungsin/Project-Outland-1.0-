using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beca_End : MonoBehaviour
{
     public GameObject[] becagangs;
    public FadeInOut Fadeinout;
    public GameObject FadeObj;
    // Start is called before the first frame update
    void Start()
    {
        Fadeinout = FadeObj.GetComponent<FadeInOut>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(FadeObj, transform.position, Quaternion.identity);
            StartCoroutine(Ambush());

        }
    }

    public IEnumerator Ambush()
    {
        yield return new WaitForSeconds(1.1f);
        for (int i = 0; i < becagangs.Length; i++)
        {
            becagangs[i].SetActive(true);
        }
    }
}
