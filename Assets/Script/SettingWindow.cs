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
        Setting.GetComponent<RectTransform>().DOSizeDelta(new Vector2(798.8884f, 608.8347f), 0.4f);
        X.GetComponent<RectTransform>().DOSizeDelta(new Vector2(100, 100), 1.5f);
        Music.GetComponent<RectTransform>().DOSizeDelta(new Vector2(250, 250), 1.5f);
        Eff.GetComponent<RectTransform>().DOSizeDelta(new Vector2(250, 250), 1.5f);
        Change.GetComponent<RectTransform>().DOSizeDelta(new Vector2(537, 225), 1.5f);
    }
}
