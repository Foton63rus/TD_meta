using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefence;
using UnityEngine;
using EventType = TowerDefence.EventType;

public class StatController : Controller
{
    public List<StatRecord> StatRecords = new List<StatRecord>();
    public override void Init(Meta meta)
    {
        MetaEvents.OnMetaLoaded += OnMetaLoaded;
    }

    public void write(EventType type, string value = null)
    {
        //todo тут можно скидывать счетчик после определенного максимума,
        //с целью уменьшения выделенной памяти ,предварительно записав логи (сервер или файл)
        StatRecord rec = new StatRecord(DateTime.Now, type, value);
        StatRecords.Add(  rec );
        Debug.Log(rec);
    }

    private void OnMetaLoaded()
    {
        write( EventType.MetaLoaded );
    }

    private void OnGameEnemyKill(int arg0)
    {
        write(EventType.GameEnemyKill, arg0.ToString());
    }
}

[Serializable]
public class StatRecord
{
    [SerializeField]
    private System.DateTime _time;
    [SerializeField]
    private TowerDefence.EventType _eventType;
    [SerializeField]
    private string _value;

    public StatRecord(DateTime time, TowerDefence.EventType eventType, string value)
    {
        _time = time;
        _eventType = eventType;
        _value = value;
    }

    public override string ToString()
    {
        return $"log: {_time}>{_eventType}>{_value}";
    }
}
