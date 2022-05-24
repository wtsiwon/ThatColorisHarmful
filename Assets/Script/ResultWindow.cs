using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ResultWindow : MonoBehaviour
{
    [SerializeField] private GameObject text, Button1, Button2 , GoTitleClick_BG;
    private void Start()
    {
        text.GetComponent<RectTransform>().DOLocalMoveY(13, 1.2f);
        Button1.GetComponent<RectTransform>().DOLocalMoveY(-128, 1.2f);
        Button2.GetComponent<RectTransform>().DOLocalMoveY(-128, 1.2f);
    }

    public void ReStart()
    {
        AudioManager.Instance.EffAudioList[1].Play();
        SceneManager.LoadScene("InGame");
    }
    public void GoTitle() => StartCoroutine(GotitleCoroutine());
    IEnumerator GotitleCoroutine()
    {
        AudioManager.Instance.EffAudioList[1].Play();
        GoTitleClick_BG.SetActive(true);
        GoTitleClick_BG.GetComponent<Image>().DOFade(1f, 1.2f);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Title");
    }
}
