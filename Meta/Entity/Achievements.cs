using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Achievements
{
    public List<Achievement> AchievementsList = new List<Achievement>();

    public Achievement this[int i]
    {
        get { return AchievementsList[i]; }
        private set { AchievementsList[i] = value; }
    }
}

[Serializable]
public class Achievement
{
    [SerializeField]
    public int unique_id;
    [SerializeField]
    public bool is_achieved;
    [SerializeField]
    public string image;
    [SerializeField]
    public string date_from;
    [SerializeField]
    public string date_to;
    [SerializeField]
    public int amount;
    [SerializeField]
    public int items_type;
    [SerializeField]
    public string name;
    [SerializeField]
    public string description;
    [SerializeField]
    public AchievementPrise prise;
    [SerializeField]
    public bool is_prise_received;
}

[Serializable]
public class AchievementPrise
{
    [SerializeField]
    public string currency;
    [SerializeField]
    public int amount;
}