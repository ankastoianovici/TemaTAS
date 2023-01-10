using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca
{
    //overrides Deposit & Withdraw methods of the Account class in order to record the method calls. 
    //this is an Internal Spy, not a Dependency Spy, because the SUT call siblings methods of the same class not external classes/methods.
    public class InternalAccountSpy : Account
    {
        List<String> actions = new List<string>();
        ILogger logger;
        public InternalAccountSpy(int value, ILogger Logger) : base(value)
        {
            logger = Logger;
        }
        public new void Deposit(float amount)
        {
            base.Deposit(amount);
            actions.Add("Deposit " + amount);
            logger.Log("Deposit " + amount);
        }

        public new void Withdraw(float amount)
        {
            base.Withdraw(amount);
            actions.Add("Withdraw " + amount);
            logger.Log("Deposit " + amount);
        }
        public new void TransferFunds(InternalAccountSpy destination, float amount)
        {
            base.TransferFunds(destination, amount);
            actions.Add("Transfer funds " + amount);
            logger.Log("Transfer funds " + amount);
        }
        public new void TransferMinFunds(InternalAccountSpy destination, float amount)
        {
            base.TransferMinFunds(destination, amount);
            actions.Add("Transfer minumum funds " + amount);
            logger.Log("Transfer minimum funds " + amount);
        }
        public new void TransferFundsEuro(Account destination, float amountInEuro, ICurrencyConvertor convertor)
        {
            base.TransferFundsEuro(destination, amountInEuro, convertor);
            actions.Add("Transfer " + amountInEuro + " euro");
            logger.Log("Transfer " + amountInEuro + " euro");
        }

        public List<String> GetActions()
        {
            return actions;
        }
    }
}
