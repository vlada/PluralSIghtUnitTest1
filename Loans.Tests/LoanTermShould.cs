using FluentAssertions;
using Loans.Domain.Applications;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace Loans.Tests
{
    public class LoanTermShould
    {
        // [SetUp]
        // public void Setup()
        // {
        // }

        [Test]
        public void ReturnTermInMonths()
        {
            //Arrange
           var sut  =  new LoanTerm(1);
           
           
           //Act
           //Assert.That(sut.ToMonths(),Is.EqualTo(12));
           var numberOfMonth = sut.ToMonths();
           
           //Assert
           // numberOfMonth.Should().Be(12);
           Assert.That(numberOfMonth, Is.EqualTo(12), "months should 12* number of years");
        }


        [Test]
        public void StoreYears()
        {
            //Arrange
            var sut = new LoanTerm(1);
            
            
            //Assert
            Assert.That(sut.Years, Is.EqualTo(1));
            sut.Years.Should().Be(1);
        }

        [Test]
        public void RespectValueEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);
            
            Assert.That(a, Is.EqualTo(b));
        }
        
        [Test]
        public void RespectValueInEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);
            
            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample()
        {
            var a = new LoanTerm(1);
            var b = a;//new LoanTerm(1);
            var c = new LoanTerm(1);
            
            Assert.That(a, Is.SameAs(b));
            Assert.That(a, Is.Not.SameAs(c)); 
        }

        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;
            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);

        }

    }
}