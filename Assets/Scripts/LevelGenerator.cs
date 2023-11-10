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

    public int levels = 20; //set this to max amoutn of levels of clouds
    public int currentLevel = 0;
    public float cloudSpacing = 3.5f;
    public List<Cloud> levelClouds = new List<Cloud>(); //level clouds container
    public List<List<Cloud>> clouds = new List<List<Cloud>>(); //all clouds
    [SerializeField] private GameObject smallCloud;
    [SerializeField] private GameObject largeCloud;
    [SerializeField] private GameObject coinPrefab;  
    private Cloud cloud; //pointing to cloud

    private const float largeMult = 6.5f;
    private const float smallMult = 4.0f;
    private const float maxCoins = 0.8f; //100*0.8 is 80 jump to get highest
    private const float minCoins = 0.3f; //30 jump to start getting doubles

    private List<GameObject> objectsList = new List<GameObject>(); //all objects
    private List<int> availSmallPos = new List<int>(); //5 per level
    private List<int> availLargePos = new List<int>(); //3 per level

    private void Start()
    {
        GeneratePremadeClouds();
    }

    private void GeneratePremadeClouds()
    {
        int pos;
        bool extra = false;
        for (int x = 0; x<5; x++)//generate 5 levels of 2 large clouds in random spots
        {
            ResetLargePos();
            for (int y = 0; y < 2; y++)
            {
                pos = ChooseRandFromList(availLargePos);
                GenerateCloud(pos, currentLevel);
            }
            clouds.Add(levelClouds);
            levelClouds.Clear();
            currentLevel++;
        }
        for (int x = 0; x<7; x++) //generate 7 levels of either big cloud in midd or 2 on each side
        {
            ResetLargePos();
            if (Random.Range(0,2) == 0)
            {
                GenerateCloud(1, currentLevel); // 1 in midd
            }
            else
            {
                GenerateCloud(0, currentLevel);
                GenerateCloud(2, currentLevel);
            }
            currentLevel++;
        }
        SystemManager.instance.progBar.SetTotalHeight(levels * cloudSpacing);
    }

    private void GenerateCloudRow(float mainChance) 
    //reference cloud, change for main to be large, chance for extra to be large
    {
        //float coinFrequency = currentLevel*
        if (Random.Range(0f,1.0f) < mainChance) //set as large
        {
            //GenerateCloud();
        }
        //generate main cloud first then add if needed
        //first determine type of cloud based on main chance
        //Random.Range(mainChan);
        ResetSmallPos(); //reset pos's
        ResetLargePos();
    }

    private void CalcItemFrequency(int level, float posx, float posy) //per cloud
    {
        float maxFrequency = levels * maxCoins; //80
        float minFrequency = levels * minCoins; //30
        float chance;
        if (level > minFrequency)
        {
            chance = (level - minFrequency)/(maxFrequency - minFrequency);
            if (Random.Range(0f,1f)<chance)
            {
                objectsList.Add(Instantiate(coinPrefab, new Vector3(posx, posy + 1.0f, 0f), Quaternion.identity));
            }
        }
    }

    private void InstantiateCloud(Cloud cloud, int level)
    {
        float newPosX;
        if (!cloud.cloudType)
        {
            newPosX = LargePosX(cloud.posx);
            objectsList.Add(Instantiate(largeCloud, new Vector3(newPosX, PosY(cloud.posy), 0f), Quaternion.identity));
        }
        else
        {
            newPosX = SmallPosX(cloud.posx);
            objectsList.Add(Instantiate(smallCloud, new Vector3(newPosX, PosY(cloud.posy), 0f), Quaternion.identity));
        }
        CalcItemFrequency(level, newPosX, PosY(cloud.posy));
    }

    private void GenerateCloud(int posx, int level, bool large = true, bool extra = false)
    {
        cloud = null;
        cloud = (Cloud)ScriptableObject.CreateInstance(typeof(Cloud));
        cloud.posx = posx;
        //cloud.level = currentLevel;
        cloud.posy = level;
        cloud.cloudType = !large;
        levelClouds.Add(cloud);
        InstantiateCloud(cloud, level);
    }

    private float LargePosX(int pos) //convert 0-2 to -6.5f, 0f, 6.5f
    {
        return (pos - 1) * largeMult;
    }

    private float PosY(int lvl, bool extra = false) 
    {
        float mod = 0f;
        if (extra) //-0.5f or 0.5f
        {
            mod = 0.5f - (2 * (Random.Range(0,2) * 0.5f));//return 0.4 or -0.5f
        }
        return (lvl * cloudSpacing) + mod;
    }

    private float SmallPosX(int pos) //convert 0-2 to -6.5f, 0f, 6.5f
    {
        return (pos - 2) * smallMult;
    }


    private int ChooseRandFromList(List<int> list) //chose random int from list (0-2/4)
    {
        int ind = Random.Range(0,(list.Count));
        int val = list[ind];
        if (list.Count > 0)
            list.RemoveAt(ind);
        return val;
    }

    private void ResetSmallPos() //generates list of 0-4, dont touch
    {
        availSmallPos.Clear();
        for (int x = 0; x<5; x++)
            availSmallPos.Add(x);
    }

    private void ResetLargePos() //generates list of 0-2, dont touch
    {
        availLargePos.Clear();
        for (int x = 0; x<3; x++)
            availLargePos.Add(x);
    }

    private void ClearData()
    {
        DeleteObjects();
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

    private void DeleteObjects()
    {
        for (int i = 0; i < objectsList.Count; i++)
        {
            Destroy(objectsList[i]);
            objectsList[i] = null;
        }
    }

    private void OnApplicationQuit()
    {
        ClearData();
    }
}
