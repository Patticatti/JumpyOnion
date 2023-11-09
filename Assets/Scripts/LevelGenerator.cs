using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    #region Singleton
    public static LevelGenerator instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public int levels; //set this to amoutn of levels of clouds
    public int currentLevel = 1;
    public float cloudSpacing = 3.5f;
    public List<Cloud> levelClouds = new List<Cloud>(); //level clouds container
    public List<List<Cloud>> clouds = new List<List<Cloud>>(); //all clouds
    [SerializeField] private GameObject smallCloud;
    [SerializeField] private GameObject largeCloud; 
    private Cloud cloud; //pointing to cloud
    private const float largeMult = 6.5f;
    private const float smallMult = 4.0f;
    private List<GameObject> cloudsList = new List<GameObject>(); //cloud objects

    private List<int> availSmallPos = new List<int>(); //5 per level
    private List<int> availLargePos = new List<int>(); //3 per level

    private void Start()
    {
        for (int x = 0; x<5; x++)
            availSmallPos.Add(x+1);
        for (int x = 0; x<3; x++)
            availLargePos.Add(x+1);
        GeneratePremadeClouds();
    }

    private void GeneratePremadeClouds()
    {
        int pos;
        bool extra = false;
        for (int x = 0; x<5; x++)//generate 5 levels of 2 large clouds in random spots
        {
            for (int y = 0; y < 2; y++)
            {
                pos = ChooseRandFromList(availLargePos);
                GenerateLargeCloud(pos, extra);
                Debug.Log("Cloud number: " + (y+1) + " level: " + x + " position: " + pos);
            }
            clouds.Add(levelClouds);
            levelClouds.Clear();
            ResetLargePos();
            currentLevel++;
        }
        for (int x = 0; x<7; x++) //generate 7 levels of either big cloud in midd or 2 on each side
        {
            ResetLargePos();
            if (Random.Range(0,2) == 0) // 1 in midd
            {
                GenerateLargeCloud(2);
            }
            else
            {
                GenerateLargeCloud(1);
                GenerateLargeCloud(3);
            }
            currentLevel++;
        }
        levels = currentLevel;
        SystemManager.instance.progBar.SetTotalHeight(levels * cloudSpacing);
    }

    private void GenerateCloudRow(Cloud mainCloud, float mainChan, float extraChan) 
    //cloud, change for main to be large, chance for extra to be large
    {
        //generate main cloud first then add if needed
        //first determine type of cloud based on main chance
        //Random.Range(mainChan);
        ResetSmallPos(); //reset pos's
        ResetLargePos();
        //if (availSmallPos.All(x => x == 0)) //if no small positions avail
        //if (availLargePos.All(x => x == 0))
    }

    private void InstantiateCloud(Cloud cloud)
    {
        if (cloud.cloudType == 0)
        {
            cloudsList.Add(Instantiate(largeCloud, new Vector3(cloud.posx, cloud.posy, 0f), Quaternion.identity));
        }
        else
        {
            cloudsList.Add(Instantiate(smallCloud, new Vector3(cloud.posx, cloud.posy, 0f), Quaternion.identity));
        }
    }

    private void GenerateLargeCloud(int posx, bool extra = false) //posx is 1-3
    {
        cloud = null;
        cloud = (Cloud)ScriptableObject.CreateInstance(typeof(Cloud));
        cloud.posx = LargePosX(posx);
        cloud.level = currentLevel;
        cloud.posy = PosY(currentLevel, extra);
        cloud.cloudType = 0;
        levelClouds.Add(cloud);
        InstantiateCloud(cloud);
        //instantiate large cloud prefab at posx
    }

    private void GenerateSmallCloud(int posx, bool extra = false)
    {
        cloud = null;
        cloud = (Cloud)ScriptableObject.CreateInstance(typeof(Cloud));
        cloud.posx = SmallPosX(posx);
        cloud.level = currentLevel;
        cloud.posy = PosY(currentLevel, extra);
        cloud.cloudType = 1;
        levelClouds.Add(cloud);
        InstantiateCloud(cloud);
    }

    private float LargePosX(int pos) //convert 1-3 to -6.5f, 0f, 6.5f
    {
        return (pos - 2) * largeMult;
    }

    private float PosY(int lvl, bool extra) 
    {
        float mod = 0f;
        if (extra) //-1.5f or 1.5f
        {
            mod = 1.5f - (2 * (Random.Range(0,2) * 1.5f));//return 1.5 or -1.5f
        }
        return (lvl * cloudSpacing) + mod;
    }

    private float SmallPosX(int pos) //convert 1-3 to -6.5f, 0f, 6.5f
    {
        return (pos - 3) * smallMult;
    }


    private int ChooseRandFromList(List<int> list) //chose random int from list (1-3/5)
    {
        var num = Random.Range(0,list.Count);
        if ((list[num] == 0)&&(list.All(x => x != 0))) //if val is 0 redo (list.All(x => x != 0)
        {    
            num = ChooseRandFromList(list);
        }
        int val = list[num];
        list[num] = 0;
        return val;
    }

    private void ResetSmallPos() //generates list of 1-5, dont touch
    {
        for (int x = 1; x<6; x++) // 0 is empty
        {
            availSmallPos[x-1]=x;
        }
    }

    private void ResetLargePos() //generates list of 1-3, dont touch
    {
        for (int x = 1; x<4; x++) // 0 is empty
        {
            availLargePos[x-1]=x;
        }
    }

    private void ClearData()
    {
        DeleteClouds();
        foreach (var cloudList in clouds)
        {
            for (int i = 0; i < cloudList.Count; i++)
            {
                Destroy(cloudList[i]);
                cloudList[i] = null;
            }
            cloudList.Clear();
        }
        clouds.Clear();
        availLargePos.Clear();
        availSmallPos.Clear();
    }

    private void DeleteClouds()
    {
        for (int i = 0; i < cloudsList.Count; i++)
        {
            Destroy(cloudsList[i]);
            cloudsList[i] = null;
        }
    }

    private void OnApplicationQuit()
    {
        ClearData();
    }
}
