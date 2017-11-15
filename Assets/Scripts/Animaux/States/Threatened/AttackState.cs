﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<GameObject>
{
    private static AttackState instance;
    private float timer;

    private AttackState() { }

    public static AttackState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AttackState();
            }
            return instance;
        }
    }

    override public void Enter(GameObject o) {
        // Get the current agent variables
        StateMachine FSM = o.GetComponent<StateMachine>();
        FSM.animator.Play("Attack");

        // Set the number of the the state is play
        FSM.timeIdle = 0.7f;
    }

    override public void Execute(GameObject o) {
        // Get the current agent variables
        StateMachine FSM = o.GetComponent<StateMachine>();

        float currTime = o.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (currTime >= FSM.timeIdle - 0.06)
        {
            AgentProperties properties = o.GetComponent<AgentProperties>();
            GameObject player = GameObject.FindWithTag("Player");

            // check if we can attack the player
            if (!player.GetComponent<AgentProperties>().isDead &&
                (player.transform.position - o.GetComponent<Transform>().position).magnitude < 5.0f) {
                // Decrease the current life of the player
                // Normalement => void JudyIsHurtByAnAnimal(float lifeLoosed)
                Debug.Log("Take that ! dmg:" + properties.damages);
                player.GetComponent<AgentProperties>().takeDamages(properties.damages);
            }
            FSM.ChangeState(PursuitState.Instance);
        }
        
    }

    override public void Exit(GameObject o) {
        StateMachine FSM = o.GetComponent<StateMachine>();
    }

}
