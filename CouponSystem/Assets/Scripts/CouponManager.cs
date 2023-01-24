using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CouponManager
{
    const string STR_MEM_COUPON = "COUPON_";
    const int VALUE_NOTUSE = -1;
    const int VALUE_ALREADYUSE = 1;

    private Dictionary<string, CouponReward> coupons;

    public CouponManager(List<string[]> _list_load)
    {
        //create dictionary
        coupons = new Dictionary<string, CouponReward>();
        for (int i = 0; i < _list_load.Count; i++)
        {
            string[] _arr_data = new string[_list_load[i].Length - 1];
            Array.Copy(_list_load[i], 1, _arr_data, 0, _arr_data.Length);

            coupons.Add(_list_load[i][0], new CouponReward(_arr_data));
        }
    }

    CouponReward GetReward(string _code)
    {
        if (coupons.ContainsKey(_code))
        {
            return coupons[_code];
        }
        else
        {
            return new CouponReward();
        }
    }

    public bool Use(string _code)
    {
        bool _retval = false;
        EV_RESULT_USECOUPON _result = EV_RESULT_USECOUPON.NONE;

        CouponReward _reward = GetReward(_code);

        if (_reward.Type == EV_COUPONTYPE.NONE ||
            _reward.Amount <= 0)
        {
            _result = EV_RESULT_USECOUPON.FAIL_CANNOTFIND;
        }
        else if (IsAlreadyUse(_code))
        {
            _result = EV_RESULT_USECOUPON.FAIL_ALREADYUSE;
        }
        //process use coupon
        else if (OnProcess_CouponUse(_reward))
        {
            //save use coupon
            PlayerPrefs.SetInt(STR_MEM_COUPON + _code, VALUE_ALREADYUSE);
            PlayerPrefs.Save();

            _result = EV_RESULT_USECOUPON.SUCC;
        }
        else
            _result = EV_RESULT_USECOUPON.NONE;

        switch (_result)
        {
            case EV_RESULT_USECOUPON.SUCC:
                _retval = true;
                Debug.Log("쿠폰을 사용하였습니다.\n" + GetString_Reward(_reward) + "을(를) 획득하였습니다");
                break;
            case EV_RESULT_USECOUPON.FAIL_CANNOTFIND:
                Debug.Log("잘못된 쿠폰을 입력하였습니다.", null);
                break;
            case EV_RESULT_USECOUPON.FAIL_ALREADYUSE:
                Debug.Log("이미 사용된 쿠폰입니다.", null);
                break;
            case EV_RESULT_USECOUPON.NONE:
            default:
                break;
        }
        return _retval;
    }
    static string GetString_Reward(CouponReward _reward)
    {
        string _retval = string.Empty;
        switch (_reward.Type)
        {
            case EV_COUPONTYPE.ACTIONPOINT:
                _retval = $"AP {_reward.Amount}개";
                break;
            case EV_COUPONTYPE.MONEY:
                _retval = $"월급 {_reward.Amount}원";
                break;
            case EV_COUPONTYPE.FEELING:
                _retval = $"기분 {_reward.Amount}개";
                break;
        }
        return _retval;
    }
    bool IsAlreadyUse(string _code)
    {
        int _result = PlayerPrefs.GetInt(STR_MEM_COUPON + _code, VALUE_NOTUSE);
        if (_result == VALUE_NOTUSE)
            return false;
        else
            return true;
    }

    bool OnProcess_CouponUse(CouponReward _reward)
    {
        bool _retval = false;

        //EV_PLAYERPREFAB _gettype = EV_PLAYERPREFAB.NONE;
        //switch (_reward.Type)
        //{
        //    case EV_COUPONTYPE.ACTIONPOINT:
        //        _gettype = EV_PLAYERPREFAB.ACTIONPOINT;
        //        break;
        //    case EV_COUPONTYPE.MONEY:
        //        _gettype = EV_PLAYERPREFAB.MONEY;
        //        break;
        //    case EV_COUPONTYPE.FEELING:
        //        _gettype = EV_PLAYERPREFAB.FEELING;
        //        break;
        //}
        //if (_gettype != EV_PLAYERPREFAB.NONE)
        //{
        //    //to do ::
        //    //call function - get user data
        //    GameManager.instance.r_ref_userinfomgr.r_ref_playerprefabmgr.Set_Delta(_gettype, _reward.Amount);
        //    _retval = true;
        //}
        return _retval;
    }
}

public class CouponReward
{
    public EV_COUPONTYPE Type;
    public int Amount;
    public CouponReward()
    {
        Type = EV_COUPONTYPE.NONE;
        Amount = 0;
    }
    public CouponReward(string[] str_lines)
    {
        if (str_lines.Length < 2)
            return;

        Type = (EV_COUPONTYPE)int.Parse(str_lines[0]);
        Amount = int.Parse(str_lines[1]);
    }
}