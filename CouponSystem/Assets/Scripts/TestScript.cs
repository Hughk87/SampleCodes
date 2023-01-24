using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TestScript : MonoBehaviour
{
    CouponManager r_ref_couponmgr;
    
    void Awake()
    {
        //init
        r_ref_couponmgr = new CouponManager(CSVReader.Load("coupon"));
        
    }
}
