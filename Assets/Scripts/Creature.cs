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

    float headPos;

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
        createBody();
        createHead();
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

            clear();

            //for (int i = 0; i < 7; i++)
            //{
            //    Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //    Creatures.transform.position = new Vector3((Random.Range(-10, 11)), (Random.Range(-10, 11)), 0f);
            //    Creatures.tag = "Player";
            //    myNodes.Add(Creatures);
            //}
            createBody();
            createHead();
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
        //Debug.Log(headNum);
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

    void clear()
    {
        GameObject[] GameObjects = GameObject.FindGameObjectsWithTag("Lower Leg");
        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        GameObjects = GameObject.FindGameObjectsWithTag("Upper Leg");
        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        GameObjects = GameObject.FindGameObjectsWithTag("Lower Arm");
        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        GameObjects = GameObject.FindGameObjectsWithTag("Upper Arm");
        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        GameObjects = GameObject.FindGameObjectsWithTag("Head");
        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        GameObjects = GameObject.FindGameObjectsWithTag("Body");
        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }
    }

    //creates creature according to values provided
    void createBody()
    {
        float x = 0, y = 0, z = 0;
        for (int i = 0; i<bodyNum; i++)
        {
            y = y + (bodySize[i] * 0.5f);
            Creatures = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Creatures.transform.position = new Vector3(x, y, z);
            Creatures.transform.localScale = new Vector3(bodySize[i], bodySize[i], bodySize[i]);
            Creatures.tag = Tags.body;
            Creatures.name = "Body";
            myNodes.Add(Creatures);
            y = y + (bodySize[i]*0.5f);
        }
        headPos = y;
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
                    Creatures.name = "Lower Leg";
                }
                else
                {
                    Creatures.tag = Tags.upper_leg;
                    Creatures.name = "Upper Leg";
                }
                myNodes.Add(Creatures);
                z = legSize[0]*-1.25f;
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x, y, z);
                Creatures.transform.localScale = new Vector3(legSize[i] * 0.5f, legSize[i] * 0.5f * 3.0f, legSize[i] * 0.5f);
                if (lower)
                {
                    Creatures.tag = Tags.lower_leg;
                    Creatures.name = "Lower Leg";
                }
                else
                {
                    Creatures.tag = Tags.upper_leg;
                    Creatures.name = "Upper Leg";
                }
                myNodes.Add(Creatures);
                x = x + legSize[0] * 2;
            }
            x = 0;
            y = legSize[i] * 1.5f;
            lower = false;
        }
    }

    void createHead()
    {
        float x = 0, y = 0, z = 0;
        y = headPos + (headSize[0] * 0.5f);
        if (headNum == 2)
            z = headSize[0] * 0.5f;
        else if (headNum == 3)
            z = headSize[0];
        for(int i = 0; i < headNum; i++)
        {
            Creatures = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Creatures.transform.position = new Vector3(x, y, z);
            Creatures.transform.localScale = new Vector3(headSize[0], headSize[0], headSize[0]);
            Creatures.tag = Tags.head;
            Creatures.name = "Head";
            myNodes.Add(Creatures);
            z = z - headSize[0];
        }
    }

    void createArms()
    {

    }
}
