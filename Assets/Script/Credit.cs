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
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("Title");
    }
    IEnumerator StartCredit()
    {
        yield return new WaitForSeconds(0.3f);
        text.text = "������\n��ȹ �� UI, ��� �׷���";
        text.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        text.text = "��ÿ�\n���α׷���";
        text.color += new Color(0, 0, 0, 1);
        text.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        text.text = "������\n���α׷���";
        text.color += new Color(0, 0, 0, 1);
        text.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Title");
    }
}
