using UnityEngine;
using System.Collections;

public class StunEnemy : MonoBehaviour
{
    private PossessController possess;
    public GameObject[] targetEnemy; 
    public GameObject possessedObjectTrigger;
    public bool isForever = false;
    private PatrolingAI target1;
    private StationAI target2;
    // Use this for initialization
    void Start()
    {
        possess = possessedObjectTrigger.GetComponent<PossessController>();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < targetEnemy.Length; i++)
        {
            if (targetEnemy[i] != null){
                if(checkStunSetting(targetEnemy[i]) == false && possess.isPossessing && checkDistance(targetEnemy[i]))
                {
                    StartCoroutine(stunEnemy(targetEnemy[i]));
                }
            }
        }

    }

    bool checkStunSetting(GameObject targetEnemy){
        PatrolingAI target = targetEnemy.GetComponent<PatrolingAI>();
        StationAI target2 = targetEnemy.GetComponent<StationAI>();
        if(target == null){
            return target2.isStuned;
        }
        else{
            return target.isStuned;
        }
    }

    bool checkDistance(GameObject targetEnemy){
        PatrolingAI target = targetEnemy.GetComponent<PatrolingAI>();
        StationAI target2 = targetEnemy.GetComponent<StationAI>();
        if (target == null)
        {
            return targetEnemy.GetComponent<StationAI>().getDistance() < 2f;
        }
        else
        {
            return targetEnemy.GetComponent<PatrolingAI>().getDistance() < 2f;
        }

    }

    IEnumerator stunEnemy(GameObject targetEnemy){
        PatrolingAI target = targetEnemy.GetComponent<PatrolingAI>();

        if (target == null)
        {
            if (isForever)
            {
                targetEnemy.GetComponent<StationAI>().isStuned = true;
            }
            else
            {
                targetEnemy.GetComponent<StationAI>().isStuned = true;
                yield return new WaitForSeconds(10f);
                targetEnemy.GetComponent<StationAI>().isStuned = false;
            }
        }
        else
        {
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
}
