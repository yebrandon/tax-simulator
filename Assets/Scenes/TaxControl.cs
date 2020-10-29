using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine.SceneManagement;
using TaxSim;

public class TaxControl : MonoBehaviour
{
    public Text RRSPValue;
    public Slider RRSPSlider;
    public Text TFSAValue;
    public Slider TFSASlider;
    public Wallet wallet;

    void Start()
    {
        GameObject econ = GameObject.Find("Balance");
        economy economy = econ.GetComponent<economy>();
        wallet = economy.wallet;

        RRSPValue.text = (wallet.rrspContribution*100).ToString();
        TFSAValue.text = (wallet.tfsaContribution*100).ToString();

        RRSPSlider.value = (wallet.rrspContribution*100);
        TFSASlider.value = (wallet.tfsaContribution*100);
    }

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting()
    {
        //Displays the value of the slider in the console.
        Debug.Log(RRSPSlider.value/100);
        Debug.Log(TFSASlider.value/100);

        wallet.SetRRSPContribution(RRSPSlider.value/100);
        wallet.SetTFSAContribution(TFSASlider.value/100);

        SceneManager.LoadScene("black");

    }

    void Update()
    {

        RRSPValue.text = RRSPSlider.value.ToString("0") + "%";
        TFSAValue.text = TFSASlider.value.ToString("0") + "%";

    }
}
