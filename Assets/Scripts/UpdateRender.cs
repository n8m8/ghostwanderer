using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRender : MonoBehaviour {

    public PhaseChangeDetector PCD_script;

    public int spawn_state = 0;

    private void Start()
    {
        update_render();
    }

    private void Update()
    {
        if(PCD_script.Phase_changed)
        {
            update_render();
            //PCD_script.Phase_changed = false;
        }
    }

    private void update_render()
    {
        switch (spawn_state)
        {
            case 0:
                {
                    if (!PCD_script.Last_GA)
                    {
                        GetComponent<SpriteRenderer>().enabled = true;
                        GetComponent<Collider2D>().enabled = true;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().enabled = false;
                        GetComponent<Collider2D>().enabled = false;
                    }
                    break;
                }
            case 1:
                {
                    if (PCD_script.Last_GA)
                    { 
                        GetComponent<SpriteRenderer>().enabled = true;
                        GetComponent<Collider2D>().enabled = true;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().enabled = false;
                        GetComponent<Collider2D>().enabled = false;
                    }
                    break;
                }
        }
    }



}
