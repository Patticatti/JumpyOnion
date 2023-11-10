using UnityEngine;

[CreateAssetMenu(fileName = "New Cloud", menuName = "Cloud")]
public class Cloud : ScriptableObject
{
    public int level; //level of jump
    public bool cloudType; //0 for large (1/3), 1 for small (1/5)

    public int posx; //1-5 for small, 1-3 for big
    public int posy; //1-levelsMax
}