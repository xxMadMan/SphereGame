using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    #region Singleton
    public static CharacterCombat instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    //public event System.Action OnAttack;

    CharacterStats myStats;
    public CharacterStats opponentStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    public void Attack(CharacterStats targetStats)
    {
        opponentStats = targetStats.GetComponent<CharacterStats>();
    }



    public void AttackHit_Event()
    {
        if (opponentStats != null)
            opponentStats.TakeDamage(myStats.damage.GetValue());
    }
}
