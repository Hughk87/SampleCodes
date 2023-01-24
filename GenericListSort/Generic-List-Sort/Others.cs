using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemInfo
{
    public long item_un;    //idn
    public ITEM item_def;   //아이템 정의값
    public int enchant;     //강화수치
    public int exp;         //경험치
    public bool is_equip;   //장착여부

    public ItemInfo(long _item_un, ITEM _item_def, bool _is_equip = false)
    {
        item_un = _item_un;
        item_def = _item_def;
        enchant = GetRandomVar.Range(0, 3);
        exp = GetRandomVar.Range(0, 1000);
        is_equip = _is_equip;
    }

    public void Print()
    {
        string _msg = is_equip ? "[E]" : string.Empty;
        Debug.Log(_msg + "+" + enchant.ToString() + " " + item_def.ToString() + "  exp: " + exp.ToString() + "/1000");
    }
}
public enum ITEM
{
    NONE = -1,
    MAN_T1_GLOVE_1_E = 0,
    MAN_T1_GLOVE_1_D,
    MAN_T1_GLOVE_1_C,
    MAN_T1_GLOVE_1_B,
    MAN_T1_GLOVE_1_A,
    MAN_T1_GLOVE_1_SS,
    MAN_T1_GLOVE_1_SSS,
}
public class Getvalue
{
    public static int GetGrade_ByItem(ITEM _item)
    {
        int retval;
        switch (_item)
        {
            case ITEM.MAN_T1_GLOVE_1_E: retval = 1; break;
            case ITEM.MAN_T1_GLOVE_1_D: retval = 2; break;
            case ITEM.MAN_T1_GLOVE_1_C: retval = 3; break;
            case ITEM.MAN_T1_GLOVE_1_B: retval = 4; break;
            case ITEM.MAN_T1_GLOVE_1_A: retval = 5; break;
            case ITEM.MAN_T1_GLOVE_1_SS: retval = 6; break;
            case ITEM.MAN_T1_GLOVE_1_SSS: retval = 7; break;
            default: retval = 0; break;
        }

        return retval;
    }
}
public class Debug
{
    public static void Log(string _msg)
    {
        Console.WriteLine(_msg);
    }
}