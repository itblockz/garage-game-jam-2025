using UnityEngine;

public enum HexType
{
    Scientist,
    Priest,
    Neologist,
    Conservationist,
    Businessman,
    Artist
}

public enum MoodState
{
    Neutral,
    Happy,
    Angry
}

public class HexData : MonoBehaviour
{
    [SerializeField] private HexType hexType;
    [SerializeField] private HexType[] enemiesHexTypes;
    [SerializeField] private MoodState moodState = MoodState.Neutral;

    public HexType HexType
    {
        get => hexType;
        set => hexType = value;
    }

    public HexType[] EnemiesHexTypes
    {
        get => enemiesHexTypes;
        set => enemiesHexTypes = value;
    }

    public MoodState MoodState
    {
        get => moodState;
        set => moodState = value;
    }
}
