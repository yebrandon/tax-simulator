using System;
namespace TaxSim
{
    public class Account
    {
        protected float _balance;
        protected float _interestRate;
        protected float _annualChange;

        public Account()
        {
            _balance = 0;
            _interestRate = 0;
            _annualChange = 0;
        }

        public float GetBalance()
        {
            return _balance;
        }

        public virtual void Deposit(float amount)
        {
            _balance += amount;
            _annualChange += amount;
        }

        public void Withdraw(float amount)
        {
            _balance -= amount;
        }

        public static void Transfer(Account from, Account to, float amount)
        {
            from.Withdraw(amount);
            to.Deposit(amount);
        }

        public float GetAnnualChange()
        {
            return _annualChange;
        }

        public virtual void Tick()
        {
            _balance = _balance * (1 + _interestRate);
            _annualChange = 0;
        }
    }
}
