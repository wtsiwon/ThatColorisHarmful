using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AudioManager : Singleton<AudioManager>
{

    public List<AudioSource> AudioList = new List<AudioSource>(); //��� ���Ǽҽ� ����Ʈ
    public List<AudioSource> EffAudioList = new List<AudioSource>(); //ȿ���� ���Ǽҽ� ����Ʈ

    private bool MusicCheck = false, EffCheck = false;

    private void Start() => StartSetting();


    void StartSetting()
    {
        #region Music
        switch (SceneManager.GetActiveScene().name)
        {
            case "Title":
                AudioList[1].Stop();
                AudioList[0].Play();
                break;
            case "Ingame":
                AudioList[0].Stop();
                AudioList[1].Play();
                break;
        }
        #endregion
    }

    public void SetMusicValue()
    {
        if(!MusicCheck)
        {
            for (int i = 0; i < AudioList.Count; i++)
                AudioList[i].volume = 0;//��� ������ǵ��� ������ ����
            MusicCheck = true;
        }
        else
        {
            for (int i = 0; i < AudioList.Count; i++)
                AudioList[i].volume = 1;//��� ������ǵ��� ������ Ų��
            MusicCheck = false;
        }
    }
    public void SetEffValue()
    {
        if (!EffCheck)
        {
            for (int i = 0; i < EffAudioList.Count; i++)
                EffAudioList[i].volume = 0;//��� ȿ�������� ������ ����
            EffCheck = true;
        }
        else
        {
            for (int i = 0; i < EffAudioList.Count; i++)
                EffAudioList[i].volume = 1;//��� ȿ�������� ������ Ų��
            EffCheck = false;
        }
    }
}
