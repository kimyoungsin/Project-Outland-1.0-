using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager SharedInstance = null; //Instance ������ �̱������� ����, �ٸ� ������Ʈ���� ��� ����

    private void Awake()
    {
        if (SharedInstance == null)//Instance�� �ý��ۿ� ���� �� 
        {
            SharedInstance = this;//�ڽ��� �ν��Ͻ��� �־���
            DontDestroyOnLoad(gameObject);//OnLoad(���� �ε�Ǿ�����)�ڽ��� �ı����ϰ� ����

        }
        else
        {
            if (SharedInstance != this)//�ν��Ͻ��� �ڽ��� �ƴ϶�� �̹� �ν��Ͻ��� ����
            {
                Destroy(this.gameObject);//Awake()�� ������ �ڽ� �ı�
            }
        }
    }

    public Sound[] effectSound;
    public Sound[] BgmSound;

    public AudioSource audioSourceBFM; //BGM �����
    public AudioSource[] audioSourceEffects; //ȿ���� ������ ��� �����ϹǷ�

    public string[] playSoundName; //������� ȿ���� �̸� �迭

    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }


    public void PlaySE(string _name)
    {
        for(int i = 0; i < effectSound.Length; i++)
        {
            if(_name == effectSound[i].name)
            {
                for(int j = 0; j< audioSourceEffects.Length; j++)
                {
                    if(!audioSourceEffects[j].isPlaying)
                    {
                        audioSourceEffects[j].clip = effectSound[i].clip;
                        audioSourceEffects[j].Play();
                        playSoundName[j] = effectSound[i].name;
                        return;
                    }
                }
                Debug.Log("��� ���尡 �����.");
                return;
            }
        }
        Debug.Log(_name + "���尡 soundmanager�� ��ϵ��� ����.");
    }

    public void PlayBGM(string _name)
    {
        for(int i = 0; i< BgmSound.Length; i++)
        {
            if(_name == BgmSound[i].name)
            {
                audioSourceBFM.clip = BgmSound[i].clip;
                audioSourceBFM.Play();
                return;
            }
        }
        Debug.Log(_name + "���尡 soundmanager�� ��ϵ��� ����.");
    }

    public void StopAllSE()
    {
        for(int i = 0; i< audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for(int i = 0; i< audioSourceEffects.Length; i++)
        {
            if(playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("�������"+ _name + "���尡 ����.");
    }

}
