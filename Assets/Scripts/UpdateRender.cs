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
        Debug.Log("TPM_script.ghostAvailable " + TPM_script.ghostAvailable);
        Debug.Log("TPM_script.bodyAvailable " + TPM_script.bodyAvailable);


        if (Last_GA != TPM_script.ghostAvailable || Last_BA != TPM_script.bodyAvailable)
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
                        try { GetComponent<Collider2D>().enabled = true; } catch { }
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().enabled = false;
                        try { GetComponent<Collider2D>().enabled = false; } catch { }
                    }
                    break;
                }
            case 1:
                {
                    if (Last_GA)
                    { 
                        GetComponent<SpriteRenderer>().enabled = true;
                        try { GetComponent<Collider2D>().enabled = true; } catch { }
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().enabled = false;
                        try { GetComponent<Collider2D>().enabled = false; } catch { }
                    }
                    break;
                }
            case 2:
                {
                    if (Last_GA && !Last_BA)
                    {
                        GetComponent<SpriteRenderer>().enabled = true;
                        try { GetComponent<Collider2D>().enabled = true; } catch { }
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().enabled = false;
                        try { GetComponent<Collider2D>().enabled = false; } catch { }
                    }
                    break;
                }
        }
    }



}
