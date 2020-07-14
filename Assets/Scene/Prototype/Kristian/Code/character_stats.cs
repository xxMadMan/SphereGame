using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.IO;

public class character_stats : MonoBehaviour
{
    public Object jsonFile;

    public string name;
    public int level;
    public int hp;
    
    void Start()
    {
        Test StatsTest = JsonUtility.FromJson<Test>(jsonFile.ToString());
        
        name = StatsTest.name;
        level = StatsTest.level;
        hp = StatsTest.hp;
    }
}

[System.Serializable]
public class Test
{
    public string name;
    public int level;
    public int hp;
}
