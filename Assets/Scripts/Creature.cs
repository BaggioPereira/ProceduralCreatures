using UnityEngine;
using UnityEditor;
using System.Collections;

public class Creature : MonoBehaviour {
    private GameObject Creatures;
    private ArrayList myNodes;

    private 

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
        createLegs();
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
            createLegs();
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
        headNum = getRandomNum(1, 4);
        headSize = new int[1];
        armsNum = getRandomNum(1, 6);
        armSize = new int[2];
        legsNum = getRandomNum(1, 6);
        legSize = new int[2];
        //Debug.Log(legsNum);
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
        float x = 0, y = 0, z = 0;
        y = bodySize[0]*0.5f;
        for (int i = 0; i<bodyNum; i++)
        {
            Creatures = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Creatures.transform.position = new Vector3(x, y, z);
            Creatures.transform.localScale = new Vector3(bodySize[i], bodySize[i], bodySize[i]);
            Creatures.tag = "Player";
            myNodes.Add(Creatures);
            y = y + bodySize[i];
        }
    }

    void createLegs()
    {
        float x = 0, y = 0, z = 0;
        bool lower = true;
        for(int i = 0; i < legSize.Length; i++)
        {
            y = y + legSize[i] * 0.75f;   
            for (int j = 0; j < legsNum; j++)
            {
                z = legSize[0]*1.25f; 
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x,y,z);
                Creatures.transform.localScale = new Vector3(legSize[i]*0.5f, legSize[i]*0.5f * 3.0f, legSize[i]*0.5f);
                if(lower)
                {
                    Creatures.tag = Tags.lower_leg;
                }
                else
                {
                    Creatures.tag = Tags.upper_leg;
                }
                myNodes.Add(Creatures);
                z = legSize[0]*-1.25f;
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x, y, z);
                Creatures.transform.localScale = new Vector3(legSize[i] * 0.5f, legSize[i] * 0.5f * 3.0f, legSize[i] * 0.5f);
                if (lower)
                {
                    Creatures.tag = Tags.lower_leg;
                }
                else
                {
                    Creatures.tag = Tags.upper_arm;
                }
                myNodes.Add(Creatures);
                x = x + legSize[0] * 2;
            }
            x = 0;
            y = legSize[i] * 1.5f;
        }
    }
}
