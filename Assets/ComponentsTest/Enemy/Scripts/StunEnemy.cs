using UnityEngine;
using System.Collections;

public class StunEnemy : MonoBehaviour
{
    private PossessController possess;
    public GameObject targetEnemy; 
    public GameObject possessedObject;
    // Use this for initialization
    void Start()
    {
        possess = possessedObject.GetComponent<PossessController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetEnemy.GetComponent<PatrolingAI>().isStuned == false && possess.isPossessing && targetEnemy.GetComponent<PatrolingAI>().getDistance() < 5f){
            StartCoroutine(stunEnemy());
        }

    }

    IEnumerator stunEnemy(){
        targetEnemy.GetComponent<PatrolingAI>().isStuned = true;
        yield return new WaitForSeconds(10f);
        targetEnemy.GetComponent<PatrolingAI>().isStuned = false;
    }
}
