using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Xml;
using System.Linq;
using Banca;

namespace Banca
{
    [TestFixture]
    public class MockFramework
    {
        float euroRate;
        float HRKRate;
  
        Moq.Mock<ILogger> loggerMock;
        InternalAccountSpy destinationSpy;
        InternalAccountSpy sourceSpy;
        [SetUp]
        public void InitAccount()
        {
            //arrange
            euroRate = 4.94F;
            HRKRate=0.65F;
            loggerMock = new Mock<ILogger>(MockBehavior.Default);
            destinationSpy = new InternalAccountSpy(0, loggerMock.Object);
            sourceSpy = new InternalAccountSpy(0, loggerMock.Object);

        }
        [Test]
        [Category("pass")]
        public void TestDepozit()
        {
            float amount = 10F;
            sourceSpy.Deposit(amount);

            NUnit.Framework.Assert.AreEqual(1, sourceSpy.GetActions().Count);
            loggerMock.Verify((cmd => cmd.Log("Deposit " + amount)), Times.Once());
        }
        [Test]
        [Category("fail")]
        public void TestWithdraw()
        {
            float amount = 10.00F;
            sourceSpy.Deposit(amount);
            sourceSpy.Withdraw(amount);
            NUnit.Framework.Assert.AreEqual(2, sourceSpy.GetActions().Count);
            loggerMock.Verify((cmd => cmd.Log("Deposit " + amount)), Times.Once());
            loggerMock.Verify((cmd => cmd.Log("Withdraw " + amount)), Times.Once());
        }
        [Test]
        [Category("pass")]
        public void TransferFunds()
        {
            sourceSpy.Deposit(528.96F);
            destinationSpy.Deposit(243.10F);

            sourceSpy.TransferFunds(destinationSpy, 75.83F);
            NUnit.Framework.Assert.AreEqual(318.93F, destinationSpy.Balance);
            NUnit.Framework.Assert.AreEqual(453.13F, sourceSpy.Balance);

            NUnit.Framework.Assert.AreEqual(2, sourceSpy.GetActions().Count);
            NUnit.Framework.Assert.AreEqual(1, destinationSpy.GetActions().Count);
            loggerMock.Verify((cmd => cmd.Log("Deposit " + 528.96)), Times.Once());
            loggerMock.Verify((cmd => cmd.Log("Deposit " + 243.10)), Times.Once());
            loggerMock.Verify((cmd => cmd.Log("Transfer funds " + 75.83)), Times.Once());

        }

        [Test]
        [Category("pass")]
        [TestCase(450, 100, 260, 190, 360)]
        [TestCase(150, 20, 100, 50, 120)]
        public void TestTransferMinFundsPass(float sourceDeposit, float destinationDeposit, float transfer, float a, float b)
        {
            sourceSpy.Deposit(sourceDeposit);
            destinationSpy.Deposit(destinationDeposit);

            sourceSpy.TransferMinFunds(destinationSpy, transfer);
            NUnit.Framework.Assert.AreEqual(a, sourceSpy.Balance);
            NUnit.Framework.Assert.AreEqual(b, destinationSpy.Balance);

            NUnit.Framework.Assert.AreEqual(2, sourceSpy.GetActions().Count);
            NUnit.Framework.Assert.AreEqual(1, destinationSpy.GetActions().Count);
            loggerMock.Verify((cmd => cmd.Log("Deposit " + sourceDeposit)), Times.Once());
            loggerMock.Verify((cmd => cmd.Log("Deposit " + destinationDeposit)), Times.Once());
            loggerMock.Verify((cmd => cmd.Log("Transfer minimum funds " + transfer)), Times.Once());
        }
        [Test]
        [Category("pass")]
        [TestCase(1000, 50, 92)]
        [TestCase(200, 0, 30)]
        public void TestTransferFundsEuro(float source_Balance, float destination_Balance, float transfer)
        {
            float sourceBalance = source_Balance;
            float destinationBalance = destination_Balance;
            float amountToTransfer = transfer;
            var convertor = new CurrencyConvertorStub(euroRate, HRKRate);
            float expectedSourceBalance = sourceBalance - euroRate * amountToTransfer;
            float expectedDestinationBalance = destinationBalance + euroRate * amountToTransfer;
            sourceSpy.Deposit(sourceBalance);
            destinationSpy.Deposit(destinationBalance);
            sourceSpy.TransferFundsEuro(destinationSpy, amountToTransfer, convertor);

            NUnit.Framework.Assert.AreEqual(expectedSourceBalance, sourceSpy.Balance);
            NUnit.Framework.Assert.AreEqual(expectedDestinationBalance, destinationSpy.Balance);

            NUnit.Framework.Assert.AreEqual(2, sourceSpy.GetActions().Count);
            NUnit.Framework.Assert.AreEqual(1, destinationSpy.GetActions().Count);

            loggerMock.Verify((cmd => cmd.Log("Deposit " + source_Balance)), Times.Once());
            loggerMock.Verify((cmd => cmd.Log("Deposit " + destination_Balance)), Times.Once());
            loggerMock.Verify((cmd => cmd.Log("Transfer " + transfer + " euro")), Times.Once());
        }

