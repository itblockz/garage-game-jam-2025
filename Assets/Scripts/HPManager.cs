using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField] private int intialHP = 100;
    private int hp;

    void Start()
    {
        ResetHP();
    }

    public void DecreaseHP(int amount)
    {
        hp = Mathf.Max(0, hp - amount);
        Debug.Log("HP decreased by " + amount + ". Current HP: " + hp);
    }

    public void ResetHP()
    {
        hp = intialHP;
        Debug.Log("HP reset to " + hp);
    }

    public int HP
    {
        get { return hp; }
    }

    public int InitialHP
    {
        get { return intialHP; }
    }
}