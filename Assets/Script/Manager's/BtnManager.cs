using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class BtnManager : MonoBehaviour
{
    public static BtnManager Instance { get; set; }

    public Button breakButton;
    public Button nextButton;
    public Button pauseButton;
    public GameObject SettingWindow;
    [SerializeField] private GameObject StartBG;
    public bool SettingTurnOnOff = true;
   
    private void OnEnable() => Instance = this;

    public void PlayClick() => StartCoroutine(PlayCoroutine());
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

            if (GameManager.Instance.OK)
            {
                GameManager.Instance.Next(SlowZone.Instance.instanceobj);
                
            }
        });
    }
    public void Break()
    {
        if (breakButton == null) return;
        breakButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.OK)
            {
                GameManager.Instance.Break(SlowZone.Instance.instanceobj);
                
            }
        });
    }
    
    public void play()
    {
        Time.timeScale = 1;
    }

}
