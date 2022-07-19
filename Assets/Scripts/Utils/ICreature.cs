using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICreature : MonoBehaviour
{
    [Header("=== ability settings ===")]
    public bool canAttack;

    [Header("=== figure settings ===")]
    protected float hp;
    protected float attack;

    [Header("=== controllers ===")]
    private AttackController attackController = null;//! �п��ܲ��ᱻ��ֵ��Ҫ������Ϊnull
    private void Awake() {
        if (canAttack) {
            attackController = gameObject.AddComponent<AttackController>();
            attackController.init();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
