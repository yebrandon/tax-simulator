using System;

namespace TaxSim
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Wallet wallet = new Wallet();
            wallet.SetRRSPContribution(0.10F);
            wallet.ReceivePaycheck(300000);
            wallet.PrintAccounts();

            wallet.Tick();
            wallet.PrintAccounts();
        }
    }
}
