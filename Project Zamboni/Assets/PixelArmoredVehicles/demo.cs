using UnityEngine;
using System.Collections;

public class demo : MonoBehaviour {

    public CarBehavior[] cars;
    public int current = 0;


	void Start () {
        cars = GameObject.FindObjectsOfType<CarBehavior>();
        OFF();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) & current<cars.Length-1) { OFF(); current++; cars[current].enabled = true; }
        if (Input.GetKeyDown(KeyCode.Q) & current>0) { OFF(); current--; cars[current].enabled = true; }
	}
    void OFF()
    {
        for (int c = 0; c < cars.Length; c++) { 
            cars[c].enabled = false;
        }
    }
}


