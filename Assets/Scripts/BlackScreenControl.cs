using System;
using System.Collections;
using System.Collections.Generic;
using TaxSim;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class BlackScreenControl : MonoBehaviour
{
    public Text summary;


    //GameObject money = econ.GetComponent<economy>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject econ = GameObject.Find("Balance");
        economy economy = econ.GetComponent<economy>();
        Wallet wallet = economy.wallet;
        Debug.Log(econ);
        summary.text = 
            "Income:" + economy.income + "\n" +
            "EI Paid:" + Math.Round(wallet.GetAnnualChange("ei"), 2) + "\n" +
            "CPP Paid:" + Math.Round(wallet.GetAnnualChange("cpp"), 2) + "\n" +
            "Taxes Paid:" + Math.Round(wallet.GetAnnualChange("income"), 2) + "\n" +
            "Cash:" + Math.Round(wallet.GetAnnualChange("cash"), 2) + "\n" +
            "TFSA:" + Math.Round(wallet.GetAnnualChange("tfsa"), 2) + "\n" +
            "RRSP:" + Math.Round(wallet.GetAnnualChange("rrsp"), 2) + "\n";
    }

}
