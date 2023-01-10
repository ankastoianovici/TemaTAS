using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
namespace Banca
{
    public class Tests
    {
        
        Account source;
        Account destination;
        float euroRate;
        float HRKRate;
        [SetUp]
        public void Setup()
        {
            source = new Account();
            source.Deposit(6782.94F);
            destination = new Account();
            destination.Deposit(1708.53F);
            euroRate = 4.94F;
            HRKRate = 0.65F;
        }
        [Test]
        [Category("fail")]
        public void Test2()
        {
            source.TransferFunds(destination, 268.19F);
            //Assert.Pass();
            NUnit.Framework.Assert.AreEqual(1976.72F, destination.Balance);
            NUnit.Framework.Assert.AreEqual(6554.75F, source.Balance);
        }
        [Test]
        [Category("pass")]
        public void Test1()
        {
            source.TransferFunds(destination, 6773.94F);
            NUnit.Framework.Assert.AreEqual(10.00F, source.MinBalance);
        }

        [Test]
        [Category("pass")]
        public void TestDeposit()
        {
            Account source2 = new Account();
            NUnit.Framework.Assert.AreEqual(0.00F, source2.Balance);
            source2.Deposit(6782.94F);
            NUnit.Framework.Assert.AreEqual(6782.94F, source2.Balance);
        }
        [Test]
        [Category("pass")]
        public void TestWithdraw()
        {
            Account source3 = new Account();
            source3.Deposit(6782.94F);
            source3.Withdraw(12.00F);
            NUnit.Framework.Assert.AreEqual(6770.94F, source3.Balance);
        }
        [Test]
        [Category("pass")]
        [TestCase(3689, 0, 528)]
        [TestCase(1268, 0, 1255)]
        [TestCase(1528, 0, 3)]
        public void TransferMinFunds(int a, int b, int c)
        {

            Account source1 = new Account();
            source1.Deposit(a);
            Account destination1 = new Account();
            destination1.Deposit(b);

            source.TransferMinFunds(destination1, c);
            NUnit.Framework.Assert.AreEqual(c, destination1.Balance);
        }
        [Test]
        [Category("pass")]
        public void TransferMinFunds4()
        {

            Account source4 = new Account(3689);
            source4.Deposit(3689);
            Account destination4 = new Account(0);
            destination4.Deposit(0);

            source.TransferMinFunds(destination4, 528);
            NUnit.Framework.Assert.AreEqual(528, destination4.Balance);
        }
        [Test]
        [ExpectedException(typeof(NotEnoughFundsException))]
        [Category("pass")]
        [TestCase(3689, 0, 528)]
        [TestCase(1268, 0, 1255)]
        [TestCase(1528, 0, 3)]

        public void TransferMinFunds2(int a, int b, int c)
        {
            Account source = new Account();
            source.Deposit(a);
            Account destination = new Account();
            destination.Deposit(b);
            destination = source.TransferMinFunds(destination, c);
            
        }

        [Test]
        [ExpectedException(typeof(NotEnoughFundsException))]
        [Category("pass")]
        [Combinatorial]

        public void TransferMinFunds3([Values(3689, 1268)] int a, [Values(0, 0)] int b, [Values(528, 1255)] int c)
        {
            Account source = new Account();
            source.Deposit(a);
            Account destination = new Account();
            destination.Deposit(b);

            destination = source.TransferMinFunds(destination, c);

        }
        [Test]
        [Category("pass")]
        public void TransferFundsFromEuroAccount()
        {
            //arrange
            Account source = new Account();
            source.Deposit(1520);
            Account destination = new Account();
            destination.Deposit(3652);
            var rate = 4.94F;

            //act
            source.TransferFundsFromEurAmount_version1(destination, 9, rate);

            //assert

            NUnit.Framework.Assert.AreEqual(3696.46F, destination.Balance);
            NUnit.Framework.Assert.AreEqual(1475.54F, source.Balance);
        }
        [Test]
        [Category("pass")]
        public void TransferFundsFromEuroAccount2()
        {
            //arrange
            Account source = new Account();
            source.Deposit(520);
            Account destination = new Account();
            destination.Deposit(2552);
            var convertor = new  CurrencyConvertorStub(euroRate,HRKRate);

            //act
            source.TransferFundsFromEurAmount_version2(destination, 8, convertor);

            //assert

            NUnit.Framework.Assert.AreEqual(2591.52F, destination.Balance);
            NUnit.Framework.Assert.AreEqual(480.48F, source.Balance);
        }
        [Test]
        [Category("pass")]
        [TestCase(56452, 2000, 62)]
        [TestCase(750, 0, 10)]
        public void TestTransferFundsHRK(float source_Balance, float destination_Balance, float transfer)
        {
            Account source4 = new Account();
            Account destination4 = new Account();
            float sourceBalance = source_Balance;
            float destinationBalance = destination_Balance;
            float amountToTransfer = transfer;
            var convertor = new CurrencyConvertorStub(euroRate, HRKRate);
            float expectedSourceBalance = sourceBalance - HRKRate * amountToTransfer;
            float expectedDestinationBalance = destinationBalance + HRKRate * amountToTransfer;
            source4.Deposit(sourceBalance);
            destination4.Deposit(destinationBalance);
            source4.TransferFundsHRK(destination4, amountToTransfer, convertor);
            NUnit.Framework.Assert.AreEqual(expectedSourceBalance, source4.Balance);
            NUnit.Framework.Assert.AreEqual(expectedDestinationBalance, destination4.Balance);
        }
        [Test]
        [Category("pass")]
        [TestCase(7770.94F, 720.53F, 200)]
        [TestCase(750, 100, 100)]
        public void TestTransferFundsEuro2(float source_Balance, float destination_Balance, float transfer)
        {
            Account source3 = new Account();
            Account destination3 = new Account();
            float sourceBalance = source_Balance;
            float destinationBalance = destination_Balance;
            float amountToTransfer = transfer;
            var convertor = new CurrencyConvertorStub(euroRate, HRKRate);
            float expectedSourceBalance = sourceBalance - euroRate * amountToTransfer;
            float expectedDestinationBalance = destinationBalance + euroRate * amountToTransfer;
            source3.Deposit(sourceBalance);
            destination3.Deposit(destinationBalance);
            source3.TransferFundsEuro(destination3, amountToTransfer, convertor);
            NUnit.Framework.Assert.AreEqual(expectedSourceBalance, source3.Balance);
            NUnit.Framework.Assert.AreEqual(expectedDestinationBalance, destination3.Balance);
        }
        public class NotEnoughFundsException : ApplicationException
        {

        }

    }
}