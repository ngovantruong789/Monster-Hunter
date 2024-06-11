using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModify : TruongMonoBehaviour
{
    [SerializeField] protected CinemachineVirtualCamera cameraMain;
    [SerializeField] protected PlayerController playerController;

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(LoadCamera), 1f);
    }

    protected void LoadCamera()
    {
        cameraMain = transform.GetComponent<CinemachineVirtualCamera>();
        playerController = FindObjectOfType<PlayerController>();
        cameraMain.Follow = playerController.transform;
    }
}
