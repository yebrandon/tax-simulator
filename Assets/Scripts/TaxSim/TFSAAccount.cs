using System;
namespace TaxSim
{
    public class TFSAAccount : Account
    {
        private float _contributionRoom;
        public TFSAAccount()
        {
            _contributionRoom = 6000;
            _interestRate = 0.10F;
        }

        public override void Tick()
        {
            base.Tick();
            _contributionRoom += 6000;
        }

        public override void Deposit(float amount)
        {
            base.Deposit(amount);
            _contributionRoom -= amount;
        }

        public float GetAvailableContributionRoom()
        {
            return Math.Max(_contributionRoom, 0);
        }
    }
}
