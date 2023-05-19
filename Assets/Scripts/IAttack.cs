using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    int damage { get; set;}
    void Attack();   
}
