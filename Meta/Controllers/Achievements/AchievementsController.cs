using TowerDefence;
using UnityEngine;

public class AchievementsController : Controller
{
    private Meta _meta;
    private string address;// = "achievements";
    public Achievements Achievements = new Achievements();
    public override void Init(Meta meta)
    {
        _meta = meta;
        address = _meta.data.route.achievements;
        MetaEvents.OnWebResponse += OnWebResponse;
        MetaEvents.WebGetRequest?.Invoke(address);
    }

    public void OnWebResponse(string address, string jsontext)
    {
        if (this.address == address)
        {
            Achievements = JsonUtility.FromJson<Achievements>(jsontext);
        }
    }
}
