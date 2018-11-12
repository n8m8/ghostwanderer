using UnityEngine;
using System.Collections;

public class StunEnemy : MonoBehaviour
{
    private PossessController possess;
    public GameObject[] targetEnemy; 
    public GameObject possessedObject;
    public bool isForever = false;
    // Use this for initialization
    void Start()
    {
        possess = possessedObject.GetComponent<PossessController>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < targetEnemy.Length; i++)
        {
            if (targetEnemy[i].GetComponent<PatrolingAI>().isStuned == false && possess.isPossessing && targetEnemy[i].GetComponent<PatrolingAI>().getDistance() < 5f)
            {
                StartCoroutine(stunEnemy(targetEnemy[i]));
            }
        }

    }

    IEnumerator stunEnemy(GameObject targetEnemy){
        if (isForever)
        {
            targetEnemy.GetComponent<PatrolingAI>().isStuned = true;
        }
        else
        {
            targetEnemy.GetComponent<PatrolingAI>().isStuned = true;
            yield return new WaitForSeconds(10f);
            targetEnemy.GetComponent<PatrolingAI>().isStuned = false;
        }
    }
}
