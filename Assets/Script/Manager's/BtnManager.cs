using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class BtnManager : Single<BtnManager>//dontDestroy¾ø´Â ½Ì±ÛÅæ
{
    

    public Button breakButton;
    public Button nextButton;
    public Button pauseButton;
    public GameObject SettingWindow;
    [SerializeField] private GameObject StartBG;
    public bool SettingTurnOnOff = true, PlayOnOff = false;

    private void Awake() 
    {
        
    }


    public void PlayClick()
    {
        if (PlayOnOff == false)
        {
            PlayOnOff = true;
            StartCoroutine(PlayCoroutine());
        }
    }
    public void SettingClick()
    {
        if (SettingTurnOnOff == true)
        {
            AudioManager.Instance.EffAudioList[1].Play();
            Instantiate(SettingWindow, GameObject.Find("Canvas").transform);
            SettingTurnOnOff = false;
        }
    }
    IEnumerator PlayCoroutine()
    {
        AudioManager.Instance.EffAudioList[0].Play();
        StartBG.GetComponent<Image>().DOFade(0.75f, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Ingame");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Next();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Break();
        }
    }
    public void Next()
    {
        if (nextButton == null) return;
        nextButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Next(GameManager.Instance.instanceobj);
        });
    }
    public void Break()
    {
        if (breakButton == null) return;
        breakButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Break(GameManager.Instance.instanceobj);
        });
    }

    public void play()
    {
        Time.timeScale = 1;
    }

}
