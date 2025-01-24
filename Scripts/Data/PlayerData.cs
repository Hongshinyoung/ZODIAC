using System.Collections.Generic;

[System.Serializable]

public class PlayerData : DataBase<PlayerData>
{
    public string id;
    public int hp;
    public int maxhp;
    public int mp;
    public int attack;
    public float attackspeed;
    public float attackmaxdistance;
    public int defense;
    public int walkspeed;
    public float rotationdamping;
    public float movementspeedmodifier;
    public float runspeedmodifier;
    public int jumpforce;

    public override void SetData(PlayerData metaData)
    {
        this.id = metaData.id;
        this.maxhp = metaData.maxhp;
        this.hp = metaData.hp;
        this.mp = metaData.mp;
        this.attack = metaData.attack;
        this.attackspeed = metaData.attackspeed;
        this.attackmaxdistance = metaData.attackmaxdistance;
        this.defense = metaData.defense;
        this.walkspeed = metaData.walkspeed;
        this.rotationdamping = metaData.rotationdamping;
        this.movementspeedmodifier = metaData.movementspeedmodifier;
        this.runspeedmodifier = metaData.runspeedmodifier;
        this.jumpforce = metaData.jumpforce;
    }
}

[System.Serializable]
public class PlayerDataList : DataBaseList<string, PlayerData, PlayerData>
{
    public List<PlayerData> Player;

    public override void SetData(List<PlayerData> metaPlayerData)
    {
        datas = new Dictionary<string, PlayerData>(metaPlayerData.Count);

        metaPlayerData.ForEach(obj =>
        {
            PlayerData player = new PlayerData();
            player.SetData(obj);
            datas.Add(player.id, player);
        });
    }
}