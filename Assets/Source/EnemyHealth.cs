using UnityEngine;
using System.Collections;
using System;

public class EnemyHealth : CharacterHealth
{
    protected override void Start()
    {
        
    }

    protected override void Update()
    {

    }

    protected override void OnCharacterDeath()
    {
        Destroy(gameObject);
    }
}