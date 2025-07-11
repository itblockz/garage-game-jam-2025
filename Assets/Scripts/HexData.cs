using System.Collections.Generic;
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

[System.Serializable]
public class MoodSpriteEntry
{
    public MoodState moodState;
    public Sprite sprite;
}

public class HexData : MonoBehaviour
{
    [SerializeField] private HexType hexType;
    [SerializeField] private HexType[] enemiesHexTypes;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private MoodState moodState = MoodState.Neutral;
    [SerializeField] private List<MoodSpriteEntry> moodSpriteEntries;
    private Dictionary<MoodState, Sprite> moodSpriteDictionary;

    void Awake()
    {
        InitializeDictionary();
    }
    
    void InitializeDictionary()
    {
        moodSpriteDictionary = new Dictionary<MoodState, Sprite>();
        
        foreach (var entry in moodSpriteEntries)
        {
            if (entry.sprite != null)
            {
                moodSpriteDictionary[entry.moodState] = entry.sprite;
            }
        }
    }

    void UpdateMoodSprite()
    {
        if (moodSpriteDictionary.TryGetValue(moodState, out Sprite sprite))
        {
            spriteRenderer.sprite = sprite;
        }
    }

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
        set
        {
            moodState = value;
            UpdateMoodSprite();
        }
    }
}
