using TowerDefence;
using UnityEngine;

public class AchievementsController : Controller
{
    private string address = "achievements";
    public Achievements Achievements = new Achievements();
    public override void Init(Meta meta)
    {
        MetaEvents.OnWebResponse += OnWebResponse;
        MetaEvents.WebGetRequest?.Invoke(address);
        //meta.Web.Get(address);
    }

    public void OnWebResponse(string address, string jsontext)
    {
        if (this.address == address)
        {
            Achievements = JsonUtility.FromJson<Achievements>(jsontext);
        }
    }
}
