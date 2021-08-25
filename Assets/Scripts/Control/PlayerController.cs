using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Mover mover;
    Fighter fighter;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
    }

    void InteractWithPickup()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.forward, 100f, 1 << 9);
 

        foreach (RaycastHit2D item in hits)
        {
            Debug.Log(item.collider.transform.name);
        }

        
    }

    private void Update()
    {

        //InteractWithPickup();

        Vector2 moveAmout = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mover.Move(moveAmout, 1f);

        if (Input.GetMouseButton(0))
        {
            fighter.BasicAttack();
        }
    }
}
