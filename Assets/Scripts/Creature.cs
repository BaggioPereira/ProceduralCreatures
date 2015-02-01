using UnityEngine;
using UnityEditor;
using System.Collections;

public class Creature : MonoBehaviour {
    private GameObject Creatures, camera;
    private ArrayList myNodes;

    int headNum;
    int armsNum;
    int legsNum;
    int bodyNum;
    int headSize;
    int[] legSize;
    int[] bodySize;
    
    float height;
    bool orbit = false;

	// Use this for initialization
	void Start () 
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        myNodes = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //if allowed to rotate, rotate over time
        if(orbit)
        {
            camera.transform.RotateAround(new Vector3(0, (height * 0.5f), 0), Vector3.up, Time.deltaTime * 25.0f);
        }
	}

    void OnGUI()
    {
        //a GUI button for generating a random creature
        if (GUI.Button(new Rect(25f, 25f, 60f, 25f), "Random"))
        {
            clear();
            creatureParts();
            creatureSize();
            creatureHeight();
            camera.transform.LookAt(new Vector3(0, (height * 0.5f), 0));
            createHead();
        }

        //a GUI button for creating a creature with set variables
        if(GUI.Button(new Rect(25f,50f,60f,25f),"Create"))
        {
            clear();
            creatureSize();
            creatureHeight();
            camera.transform.LookAt(new Vector3(0, (height * 0.5f), 0));
            createHead();
        }

        //a GUI button to toggle the camera rotation
        if (GUI.Button(new Rect(25f, 275f, 120f, 25f), "Rotate Camera"))
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

        GUI.Label(new Rect(25f,75f,120f,25f),"Number of Heads = ");
        GUI.Label(new Rect(150f, 75f, 25f, 25f), headNum.ToString());
        headNum = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(25f, 100f, 60f, 25f), headNum, 1, 3)); //a GUI slider to set the number of heads

        GUI.Label(new Rect(25f, 125f, 120f, 25f), "Number of Bodys = ");
        GUI.Label(new Rect(150f, 125f, 25f, 25f), bodyNum.ToString());
        bodyNum = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(25f, 150f, 60f, 25f), bodyNum, 2, 5)); // a GUI slider to set the number of body parts
        bodySize = new int[bodyNum];

        GUI.Label(new Rect(25f, 175f, 120f, 25f), "Number of Arms = ");
        GUI.Label(new Rect(150f, 175f, 25f, 25f), armsNum.ToString());
        armsNum = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(25f, 200f, 60f, 25f), armsNum, 0, bodyNum)); // a GUI slider to set the number of arms

        GUI.Label(new Rect(25f, 225f, 120f, 25f), "Number of Legs = ");
        GUI.Label(new Rect(150f, 225f, 25f, 25f), legsNum.ToString());
        legsNum = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(25f, 250f, 60f, 25f), legsNum, 1, 5)); //a GUI slider to set the number of legs
        legSize = new int[2];
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
        bodyNum = getRandomNum(2, 6);
        bodySize = new int[bodyNum];
        armsNum = getRandomNum(1, bodyNum);
        legsNum = getRandomNum(1, 6);
        legSize = new int[2];
    }

    //function for getting size for the parts
    void creatureSize()
    {
        headSize = getRandomNum(1, 4);

        for (int i = 0; i < 2; i++)
        {
            legSize[i] = getRandomNum(1, 4);
        }

        for (int i = 0; i < bodyNum; i++)
        {
            bodySize[i] = getRandomNum(1, 4);
        }
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

    //creates body and arm
    void createBody()
    {
        float x = 0, y = 0, z = 0;
        int last = 0;
        int armsCreated = 0;
        y = height;
        for (int i = 0; i<bodyNum; i++)
        {
            y -= (bodySize[i] * 0.5f);
            Creatures = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Creatures.transform.position = new Vector3(x, y, z);
            Creatures.transform.localScale = new Vector3(bodySize[i], bodySize[i], bodySize[i]);
            Creatures.tag = Tags.body;
            if(i == 0)
            {
                Creatures.name = "Main Body";
            }

            else
            {
                Creatures.name = "Body";
            }
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
        last -= 1;
        height = y;
        createLegs();
    }

    //creates legs
    void createLegs()
    {
        float x = 0, y = 0, z = 0;
        int size = 0;
        bool lower = false;
        for (int i = 0; i < legsNum*2; i++)
        {
            if(!lower)
            {
                y = height;
            }

            y -= (legSize[size] * 0.75f);

            if(!lower)
            {
                z = 0;
                y += legSize[0] * 0.5f;
                Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Creatures.transform.position = new Vector3(x, y, z);
                Creatures.transform.localScale = new Vector3(legSize[size] * 0.5f, 1.0f, legSize[size] * 0.5f);
                Creatures.tag = Tags.hip;
                Creatures.name = "Hip";
                myNodes.Add(Creatures);
                y -= legSize[0] * 0.5f;
            }

            z = legSize[0]*0.75f; 
            Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Creatures.transform.position = new Vector3(x,y,z);
            Creatures.transform.localScale = new Vector3(legSize[size] * 0.5f, legSize[size] * 0.5f * 3.0f, legSize[size] * 0.5f);
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
                
            z = -legSize[0]*0.75f;
            Creatures = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Creatures.transform.position = new Vector3(x, y, z);
            Creatures.transform.localScale = new Vector3(legSize[size] * 0.5f, legSize[size] * 0.5f * 3.0f, legSize[size] * 0.5f);
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
            
            if(lower==false)
            {
                
                y = y - (legSize[size] * 0.75f);
                lower = true;
                size++;
            }

            else
            {
                x = x + 2.0f;
                lower = false;
                size--;
            }
        }
    }
}