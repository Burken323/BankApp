using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankApp;

namespace BankAppTesting
{
    [TestClass]
    public class UnitTest1
    {
        Bank bank = new Bank();
        Account accountOne = new Account(14000, 1200, 100.00M);
        Account accountTwo = new Account(14001, 1201, 100.00M);

        [TestMethod]
        public void InvalidAmounts()
        {
            // Withdraw or transfer larger amounts than the amounts than what currently exist on the accounts.
            //Credit is at 10000 standard.
            accountOne.Credit = 0;
            decimal testAmountOne = 101.00M;
            bool test = accountOne.Withdraw(testAmountOne);
            decimal result = 100.00M;
            decimal testAmountTwo = 101.00M;
            bank.CheckCreditForTransfer(accountOne, accountTwo, testAmountTwo);

            //Withdraw test:
            Assert.AreEqual(false, test);
            //Transfer test:
            Assert.AreEqual(result == 100.00M, accountOne.Balance == result);
        }
        [TestMethod]
        public void NegativeValues()
        {
            // Withdraw or deposit negative values from accounts.
            decimal testAmountOne = -50;
            bool test = accountOne.Withdraw(testAmountOne);
            decimal testAmountTwo = -50;
            decimal expected = 100.00M;
            accountOne.Deposit(testAmountTwo);

            //Withdraw test:
            Assert.AreEqual(false, test);
            //Deposit test:
            Assert.AreEqual(expected, accountOne.Balance);
        }
        [TestMethod]
        public void CheckCreditAndInterest()
        {
            // Withdraw and transfer works with credit and interest.

            accountOne.Credit = 1000;
            decimal testAmountOne = 101.00M;
            accountOne.Withdraw(testAmountOne);
            bool result = accountOne.DebtInterest != 0;
            decimal testAmountTwo = 101.00M;
            bool expected = accountTwo.DebtInterest != 0 && accountTwo.Balance < 0;
            bank.CheckCreditForTransfer(accountTwo, accountOne, testAmountTwo);
            bool resultTwo = accountTwo.DebtInterest != 0;

            //Withdraw with credit and debtinterest test:
            Assert.AreEqual(true, result);
            //Transfer with credit and debtinterest test:
            Assert.AreEqual(true, accountTwo.DebtInterest != 0 && accountTwo.Balance < 0);
        }
    }
}
