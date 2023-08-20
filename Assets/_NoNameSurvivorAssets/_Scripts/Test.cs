using System.Collections;
using System.Collections.Generic;
using FikretGezer;
using UnityEngine;

public abstract class Test : MonoBehaviour, IDamageable
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.A)) Interact();   
    }
    public virtual void Interact()
    {
        Die();
    }

    public void Die()
    {
        Debug.Log("<color=yellow>DIED</color>");
    }
}
