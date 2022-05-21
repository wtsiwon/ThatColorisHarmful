using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Credit : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Start() => StartCoroutine(StartCredit());
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Title");
    }
    IEnumerator StartCredit()
    {
        yield return new WaitForSeconds(0.3f);
        text.text = "조찬우\n기획 및 UI, 배경 그래픽";
        text.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        text.text = "김시원\n프로그래밍";
        text.color += new Color(0, 0, 0, 1);
        text.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        text.text = "박현수\n프로그래밍";
        text.color += new Color(0, 0, 0, 1);
        text.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Title");
    }
}
