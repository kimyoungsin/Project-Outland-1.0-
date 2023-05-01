using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSystem : MonoBehaviour
{
    public static MainSystem SharedInstance = null; //Instance ������ �̱������� ����, �ٸ� ������Ʈ���� ��� ����


    //public int Metal = 0;
    private void Awake()
    {
        if (SharedInstance == null)//Instance�� �ý��ۿ� ���� �� 
        {
            SharedInstance = this;//�ڽ��� �ν��Ͻ��� �־���
            DontDestroyOnLoad(gameObject);//OnLoad(���� �ε�Ǿ�����)�ڽ��� �ı����ϰ� ����

        }
        else
        {
            if(SharedInstance != this)//�ν��Ͻ��� �ڽ��� �ƴ϶�� �̹� �ν��Ͻ��� ����
            {
                Destroy(this.gameObject);//Awake()�� ������ �ڽ� �ı�
            }
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
