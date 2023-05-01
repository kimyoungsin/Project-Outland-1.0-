using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSetting : MonoBehaviour
{

    public int Cursor = 0;
    public Image ResolutionImage;
    public TMP_Text ResolutionText;
    public Image SoundVolumeImage;
    public TMP_Text VolumeText;
    public Image ResetImage;
    public Image BackImage;
    public Sprite[] sprites;
    public GameObject TitleMenu;

    public int SetWidth = 1280;
    public int SetHeight = 720;
    public int DeviceWidth;
    public int DeviceHeight;



    public int ResolutionCursor = 0;
    public int SoundVolumeCursor = 100;
    public int Resolution = 0; //����Ʈ 0: 1280x720, 1: 1980x1080
    public int SoundVolume = 100; //����Ʈ ���� 100


    void Start()
    {
        ResolutionImage.sprite = sprites[4];
        Cursor = 0;
        DeviceWidth = Screen.width;
        DeviceHeight = Screen.height;
        ResolutionSet(1920, 1080);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Cursor == 3)
            {

                Cursor = 0;
                ResolutionImage.sprite = sprites[4];
                SoundVolumeImage.sprite = sprites[1];
                ResetImage.sprite = sprites[2];
                BackImage.sprite = sprites[3];
            }
            else if (Cursor == 2)
            {

                Cursor += 1;
                ResolutionImage.sprite = sprites[0];
                SoundVolumeImage.sprite = sprites[1];
                ResetImage.sprite = sprites[2];
                BackImage.sprite = sprites[7];
            }
            else if (Cursor == 1)
            {

                Cursor += 1;
                ResolutionImage.sprite = sprites[0];
                SoundVolumeImage.sprite = sprites[1];
                ResetImage.sprite = sprites[6];
                BackImage.sprite = sprites[3];
            }
            else if (Cursor == 0)
            {

                Cursor += 1;
                ResolutionImage.sprite = sprites[0];
                SoundVolumeImage.sprite = sprites[5];
                ResetImage.sprite = sprites[2];
                BackImage.sprite = sprites[3];
            }




        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Cursor == 0)
            {

                Cursor = 3;
                ResolutionImage.sprite = sprites[0];
                SoundVolumeImage.sprite = sprites[1];
                ResetImage.sprite = sprites[2];
                BackImage.sprite = sprites[7];
            }
            else if (Cursor == 1)
            {

                Cursor -= 1;
                ResolutionImage.sprite = sprites[4];
                SoundVolumeImage.sprite = sprites[1];
                ResetImage.sprite = sprites[2];
                BackImage.sprite = sprites[3];
            }
            else if (Cursor == 2)
            {

                Cursor -= 1;
                ResolutionImage.sprite = sprites[0];
                SoundVolumeImage.sprite = sprites[5];
                ResetImage.sprite = sprites[2];
                BackImage.sprite = sprites[3];
            }
            else if (Cursor == 3)
            {

                Cursor -= 1;
                ResolutionImage.sprite = sprites[0];
                SoundVolumeImage.sprite = sprites[1];
                ResetImage.sprite = sprites[6];
                BackImage.sprite = sprites[3];
            }


        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Cursor == 0)
            {
                if (ResolutionCursor == 1)
                {
                    ResolutionCursor = 0;
                }
                else
                {
                    ResolutionCursor += 1;
                }
                ResolutionTextSetting();
            }
            else if (Cursor == 1)
            {
                if (SoundVolumeCursor == 100)
                {
                    SoundVolumeCursor = 0;
                    VolumeText.text = "" + SoundVolumeCursor;
                }
                else
                {
                    SoundVolumeCursor += 20;
                    VolumeText.text = "" + SoundVolumeCursor;
                }
            }






        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Cursor == 0)
            {
                if (ResolutionCursor == 0)
                {
                    ResolutionCursor = 1;
                }
                else
                {
                    ResolutionCursor -= 1;
                }
                ResolutionTextSetting();
            }
            else if (Cursor == 1)
            {
                if (SoundVolumeCursor == 0)
                {
                    SoundVolumeCursor = 100;
                    VolumeText.text = "" + SoundVolumeCursor;
                }
                else
                {
                    SoundVolumeCursor -= 20;
                    VolumeText.text = "" + SoundVolumeCursor;
                }
            }





        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            if (Cursor == 0) //�ػ�
            {
                if (ResolutionCursor == 0)
                {
                    ResolutionSet(1280, 720);
                }
                else if (ResolutionCursor == 1)
                {
                    ResolutionSet(1920, 1080);
                }
            }
            else if (Cursor == 1) //����
            {
                if (SoundVolumeCursor == 100)
                {

                }
                else if (SoundVolumeCursor == 1)
                {

                }
            }
            else if (Cursor == 2) //���� �ʱ�ȭ
            {
                ResolutionSet(1280, 720); //����Ʈ 0: 1280x720, 1: 1980x1080
                ResolutionCursor = 0;
                SoundVolume = 100; //����Ʈ ���� 100
                SoundVolumeCursor = 100;
            }
            else if (Cursor == 3) //Ÿ��Ʋ�� ���ư���
            {
                TitleMenu.SetActive(true);
                gameObject.SetActive(false);
            }

        }
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Escape))
        {
            TitleMenu.SetActive(true);
            gameObject.SetActive(false);

        }
    }


    public void ResolutionTextSetting()
    {
        if (ResolutionCursor == 0) //
        {
            ResolutionText.text = "1280x720";

        }
        else if (ResolutionCursor == 1) //
        {
            ResolutionText.text = "1920x1080";

        }
    }

    public void ResolutionSet(int Width, int Height)
    {
        SetWidth = Width;
        SetHeight = Height;
        Screen.SetResolution(SetWidth, (int)(((float)DeviceHeight / DeviceWidth) * SetWidth), true); // SetResolution �Լ� ����� ����ϱ�
        if ((float)SetWidth / SetHeight < (float)DeviceWidth / DeviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)SetWidth / SetHeight) / ((float)DeviceWidth / DeviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)DeviceWidth / DeviceHeight) / ((float)SetWidth / SetHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }

}
