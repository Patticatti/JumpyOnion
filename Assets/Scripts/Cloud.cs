using UnityEngine;

[CreateAssetMenu(fileName = "New Cloud", menuName = "Cloud")]
public class Cloud : ScriptableObject
{
    public int level; //level of jump
    public int cloudType; //0 for large (1/3), 1 for small (1/5)

    public float posx; //1-5 for small, 1-3 for big
    public float posy; //1-levelsMax
}