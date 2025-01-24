[System.Serializable]

public class UserData : DataBase<UserData>
{
    public string id;
    public string name;
    public int level = 1;
    public int exp;
    public int nextlevelexp = 100;

    public bool AddExperience(int amount)
    {
        exp += amount;

        if (exp >= nextlevelexp)
        {
            LevelUp();
            return true; 
        }

        return false; 
    }

    private void LevelUp()
    {
        exp -= nextlevelexp;
        level++;
    }

    public override void SetData(UserData metaData)
    {
        this.id = metaData.id;
        this.name = metaData.name;
        this.level = metaData.level;
        this.exp = metaData.exp;
        this.nextlevelexp = metaData.nextlevelexp;
    }
}