using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Fire();
        }
    }
}
