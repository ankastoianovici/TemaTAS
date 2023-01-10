using System;
using System.Collections.Generic;
using System.Text;

namespace Banca
{
    public class Account
    {
        private float balance;
        private float minBalance = 10;
        public Account()
        {
            balance = 0;
        }
        public Account(int value)
        {
            balance = value;
        }
        public void Deposit(float amount)
        {
            balance += amount;
        }
        public void Withdraw(float amount)
        {
            balance -= amount;
        }
        public void TransferFunds(Account destination, float amount)
        {
            destination.Deposit(amount);
            Withdraw(amount);
        }

        public Account TransferMinFunds(Account destination, float amount)
        {
            if (Balance - amount > MinBalance)
            {
                destination.Deposit(amount);
                Withdraw(amount);
            }
            else throw new NotEnoughFundsException();
            return destination;
        }
        public float Balance
        {
            get { return balance; }
        }
        public float MinBalance
        {
            get { return minBalance; }
        }

        public void TransferFundsFromEurAmount_version1(Account destination, float amountInEuro, float rate)
        {
            float amountInRon = amountInEuro*rate;

            destination.Deposit(amountInRon);
            Withdraw(amountInRon);
        }
        public void TransferFundsFromEurAmount_version2(Account destination, float amountInEuro, ICurrencyConvertor convertor)
        {
            float amountInRon = convertor.EuroToRon(amountInEuro);

            destination.Deposit(amountInRon);
            Withdraw(amountInRon);
        }
        public void TransferFundsEuro(Account destination, float amountInPound, ICurrencyConvertor convertor)
        {
           float amountInRon = convertor.EuroToRon(amountInPound);

           destination.Deposit(amountInRon);
           Withdraw(amountInRon);
        }
        public void TransferFundsEuro2(Account destination, float amountInPound, ICurrencyConvertor convertor)
        {
            float amountInRon = convertor.EuroToRon(amountInPound);

            destination.Deposit(amountInRon);
            Withdraw(amountInRon);
        }
        public void TransferFundsFromEurAmount_version3(Account destination, float amountInEuro, ICurrencyConvertor convertor)
        {
            float amountInRon = convertor.EuroToRon(amountInEuro);

            destination.Deposit(amountInRon);
            Withdraw(amountInRon);
        }
        public void TransferFundsHRK(Account destination, float amountInHRK, ICurrencyConvertor convertor)
        {
            float amountInRon = convertor.HRKToRon(amountInHRK);

            destination.Deposit(amountInRon);
            Withdraw(amountInRon);
        }

    }
        public class NotEnoughFundsException : ApplicationException
        {

        }
 }