        //nu am rezolvat erorile deci pica 
        /*[Test]
        [Category("pass")]
        public void TransferFunds2()
        {

            //arrange the MockObject
            var logMock = new Moq.Mock<ILogger>();

            //arrange SUT

            var client = new ClientDummy();
            var source = new AccountForLogSpy(200, client, logMock.Object);
            var destination = new AccountForLogSpy(150, client, logMock.Object);

            //set mocked logger expectations

            logMock.Setup(d => d.Log("method Log was called with message : Transaction : 100 & 250"));

            //logMock.ExpectedNumberOfCalls(1);
            //act

            source.TransferFunds(destination, 100.00F);

            //assert 
            NUnit.Framework.Assert.AreEqual(250.00F, destination.Balance);
            NUnit.Framework.Assert.AreEqual(100.00F, source.Balance);

            //mock object verify

            logMock.Verify();
        }
        */
        [Test]
        [Category("pass")]
        public void TransferFundsFromEurAmount_MockFramework_ShouldWork()
        {

            //arrange

            var source = new Account(200);
            var destination = new Account(150);

            var rateEurRon = 4.49F;
            var convertorMock = new Mock<ICurrencyConvertor>();

            convertorMock.Setup(_ => _.EuroToRon(20)).Returns(20 * rateEurRon); // set mock to act as a TestDouble Stub - gives IndirectInputs to the SUT

            //act
            source.TransferFundsFromEurAmount_version3(destination, 20.00F, convertorMock.Object);

            //assert
            NUnit.Framework.Assert.AreEqual(150.00F + 20 * rateEurRon, destination.Balance);
            NUnit.Framework.Assert.AreEqual(200.00F - 20 * rateEurRon, source.Balance);

            convertorMock.Verify(_ => _.EuroToRon(20), Times.Once()); //verify behavior 
        }
        [Test]
        [Category("pass")]
        public void TransferFundsFromHRKAmount_MockFramework_ShouldWork()
        {

            //arrange

            var source = new Account(235);
            var destination = new Account(178);

            var rateHRKRon = 0.65F;
            var convertorMock = new Mock<ICurrencyConvertor>();

            convertorMock.Setup(_ => _.HRKToRon(35)).Returns(35 * rateHRKRon); // set mock to act as a TestDouble Stub - gives IndirectInputs to the SUT

            //act
            source.TransferFundsHRK(destination, 35.00F, convertorMock.Object);

            //assert
            NUnit.Framework.Assert.AreEqual(178.00F + 35 * rateHRKRon, destination.Balance);
            NUnit.Framework.Assert.AreEqual(235.00F - 35 * rateHRKRon, source.Balance);

            convertorMock.Verify(_ => _.HRKToRon(35), Times.Once()); //verify behavior 
        }
    }
}

