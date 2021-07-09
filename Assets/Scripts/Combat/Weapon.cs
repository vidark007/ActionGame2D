using ActionGame.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    Vector2 direction;
    Camera mainCamera;

    [SerializeField] protected private Transform attackPoint;
    [SerializeField] protected WeaponNames weaponName;

    protected GameObject pooler;
    
    protected virtual void Awake()
    {
        mainCamera = Camera.main;
        pooler = GameObject.Find("Pooler");
    }

    protected virtual void Update()
    {
        WeaponRotation();
    }

    private void WeaponRotation()
    {
        direction = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(direction) - 90);
    }

    private float GetAngleFromVector(Vector2 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);

        //rad To degrees
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }

    public abstract WeaponConfigSO GetEquippedWeapon();

    public abstract void AttackBehavior(float dammage, float range , bool isPlayer);

    
}
