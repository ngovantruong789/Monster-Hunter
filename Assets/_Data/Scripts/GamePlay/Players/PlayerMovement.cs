using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : TruongMonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    public PlayerController PlayerController => playerController;

    [SerializeField] protected Vector2 inputMove;
    public Vector2 InputMove => inputMove;

    [SerializeField] protected float speed;
    public float Speed => speed;

    [SerializeField] protected float limitX = 19.73f;
    [SerializeField] protected float limitY = 13.12f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerController();
        LoadSpeed();
    }

    protected void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    protected void LoadSpeed()
    {
        speed = playerController.PlayerSO.speed;
    }

    protected void OnMove(InputValue value)
    {
        inputMove = value.Get<Vector2>();
    }

    public void Movement()
    {
        transform.parent.Translate(inputMove * speed * Time.deltaTime);
        RotateDirection();
        MovementLimit();
    }

    protected void RotateDirection()
    {
        Vector3 scale = transform.parent.localScale;
        if (inputMove.x < 0) scale.x = -1;
        if (inputMove.x > 0) scale.x = 1;
        transform.parent.localScale = new Vector3(scale.x, scale.y);
    }

    protected void MovementLimit()
    {
        Vector3 parentPos = transform.parent.position;
        parentPos.x = Mathf.Clamp(parentPos.x, -limitX, limitX);
        parentPos.y = Mathf.Clamp(parentPos.y, -limitY, limitY);
        transform.parent.position = parentPos;
    }
}
