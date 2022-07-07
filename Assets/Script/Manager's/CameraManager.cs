using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager Instance { get; set; }
    [SerializeField] private Camera MainCamera;
    Vector3 CameraPos;

    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.05f; //����ũ ũ��
    [SerializeField] [Range(0.1f, 1f)] float duration = 0.5f; //����ũ �Ⱓ
    private void Start()
    {
        Instance = this;
        if(MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }
    public void Shake()
    {
        CameraPos = MainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    }

    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = MainCamera.transform.position;
        cameraPos.x = cameraPosX;
        cameraPos.y = cameraPosY;
        MainCamera.transform.position = cameraPos;
    }
    void StopShake()
    {
        CancelInvoke("StartShake");
        MainCamera.transform.position = CameraPos;
    }

}
