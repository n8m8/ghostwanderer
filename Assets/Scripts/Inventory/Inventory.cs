using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public bool[] isFull;
	public string[] slots;

	public void removeItem(int i){
		isFull[i] = false;
		slots[i] = "";
	}

}
