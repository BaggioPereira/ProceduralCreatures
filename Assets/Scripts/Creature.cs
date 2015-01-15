using UnityEngine;
using UnityEditor;
using System.Collections;

public class Creature : MonoBehaviour {

    private GameObject Creatures;
    private ArrayList myNodes;

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

        myNodes = new ArrayList();

        //for (int i = 0; i < 7; i++)
        //{
        //    Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    Creatures.transform.position = new Vector3((Random.Range(-10, 11)), (Random.Range(-10, 11)), 0f);
        //    Creatures.tag = "Player";
        //    myNodes.Add(Creatures);
        //}
        create();
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

            //Test for creating and deleteing cube meshes

            GameObject[] GameObjects = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < GameObjects.Length; i++)
            {
                Destroy(GameObjects[i]);
            }

            //for (int i = 0; i < 7; i++)
            //{
            //    Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //    Creatures.transform.position = new Vector3((Random.Range(-10, 11)), (Random.Range(-10, 11)), 0f);
            //    Creatures.tag = "Player";
            //    myNodes.Add(Creatures);
            //}
            create();
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
        armsNum = getRandomNum(2, 11);
        armSize = new int[2];
        legsNum = getRandomNum(2, 11);
        legSize = new int[2];
        Debug.Log(legsNum);
        bodyNum = getRandomNum(1, 6);
        bodySize = new int[bodyNum];
    }

    //function for getting size for the parts
    void creatureSize()
    {
        for(int i = 0; i < 1; i++)
        {
            headSize[i] = getRandomNum(1, 4);
            //Debug.Log(headSize[i]);
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

    //creates creature according to values provided
    void create()
    {
        int x = 0, y = 0, z = 0;
        for(int i = 0; i < legSize.Length; i++)
        {
            for (int j = 0; j < legsNum; j++)
            {
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x,y,z);
                Creatures.transform.localScale = new Vector3(legSize[i], legSize[i] * 3.0f, legSize[i]);
                Creatures.tag = "Player";
                myNodes.Add(Creatures);
                x = legSize[i] * 3;
            }
            y = legSize[i] * 3;
        }
    }
}
