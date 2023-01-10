using System;
using System.Collections.Generic;

namespace Banca
{
    public class AccountForLogSpy
    {
        List<String> actions = new List<string>();
        ILogger logger;
        private int v;
        private ClientDummy client;
        private ILogger @object;
        private float balance;
        public AccountForLogSpy(int v, ClientDummy client, ILogger @object)
        {
            this.v = v;
            this.client = client;
            this.@object = @object;
        }

        public double Balance { get; internal set; }
        /*public void Withdraw(float amount)
        {
            balance -= amount;
        }
        public void TransferFunds(Account destination, float amount)
        {
            destination.Deposit(amount);
            Withdraw(amount);
        }

        public new void TransferFunds2(AccountForLogSpy destination, float amount)
        {
            base.TransferFunds(destination, amount);
            actions.Add("Transfer funds " + amount);
            logger.Log("Transfer funds " + amount);
        }*/
    }
}