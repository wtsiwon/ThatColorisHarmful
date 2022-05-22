using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SettingWindow : MonoBehaviour
{
    [SerializeField] private GameObject Setting;
    [SerializeField] private Image MusicImage, EffImage;
    [SerializeField] private Sprite OnMusicSprite, OffMusicSprite, OnEffSprite, OffEffSprite;

    private void FixedUpdate()
    {
        if (AudioManager.Instance.MusicCheck)
            MusicImage.sprite = OffMusicSprite;
        else
            MusicImage.sprite = OnMusicSprite;

        if (AudioManager.Instance.EffCheck)
            EffImage.sprite = OffEffSprite;
        else
            EffImage.sprite = OnEffSprite;
    }
    public void MusicClick() => AudioManager.Instance.SetMusicValue();

    public void EffClick() => AudioManager.Instance.SetEffValue();

    public void ExitClick()
    {
        AudioManager.Instance.EffAudioList[1].Play();
        Destroy(gameObject);
        BtnManager.Instance.SettingTurnOnOff = true;
        BtnManager.Instance.play();
    }

    private void Start()
    {
        Setting.GetComponent<RectTransform>().DOScale(new Vector2(1, 1), 1.5f);
        if (SceneManager.GetActiveScene().name == "InGame")
            StartCoroutine(TimeStop());
    }

    IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
    }
}
