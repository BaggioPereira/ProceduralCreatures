using UnityEngine;
using UnityEditor;
using System.Collections;

public class Creature : MonoBehaviour {
    private GameObject Creatures, camera;
    private ArrayList myNodes;

    Light light1, light2, light3;

    int headNum;
    int armsNum;
    int legsNum;
    int bodyNum;
    int headSize;
    //int[] armSize;
    int[] legSize;
    int[] bodySize;

    float height;
    bool orbit = false;

	// Use this for initialization
	void Start () 
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        creatureParts();
        creatureSize();

        myNodes = new ArrayList();

        light1 = new Light();
        light2 = new Light();
        light3 = new Light();

        //light1.transform.position =new Vector3(0, (height*0.5f),0);

        camera.transform.LookAt(new Vector3(0, (height * 0.5f), 0));

        //for (int i = 0; i < 7; i++)
        //{
        //    Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    Creatures.transform.position = new Vector3((Random.Range(-10, 11)), (Random.Range(-10, 11)), 0f);
        //    Creatures.tag = "Player";
        //    myNodes.Add(Creatures);
        //}
        //createBody();
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

            camera.transform.LookAt(new Vector3(0, (height * 0.5f), 0));
            

            clear();

            //for (int i = 0; i < 7; i++)
            //{
            //    Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //    Creatures.transform.position = new Vector3((Random.Range(-10, 11)), (Random.Range(-10, 11)), 0f);
            //    Creatures.tag = "Player";
            //    myNodes.Add(Creatures);
            //}
           // createBody();
            createHead();
        }

        //key input for auto rotate camera around creature
        if(Input.GetKeyDown("r"))
        {
            if(orbit)
            {
                orbit = false;
            }
            else
            {
                orbit = true;
            }
        }

        //if allowed to rotate, rotate speed is time
        if(orbit)
        {
            camera.transform.RotateAround(new Vector3(0, (height * 0.5f), 0), Vector3.up, Time.deltaTime * 25.0f);
        }

        //light1.color = new Color(Random.value,Random.value,Random.value);
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
        //Debug.Log(headNum);
        bodyNum = getRandomNum(2, 6);
        bodySize = new int[bodyNum];
        armsNum = getRandomNum(1, bodyNum);
        //armSize = new int[2];
        legsNum = getRandomNum(1, 6);
        legSize = new int[2];
    }

    //function for getting size for the parts
    void creatureSize()
    {
        for(int i = 0; i < 1; i++)
        {
            headSize = getRandomNum(1, 4);
            //Debug.Log(headSize[i]);
        }

        //for (int i = 0; i < 2; i++)
        //{
        //    armSize[i] = getRandomNum(1, 3);
        //    Debug.Log(headSize[i]);
        //}

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
        creatureHeight();
    }

    //Gets the creatures height from the size of the parts
    void creatureHeight()
    {
        height = 0;
        height += headSize;

        for(int i = 0; i<bodyNum; i++)
        {
            height += bodySize[i];
        }

        height += (legSize[0] + legSize[1]) * 1.5f;
    }

    //clears the last creature before creating a new creature
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

        GameObjects = GameObject.FindGameObjectsWithTag("Hip");
        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }
    }

    //creates creature according to values provided
    void createHead()
    {
        float x = 0, y = 0, z = 0;
        //y = headPos + (headSize * 0.5f);
        y = height;
        if (headNum == 2)
            z = headSize * 0.5f;
        else if (headNum == 3)
            z = headSize;
        for(int i = 0; i < headNum; i++)
        {
            Creatures = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Creatures.transform.position = new Vector3(x, y, z);
            Creatures.transform.localScale = new Vector3(headSize, headSize, headSize);
            Creatures.tag = Tags.head;
            Creatures.name = "Head";
            myNodes.Add(Creatures);
            z = z - headSize;
        }
        height = y - (headSize * 0.5f);
        createBody();
    }

    //creates body and arms
    void createBody()
    {
        float x = 0, y = 0, z = 0;
        int last = 0;
        int armsCreated = 0;
        y = height;
        for (int i = 0; i<bodyNum; i++)
        {
            //y = y + (bodySize[i] * 0.5f);
            y -= (bodySize[i] * 0.5f);
            Creatures = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Creatures.transform.position = new Vector3(x, y, z);
            Creatures.transform.localScale = new Vector3(bodySize[i], bodySize[i], bodySize[i]);
            Creatures.tag = Tags.body;
            Creatures.name = "Body";
            myNodes.Add(Creatures);

            //Arms Creation
            //Adds upper and lower arm for body of each side
            if(armsCreated < armsNum)
            {
                z -= (bodySize[i] * 0.5f) + (bodySize[i] * 0.75f);
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x, y, z);
                Creatures.transform.localScale = new Vector3(bodySize[i] * 0.5f, bodySize[i] * 0.5f, bodySize[i] * 1.5f);
                Creatures.tag = Tags.upper_arm;
                Creatures.name = "Upper Arm";
                myNodes.Add(Creatures);

                z -= (bodySize[i] * 1.25f);
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x, y, z);
                Creatures.transform.localScale = new Vector3(bodySize[i] * 0.25f, bodySize[i] * 0.25f, bodySize[i] * 1.0f);
                Creatures.tag = Tags.lower_arm;
                Creatures.name = "Lower Arm";
                myNodes.Add(Creatures);

                z = 0;

                z += (bodySize[i] * 0.5f) + (bodySize[i] * 0.75f);
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x, y, z);
                Creatures.transform.localScale = new Vector3(bodySize[i] * 0.5f, bodySize[i] * 0.5f, bodySize[i] * 1.5f);
                Creatures.tag = Tags.upper_arm;
                Creatures.name = "Upper Arm";
                myNodes.Add(Creatures);

                z += (bodySize[i] * 1.25f);
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x, y, z);
                Creatures.transform.localScale = new Vector3(bodySize[i] * 0.25f, bodySize[i] * 0.25f, bodySize[i] * 1.0f);
                Creatures.tag = Tags.lower_arm;
                Creatures.name = "Lower Arm";
                myNodes.Add(Creatures);
                armsCreated += 1;

                z = 0;
            }
            y = y - (bodySize[i]*0.5f);
            last+=1;
        }
        //headPos = y;
        last -= 1;
        height = y;
        createLegs();
    }

    //creates legs
    void createLegs()
    {
        float x = 0, y = 0, z = 0;
        bool lower = false;
        y = height;
        for(int i = 0; i < legSize.Length; i++)
        {
            y -= (legSize[i] * 0.75f);
            for (int j = 0; j < legsNum; j++)
            {
                if(!lower)
                {
                    z = 0;
                    y += legSize[0] * 0.5f;
                    Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Creatures.transform.position = new Vector3(x, y, z);
                    Creatures.tag = Tags.hip;
                    Creatures.name = "Hip";
                    y -= legSize[0] * 0.5f;
                }

                z = legSize[0]; 
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x,y,z);
                Creatures.transform.localScale = new Vector3(legSize[i] * 0.5f, legSize[i] * 0.5f * 3.0f, legSize[i] * 0.5f);
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
                z = -legSize[0];
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
                x = x + 2.0f;               
            }
            x = 0;
            y = y - (legSize[i] * 0.75f);
            lower = true;
        }
        //bodyPos = y;
    }
}