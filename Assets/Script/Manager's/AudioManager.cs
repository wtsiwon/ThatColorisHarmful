using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AudioManager : Singleton<AudioManager>
{

    public List<AudioSource> AudioList = new List<AudioSource>(); //배경 음악소스 리스트
    public List<AudioSource> EffAudioList = new List<AudioSource>(); //효과음 음악소스 리스트

    public bool MusicCheck = false, EffCheck = false;

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
                AudioList[i].volume = 0;//모든 배경음악들의 볼륨을 끈다
            MusicCheck = true;
        }
        else
        {
            for (int i = 0; i < AudioList.Count; i++)
                AudioList[i].volume = 1;//모든 배경음악들의 볼륨을 킨다
            MusicCheck = false;
        }
    }
    public void SetEffValue()
    {
        if (!EffCheck)
        {
            for (int i = 0; i < EffAudioList.Count; i++)
                EffAudioList[i].volume = 0;//모든 효과음들의 볼륨을 끈다
            EffCheck = true;
        }
        else
        {
            for (int i = 0; i < EffAudioList.Count; i++)
                EffAudioList[i].volume = 1;//모든 효과음들의 볼륨을 킨다
            EffCheck = false;
        }
    }
}
