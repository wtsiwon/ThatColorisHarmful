using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SettingWindow : MonoBehaviour
{
    [SerializeField] private GameObject Setting;

    public void MusicClick() => AudioManager.Instance.SetMusicValue();
    public void EffClick() => AudioManager.Instance.SetEffValue();
    public void ExitClick()
    {
        Destroy(gameObject);
        BtnManager.Instance.SettingTurnOnOff = true;
    }

    private void Start()
    {
        Setting.GetComponent<RectTransform>().DOScale(new Vector2(1, 1), 1.5f);
    }
}
