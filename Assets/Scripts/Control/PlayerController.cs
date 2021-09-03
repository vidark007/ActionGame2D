using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Mover mover;
    Fighter fighter;

    bool mouseIsOverUI;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
    }

    private void Update()
    {

        Vector2 moveAmout = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mover.Move(moveAmout, 1f);

        if (Input.GetMouseButton(0) && !mouseIsOverUI)
        {
           fighter.BasicAttack();
        }
    }

    public void MouseIsOverUI(bool statement)
    {
        mouseIsOverUI = statement;
    }
}
