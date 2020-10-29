using System;
using System.Collections;
using System.Collections.Generic;
using TaxSim;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class RecapControl : MonoBehaviour
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
        summary.text = "LIFETIME VALUES:" + "\n" + "\n" +
            "Total Assets:" + Math.Round(wallet.GetBalance(),2) + "\n" +
            "EI Paid:" + Math.Round(wallet.GetAccountBalance("ei"), 2) + "\n" +
            "CPP Paid:" + Math.Round(wallet.GetAccountBalance("cpp"), 2) + "\n" +
            "Taxes Paid:" + Math.Round(wallet.GetAccountBalance("income"), 2) + "\n" +
            "Cash:" + Math.Round(wallet.GetAccountBalance("cash"), 2) + "\n" +
            "TFSA:" + Math.Round(wallet.GetAccountBalance("tfsa"), 2) + "\n" +
            "RRSP:" + Math.Round(wallet.GetAccountBalance("rrsp"), 2) + "\n";
    }

}
