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
    public GameObject playerMotion;
    public GameObject player;
    private void Awake() => Instance = this;

    public void PlayClick() => StartCoroutine(PlayCoroutine());
    public void SettingClick()
    {
        if (SettingTurnOnOff == true)
        {
            AudioManager.Instance.EffAudioList[1].Play();
            Instantiate(SettingWindow, GameObject.Find("Canvas").transform);
            SettingTurnOnOff = false;
            Time.timeScale = 0;
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
        breakButton.onClick.AddListener(() =>
        {
            playerMotion.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
            Invoke("Change", 0.1f);
            if (GameManager.Instance.OK)
            {

                GameManager.Instance.Break(SlowZone.Instance.instanceobj);
            }
        });
    }
    public void Change()
    {
        playerMotion.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
    }
    public void play()
    {
        Time.timeScale = 1;
    }

}
