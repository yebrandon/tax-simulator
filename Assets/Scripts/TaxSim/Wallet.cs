using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaxSim
{
    public class Wallet
    {
        private Dictionary<string, Account> accounts;
        private Dictionary<string, Account> taxAccounts;
        public float rrspContribution;
        public float tfsaContribution;

        public Wallet()
        {
            accounts = new Dictionary<string, Account>();
            accounts.Add("cash", new Account());
            accounts.Add("rrsp", new RRSPAccount());
            accounts.Add("tfsa", new TFSAAccount());

            taxAccounts = new Dictionary<string, Account>();
            taxAccounts.Add("ei", new Account());
            taxAccounts.Add("cpp", new Account());
            taxAccounts.Add("income", new Account());

            rrspContribution = 0;
            tfsaContribution = 0;
        }


        public float GetBalance()
        {
            return SumAccountDict(accounts);
        }

        public float GetAccountBalance(string identifier)
        {
            if (accounts.ContainsKey(identifier))
            {
                return accounts[identifier].GetBalance();
            }
            else if (taxAccounts.ContainsKey(identifier))
            {
                return taxAccounts[identifier].GetBalance();
            }
            else
            {
                return 0;
            }
        }

        public float GetAnnualChange(string identifier)
        {
            if (accounts.ContainsKey(identifier))
            {
                return accounts[identifier].GetAnnualChange();
            }
            else if (taxAccounts.ContainsKey(identifier))
            {
                return taxAccounts[identifier].GetAnnualChange();
            }
            else
            {
                return 0;
            }
        }

        public float GetTaxPaid()
        {
            return SumAccountDict(taxAccounts);
        }

        public void SetRRSPContribution(float contribution)
        {
            rrspContribution = Math.Min(contribution, Rates.maxRRSPContribution);
        }

        public void SetTFSAContribution(float contribution)
        {
            tfsaContribution = contribution;
        }

        public void Tick()
        {
            foreach (Account account in accounts.Values)
            {
                account.Tick();
            }

            foreach (Account account in taxAccounts.Values)
            {
                account.Tick();
            }
        }

        public void ReceivePaycheck(float amount)
        {
            Debug.Log("You got paid " + amount);
            float cppAmount = Math.Min(Math.Max(0, amount - Rates.cppExemption)
                * Rates.cppRate, Rates.cppMaximumContribution);
            float eiAmount = Math.Min(amount * Rates.eiRate, Rates.eiMaximumContribution);
            float rrspAmount = Math.Min(amount * rrspContribution, Rates.maxRRSPContribution);
            float tfsaAmount = Math.Min((accounts["tfsa"] as TFSAAccount).GetAvailableContributionRoom(), amount * tfsaContribution);
            float cashAmount = amount - cppAmount - eiAmount - rrspAmount - tfsaAmount;

            Debug.Log((accounts["tfsa"] as TFSAAccount).GetAvailableContributionRoom());
            Debug.Log(amount);
            Debug.Log(tfsaAmount);


            taxAccounts["cpp"].Deposit(cppAmount);
            taxAccounts["ei"].Deposit(eiAmount);

            accounts["rrsp"].Deposit(rrspAmount);
            accounts["tfsa"].Deposit(tfsaAmount);

            float federalTaxableIncome = Math.Max(amount - Rates.baseFederalExemption - rrspAmount, 0);
            float provincialTaxableIncome = Math.Max(amount - Rates.baseProvincialExemption - rrspAmount, 0);
            float incomeTaxAmount = Rates.ComputeProvincialTax(provincialTaxableIncome) + Rates.ComputeFederalTax(federalTaxableIncome);
            cashAmount -= incomeTaxAmount;

            taxAccounts["income"].Deposit(incomeTaxAmount);

            accounts["cash"].Deposit(cashAmount);

            this.PrintAccounts();
        }

        public void PrintAccounts()
        {
            Debug.Log("Accounts: ");
            foreach (KeyValuePair<string, Account> account in accounts)
            {
                Debug.Log(account.Key + ": " + account.Value.GetBalance());
            }

            Debug.Log("Tax Payments: ");
            foreach (KeyValuePair<string, Account> account in taxAccounts)
            {
                Debug.Log(account.Key + ": " + account.Value.GetBalance());
            }
        }

        public Boolean Spend(float amount)
        {
            if (amount <= accounts["cash"].GetBalance())
            {
                accounts["cash"].Withdraw(amount);
                return true;
            }
            else
            {
                return false;
            }
        }

        private float SumAccountDict(Dictionary<string, Account> dict)
        {
            float balance = 0;
            foreach (Account account in accounts.Values)
            {
                balance += account.GetBalance();
            }

            return balance;
        }
    }
}
