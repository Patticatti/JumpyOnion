using UnityEngine;

[CreateAssetMenu(fileName = "New Cloud", menuName = "Cloud")]
public class Cloud : ScriptableObject
{
    public int posx; 
    public int poxy;
    public int cloudType; //0 for small (1/5), 1 for big (1/3)
}