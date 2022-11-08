using TowerDefence;
using UnityEngine;

public class AchievementsController : Controller
{
    private string address = "achievements";
    public Achievements Achievements = new Achievements();
    public override void Init(Meta meta)
    {
        MetaEvents.OnServerJsonResponse += OnServerJsonResponse;
        MetaEvents.OnServerJsonRequest?.Invoke("achievements");
    }

    public void OnServerJsonResponse(string address, string jsontext)
    {
        if (this.address == address)
        {
            Achievements = JsonUtility.FromJson<Achievements>(jsontext);
        }
    }
}
