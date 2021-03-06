﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel : MonoBehaviour {

    [SerializeField] private GameObject Target;
    [SerializeField] private Sprite SwitchOnSprite;
    [SerializeField] private Sprite SwitchOffSprite;

    private bool switch_on;
    private bool target_destroied;

    // Use this for initialization
    void Start() {
        switch_on = false;
        GetComponent<SpriteRenderer>().sprite = SwitchOffSprite;
        this.target_destroied = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public void toggle_switch()
    {
        if(switch_on)
        {
            turn_off_switch();
        }
        else if(!target_destroied)
        {
            turn_on_switch();
        }
    }

    private void turn_on_switch()
    {
        //placeholder;
        switch_on = true;
        GetComponent<SpriteRenderer>().sprite = SwitchOnSprite;
        StartCoroutine(KillEnemy(Target));
        target_destroied = true;
		if(GetComponent<AudioSource> () != null)
			GetComponent<AudioSource>().Play ();
    }

    private void turn_off_switch()
    {
        switch_on = false;
        GetComponent<SpriteRenderer>().sprite = SwitchOffSprite;
    }

    private IEnumerator KillEnemy(GameObject enemy)
    {
        if (enemy.GetComponent<PatrolingAI>() != null)
            enemy.GetComponent<PatrolingAI>().isStuned = true;
        else if (enemy.GetComponent<PatrolingAIDistrac>() != null)
            enemy.GetComponent<PatrolingAIDistrac>().isStuned = true;
        else if (enemy.GetComponent<StationAI>() != null)
            enemy.GetComponent<StationAI>().isStuned = true;

        Animator animator = enemy.GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("die", true);

        yield return new WaitForSeconds(3f);
        Destroy(enemy);
    }
}
