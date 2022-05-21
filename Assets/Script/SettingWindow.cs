using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SettingWindow : MonoBehaviour
{
    [SerializeField] private GameObject Setting, X, Music, Eff, Change;

    public void MusicClick() => AudioManager.Instance.SetMusicValue();
    public void EffClick() => AudioManager.Instance.SetEffValue();
    public void ExitClick() => Destroy(gameObject);

    private void Start()
    {
        Setting.GetComponent<RectTransform>().DOSizeDelta(new Vector2(700, 500), 0.7f);
        X.GetComponent<RectTransform>().DOSizeDelta(new Vector2(100, 100), 1.5f);
        Music.GetComponent<RectTransform>().DOSizeDelta(new Vector2(230, 230), 1.5f);
        Eff.GetComponent<RectTransform>().DOSizeDelta(new Vector2(230, 230), 1.5f);
        Change.GetComponent<RectTransform>().DOSizeDelta(new Vector2(550, 100), 1.5f);
    }
}
