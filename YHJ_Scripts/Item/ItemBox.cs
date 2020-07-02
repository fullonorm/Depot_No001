using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemBox
{
    /* Key = ItemId, Value = ItemAmount */
    private Dictionary<int, int> box = new Dictionary<int, int>();
    
    // 아이템을 추가한다. 
    public void AddItem(int id, int amount)
    {
        box.Add(id, amount);
    }

    // 수정할 값에 따라 대응하는 메소드로 나중에 수정한다. 
    public void ModifyItem(int id, int dif)
    {
        if(box.ContainsKey(id))
        {
            box[id] = box[id] + dif;
        }
    }
    
    // 아이템을 제거한다.
    public void RemoveItem(int id)
    {
        // 이미 존재가 확실한 아이템만 제거할것 -> 키가 존재하는지 확인을 안해도 된다?
        if (box.ContainsKey(id))
        {
            box.Remove(id);
        }
    }

    // 아이템 id 에 따라서 정렬한다. -> 각 아이템에 따라서 나중에 다시한다.? 
    public void SortItemBox()
    {
        var temp = from pair in box
                   orderby pair.Key ascending
                   select pair;

        Dictionary<int, int> sortedBox = new Dictionary<int, int>();

        foreach (KeyValuePair<int, int> item in temp)
        {
            sortedBox.Add(item.Key, item.Value);
        }

        box.Clear();
        box = sortedBox;
    }

    // box의 아이디에 따른 Amount양 반환
    public int GetItemAmount(int id)
    {
        if (!box.ContainsKey(id))
            return -1;

        return box[id];
    }

    public void displayBox()
    {
        foreach (KeyValuePair<int,int> item in box)
        {
            Debug.Log(item.Key + ", " + item.Value);
        }
    }

    public void SetBox(Dictionary<int, int> newBox)
    {
        box.Clear();
        box = newBox;
    }

    public string GetBoxString()
    {
        string result = null;
        foreach (KeyValuePair<int, int> item in box)
        {
            string str = item.Key + "," + item.Value;
            result += str;
            result += "/";
        }
        result = result.Substring(0, result.Length - 1);
        Debug.Log(result);
        return result;
    }
}
