using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public List<Cloud> levelClouds = new List<Cloud>(); //level clouds container
    public List<List<Cloud>> clouds = new List<List<Cloud>>(); //all clouds
    [SerializeField] private GameObject smallCloud;
    [SerializeField] private GameObject largeCloud; 
    private Cloud cloud; //pointing to cloud
    private const float largeMult = 6.5f;
    private const float smallMult = 4.0f;
    private int currentLevel = 1;

    private List<int> availSmallPos = new List<int>(); //5 per level
    private List<int> availLargePos = new List<int>(); //3 per level

    private void Start(){
        for (int x = 1; x<6; x++)
            availSmallPos.Add(x);
        for (int x = 1; x<4; x++)
            availLargePos.Add(x);
    }

    private void GeneratePremadeClouds()
    {
        int pos;
        bool extra = true;
        for (int x = 0; x<5; x++)//generate 5 levels of 2 large clouds in random spots
        {
            ResetLargePos();
            for (int x = 0; x < 2; x++)
            {
                if (!availLargePos.All(x => x == 0))
                {
                    pos = ChooseRandFromList(availLargePos);
                    GenerateLargeCloud(pos, extra);
                    extra = false;
                }
            }
            currentLevel++;
        }
    }

    private void GenerateCloudRow(Cloud mainCloud, float mainChan, float extraChan) 
    //cloud, change for main to be large, chance for extra to be large
    {
        //generate main cloud first then add if needed
        //first determine type of cloud based on main chance
        Random.Range(mainChan);

        ResetSmallPos(); //reset pos's
        ResetLargePos();
        //if (availSmallPos.All(x => x == 0)) //if no small positions avail
        //if (availLargePos.All(x => x == 0))
    }

    private void GenerateLargeCloud(int posx, bool extra)
    {
        float mult = 1f;
        if (extra) //-1.5f or 1.5f
        {
            mult = 1.5f - (2 * (Random.Range(0,2) * 1.5f));//return 0 or 3f
        }
        cloud = null;
        cloud = (Cloud)ScriptableObject.CreateInstance(typeof(Cloud));
        cloud.posx = LargePosX(posx);
        cloud.level = currentLevel;
        cloud.posy = LargePosY(currentLevel, mult);
        cloud.cloudType = 0;
        clouds.Add(cloud);
        //instantiate large cloud prefab at posx
    }

    private void InstantiateCloud(Cloud cloud)
    {
        if (cloud.cloudType == 0)
        {
            Instantiate(largeCloud, new Vector3(cloud.posx, cloud.posy, 0f), Quaternion.identity);
        }
        else
        {
            Instantiate(smallCloud, new Vector3(cloud.posx, cloud.posy, 0f), Quaternion.identity);
        }
    }

    private float GenerateSmallCloud(int minPos, int maxPos)
    {
        return Random.Range(minPos, maxPos) * largeMult;
    }

    private float LargePosX(int pos) //convert 1-3 to -6.5f, 0f, 6.5f
    {
        return (pos - 2) * largeMult;
    }

    private float LargePosY(int lvl, float mult) //convert 1-3 to -6.5f, 0f, 6.5f
    {
        return lvl * mult;
    }

    private float SmallPosX(int pos) //convert 1-3 to -6.5f, 0f, 6.5f
    {
        return (pos - 3) * smallMult;
    }

    private int ChooseRandFromList(List<int> list) //chose random int from list (1-3/5)
    {
        var num = Random.Range(0,list.Count);
        if (list[num] == 0){ //if val is 0 redo
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
        foreach (var cloudList in clouds)
        {
            foreach (var cloud in cloudList)
            {
                Destroy(cloud);
            }
            cloudList.Clear();
        }
        clouds.Clear();
    }
}
