using UnityEngine;
using System.Collections;
using System.IO;


public class Plasma : MonoBehaviour {

    Color[] colour;
    int width = 1024;
    int length = 1024;
    float size;
    float grey;
    float cor1, cor2, cor3, cor4;
    float r, g, b;
    Texture2D texture, colormap;
    float[,] heights;
    GameObject terrain;
    TerrainData tData;
    public bool orbit = false;

	// Use this for initialization
	void Start () 
    {
        size = (float)width + length;

        texture = new Texture2D(width, length);
        renderer.material.mainTexture = texture;
		colormap = new Texture2D (width, length);
		renderer.material.mainTexture = colormap;

        //camera = GameObject.FindGameObjectWithTag("MainCamera");
        //camera.transform.position = new Vector3(525, 1450, -250);
        //camera.transform.LookAt(new Vector3(512, 0, 512), Vector3.up);

        colour = new Color[width * length];

        //sets a random height between 0 and 1 for the 4 corners of the grid
        cor1 = Random.value;
        cor2 = Random.value;
        cor3 = Random.value;
        cor4 = Random.value;
        heights = new float[width, length];

        drawPlasma(width, length);
        texture.SetPixels(colour);
        texture.Apply();
        
        loadHeightMap();
	}
	
	// Update is called once per frame
	void Update () 
    {
       if(Input.GetKeyDown("n"))
       {
           GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
           for (int i = 0; i < objs.Length; i++ )
           {
               Destroy(objs[i]);
           }
            cor1 = Random.value;
            cor2 = Random.value;
            cor3 = Random.value;
            cor4 = Random.value;
            drawPlasma(width, length);
            texture.SetPixels(colour);
            texture.Apply();
            loadHeightMap();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    if (orbit == true)
        //    {
        //        orbit = false;
        //    }

        //    else
        //    {
        //        orbit = true;
        //    }
        //}

        //if (orbit)
        //{
        //    camera.transform.RotateAround(new Vector3(512, 0, 512), Vector3.up, Time.deltaTime * 15);
        //}
	}

    float displace(float num)
    {
        float max = num / size * 8;
        return Random.Range(-0.5f, 0.5f) * max;
    }

    //get a r, g, b value for each point on the grid
    Color getColour(float c)
    {
        r = 0;
        g = 0;
        b = 0;

        if(c < 0.5f)
        {
            r = c * 2;
        }
        
        else
        {
            r = (1.0f - c) * 2;
        }

        if(c>=0.3f && c < 0.8f)
        {
            g = (c - 0.3f) * 2;
        }

        else if(c < 0.3f)
        {
            g = (0.3f - c) * 2;
        }

        else
        {
            g = (1.3f - c) * 2;
        }

        if(c>=0.5f)
        {
            b = (c - 0.5f) * 2;
        }

        else
        {
            b = (0.5f - c) * 2;
        }

        //grey = (0.299f * r) + (0.587f * g) + (0.114f * b);

        return new Color(r,g,b);
    }

    //divide the grid down
    void divideGrid(float x, float y, float w, float l, float c1, float c2, float c3, float c4)
    {
        float newWidth = w * 0.5f;
        float newLength = l * 0.5f;

        //get the average of the grid piece and draw as asingle pixel
        if(w < 1.0f || l < 1.0f)
        {
            float c = (c1 + c2 + c3 + c4) * 0.25f;
            colour[(int)x + (int)y * width] = getColour(c);
        }

        else
        {
            float middle = (c1 + c2 + c3 + c4) * 0.25f + displace(newWidth + newLength); //mid point displacement
            float edge1 = (c1 + c2) * 0.5f; //top edge average
            float edge2 = (c2 + c3) * 0.5f; //right edge average
            float edge3 = (c3 + c4) * 0.5f; // bottom edge average
            float edge4 = (c4 + c1) * 0.5f; //left edge average

            //check to see if the mid point is between 0 and 1
            if(middle <= 0)
            {
                middle = 0;
            }

            else if(middle > 1.0f)
            {
                middle = 1.0f;
            }

            //redo the operation for each of the new grids
            divideGrid(x, y, newWidth, newLength, c1, edge1, middle, edge4);
            divideGrid(x + newWidth, y, newWidth, newLength, edge1, c2, edge2, middle);
            divideGrid(x + newWidth, y + newLength, newWidth, newLength, middle, edge2, c3, edge3);
            divideGrid(x, y + newLength, newWidth, newLength, edge4, middle, edge3, c4);
        }
    }
    
    void drawPlasma(float w, float l)
    {
        //call operation to calculate averages and drawing
        divideGrid(0.0f, 0.0f, w, l, cor1, cor2, cor3, cor4);
    }

    //For terrain
    void loadHeightMap()
    {
        Color[] values = texture.GetPixels();
        int index = 0;
        for (int z = 0; z < texture.height; z++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                heights[z, x] = values[index].grayscale;
                index++;
            }
        }

		float size = 0;
		for (int i = 0; i < width; i++)
        {
			for (int j = 0; j < width; j++)
            {
		  		size = heights[i,j];

                if (size <= 1.0f && size > 0.75f)
                {
                    //colormap.SetPixel(i,j, new Color (0,0,0));
                    colormap.SetPixel(i, j, new Color(1.0f,1.0f,1.0f));
                }

			    else if (size <= 0.75f && size >0.66f)
                {
					colormap.SetPixel(i,j, new Color (0.4f, 0.26f, 0.13f));
					//colormap.SetPixel(i,j, new Color(255/255.0f,255/255.0f,255/255.0f));
			    }
				
                else if (size <= 0.66f && size> 0.56f)
                {
					colormap.SetPixel(i,j, new Color(0.0f, 0.32f, 0.0f));
					//colormap.SetPixel(i,j, new Color(255/255.0f,255/255.0f,255/255.0f));
				}

			    else if (size <=0.56f && size > 0.0f)
                {
					colormap.SetPixel(i,j, new Color(0.0f, 0.5f, 0.0f));
					//colormap.SetPixel(i,j, new Color(255/255.0f,255/255.0f,255/255.0f));
				}
		    }
		}
		//byte[] img = colormap.EncodeToPNG ();
		//File.WriteAllBytes (Application.dataPath + "/map" + ".png", img);
		colormap.Apply ();
        applyTerrain();
    }

    void applyTerrain()
    {
        tData = new TerrainData();
        tData.heightmapResolution = width;
        tData.size = new Vector3(width*4, 255, width*4);
        tData.SetHeights(0, 0, heights);
        SplatPrototype[] terrainTexture = new SplatPrototype[1];
        terrainTexture[0] = new SplatPrototype();
        terrainTexture[0].texture = colormap;
        terrainTexture[0].tileSize = new Vector2(width*4,length*4);
        tData.splatPrototypes = terrainTexture;
        terrain = Terrain.CreateTerrainGameObject(tData);
		terrain.transform.position = new Vector3 (-width*2, -128, -width*2);
        terrain.name = "Terrain";
        terrain.tag = "Player";
    }
}