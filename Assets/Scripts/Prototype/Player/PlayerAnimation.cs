using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    bool attackNow = false;
    CharacterCombat combat;
    public bool inventoryOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
    }


    void Update()
    {
        if (inventoryOpen == false)
        {
            if (Input.GetMouseButton(0))
            {
                attackNow = !attackNow;
                Attacking();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Attacking();
            }
        }
    }


    void Attacking()
    {
        if (attackNow == true)
        {
            anim.SetInteger("Condition", 2);
            attackNow = false;

        } else if (attackNow == false)
        {
          anim.SetInteger("Condition", 0);
        }
        
    }

    public void AttackHitEvent()
    {
        combat.AttackHit_Event();
    }
}
