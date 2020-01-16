﻿using System;
using Loans.Domain.Applications;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Gen = System.Collections.Generic;

namespace Loans.Tests
{
    [TestFixture]
    public class ProductComparerShould
    {
        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            var product = new Gen.List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), product);

            Gen.List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicateComparisons()
        {
            var product = new Gen.List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3),
            };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), product);

            Gen.List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            var product = new Gen.List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3),
            };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), product);

            Gen.List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues()
        {
            var product = new Gen.List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3),
            };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), product);

            Gen.List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));


            // Assert.That(comparisons, Has.Exactly(1).Property("ProductName").EqualTo("a")
            //     .And
            //     .Property("InterestRate").EqualTo(1)
            //     .And
            //     .Property("MonthlyRepayment").GreaterThan(0));
            
            
            Assert.That(comparisons, Has.Exactly(1).Matches<MonthlyRepaymentComparison>(item => 
                                                            item.ProductName == "a" && 
                                                            item.InterestRate == 1 &&
                                                            item.MonthlyRepayment > 0));
        }

        [Test]
        public void NotAllowZeroYears()
        {
          //  Assert.That(()=>new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>().With.Message.EqualTo());
        }
    }
}