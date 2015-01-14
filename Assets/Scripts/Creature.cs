using UnityEngine;
using UnityEditor;
using System.Collections;

public class Creature : MonoBehaviour {

    int headNum;
    int armsNum;
    int legsNum;
    int bodyNum;
    int[] headSize;
    int[] armSize;
    int[] legSize;
    int[] bodySize;

	// Use this for initialization
	void Start () 
    {
        creatureParts();
        creatureSize();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Wait for key input to generate new creature
        if(Input.GetKeyDown("n"))
        {
            Debug.ClearDeveloperConsole();
            creatureParts();
            creatureSize();
        }
	}
    
    //random function, just because
    int getRandomNum(int min,int max)
    {
        int i = Random.Range(min, max);
        return i;
    }

    //function to get random number of parts for the creature
    void creatureParts()
    {
        headNum = getRandomNum(1, 6);
        headSize = new int[1];
        Debug.Log(headNum);
        armsNum = getRandomNum(1, 11);
        armSize = new int[2];
        legsNum = getRandomNum(1, 11);
        legSize = new int[2];
        bodyNum = getRandomNum(1, 6);
        bodySize = new int[bodyNum];
    }

    //function for getting size for the parts
    void creatureSize()
    {
        for(int i = 0; i < 1; i++)
        {
            headSize[i] = getRandomNum(1, 4);
            Debug.Log(headSize[i]);
        }

        for (int i = 0; i < 2; i++)
        {
            armSize[i] = getRandomNum(1, 4);
            //Debug.Log(headSize[i]);
        }

        for (int i = 0; i < 2; i++)
        {
            legSize[i] = getRandomNum(1, 4);
            //Debug.Log(headSize[i]);
        }

        for (int i = 0; i < bodyNum; i++)
        {
            bodySize[i] = getRandomNum(1, 4);
            //Debug.Log(headSize[i]);
        }
    }
}
