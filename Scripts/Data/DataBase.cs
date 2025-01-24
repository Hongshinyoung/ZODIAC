using System.Collections.Generic;

public abstract class DataBase<T>
{ 
    public abstract void SetData(T metaData);
}

public abstract class DataBaseList<T1, T2, T3>
{
    public Dictionary<T1, T2> datas = new Dictionary<T1, T2>();

    public abstract void SetData(List<T3> metaDataList);

    public T2 LoadData(T1 key)
    {
        if (!datas.ContainsKey(key)) return default; // 널과 같움
        return datas[key];
    }   
}