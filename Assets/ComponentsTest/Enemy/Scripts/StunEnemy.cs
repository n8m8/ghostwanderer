using UnityEngine;
using System.Collections;

public class StunEnemy : MonoBehaviour
{
    private PossessController possess;
    public GameObject[] targetEnemy; 
    public GameObject possessedObject;
    public bool isForever = false;
    private PatrolingAI target1;
    private StationAI target2;
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
            if (targetEnemy[i] != null){
                if(checkStunSetting(targetEnemy[i]) == false && possess.isPossessing && targetEnemy[i].GetComponent<PatrolingAI>().getDistance() < 5f)
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
