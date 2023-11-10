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
    private List<GameObject> cloudsList = new List<GameObject>(); //cloud objects

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
                GenerateCloud(pos);
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
                GenerateCloud(1); // 1 in midd
            }
            else
            {
                GenerateCloud(0);
                GenerateCloud(2);
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

    private void GenerateCloud(int posx, bool large = true, bool extra = false)
    {
        cloud = null;
        cloud = (Cloud)ScriptableObject.CreateInstance(typeof(Cloud));
        if (large)
        {
            cloud.posx = LargePosX(posx);
        }
        else
        {
            cloud.posx = SmallPosX(posx);
        }
        cloud.level = currentLevel;
        cloud.posy = PosY(currentLevel, extra);
        cloud.cloudType = 0;
        levelClouds.Add(cloud);
        InstantiateCloud(cloud);
    }

    private float LargePosX(int pos) //convert 0-2 to -6.5f, 0f, 6.5f
    {
        return (pos - 1) * largeMult;
    }

    private float PosY(int lvl, bool extra) 
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
