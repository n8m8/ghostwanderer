using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRender : MonoBehaviour {

    public TestPlayerMove TPM_script;

    public int spawn_state = 0;

    public bool Phase_changed { get; set; }
    public bool Last_GA { get; set; }
    public bool Last_BA { get; set; }

    private void Start()
    {
        update_render();

        this.Phase_changed = false;
        this.Last_GA = TPM_script.ghostAvailable;
        this.Last_BA = TPM_script.bodyAvailable;
    }

    private void Update()
    {
        if (Last_GA != TPM_script.ghostAvailable && Last_BA != TPM_script.ghostAvailable)
        {
            Debug.Log("phase changed");
            Last_GA = TPM_script.ghostAvailable;
            Last_BA = TPM_script.bodyAvailable;
            Phase_changed = true;
        }
        if (Phase_changed)
        {
            update_render();
            Phase_changed = false;
        }
    }

    private void update_render()
    {


        switch (spawn_state)
        {
            case 0:
                {
                    if (!Last_GA)
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
                    if (Last_GA)
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
