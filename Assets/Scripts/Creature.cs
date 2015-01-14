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
        
        if(Input.GetKeyDown("n"))
        {
            Debug.ClearDeveloperConsole();
            creatureParts();
            creatureSize();
        }
	}
    
    int getRandomNum(int min,int max)
    {
        int i = Random.Range(min, max);
        return i;
    }

    void creatureParts()
    {
        headNum = getRandomNum(1, 6);
        headSize = new int[headNum];
        Debug.Log(headNum);
        armsNum = getRandomNum(1, 11);
        armSize = new int[armsNum];
        legsNum = getRandomNum(1, 11);
        legSize = new int[legsNum];
        bodyNum = getRandomNum(1, 6);
        bodySize = new int[bodyNum];
    }

    void creatureSize()
    {
        for(int i = 0; i < headNum;i++)
        {
            headSize[i] = getRandomNum(1, 4);
            Debug.Log(headSize[i]);
        }

        for (int i = 0; i < armsNum; i++)
        {
            armSize[i] = getRandomNum(1, 4);
            //Debug.Log(headSize[i]);
        }

        for (int i = 0; i < legsNum; i++)
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
