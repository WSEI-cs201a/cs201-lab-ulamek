﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UlamekAsClassLib;

namespace UlamekAsClassUnitTest
{
    /// <summary>
    /// Klasa testująca poprawność implementacji ułamka
    /// </summary>
    [TestClass]
    public class UnitTestConstruction
    {

        #region Test konstruktorów

        /// <summary>
        /// Test tworzenia ułamka za pomocą konstruktora dwuargumentowego 
        /// z poprawnymi (sensownymi) danymi, nie wymagającego upraszczania
        /// </summary>
        [DataTestMethod]
        [DataRow(1, 3, 1, 3)]
        [DataRow(3, 1, 3, 1)]
        [DataRow(0, 2, 0, 1)]
        public void Konstruktor_Dwuargumentowy_PoprawneDaneBezUpraszczania_OK(long licznik, long mianownik, long expextedLicznik, long expectedMianownik)
        {
            // arrange - realizowane jako DataRow

            // act
            Ulamek u = new Ulamek(licznik, mianownik);

            // assert
            Assert.AreEqual(expextedLicznik, u.Licznik);
            Assert.AreEqual(expectedMianownik, u.Mianownik);
        }


        /// <summary>
        /// Test tworzenia ułamka za pomocą konstruktora dwuargumentowego 
        /// z poprawnymi (sensownymi) danymi, wymagającego upraszczania
        /// </summary>
        [DataTestMethod]
        [DataRow(2, 6, 1, 3)]
        [DataRow(4, 2, 2, 1)]
        [DataRow(0, 2, 0, 1)]
        public void Konstruktor_Dwuargumentowy_PoprawneDaneZUpraszczaniem_OK(long licznik, long mianownik, long expextedLicznik, long expectedMianownik)
        {
            // arrange
            //realizowane jako DataRow

            // act
            Ulamek u = new Ulamek(licznik, mianownik);

            // assert
            Assert.AreEqual(expextedLicznik, u.Licznik);
            Assert.AreEqual(expectedMianownik, u.Mianownik);
        }

        /// <summary>
        /// Test tworzenia ułamka z zerowym mianownikiem i weryfikacja zgłoszenia wyjątku.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Konstruktor_ZerowyMianownik_WyjatekArgumentOutOfRange()
        {
            // arrange

            // act
            Ulamek u = new Ulamek(1, 0);

            // assert
            // przechwycone przez ExpectedException
        }

        /// <summary>
        /// Test tworzenia ułamka za pomocą konstruktora dwuargumentowego 
        /// z poprawnymi (sensownymi) danymi, wymagającego upraszczania i normalizacji znaku
        /// </summary>
        [DataTestMethod]
        [DataRow(-2, 6, -1, 3)]
        [DataRow(4, -2, -2, 1)]
        [DataRow(0, -2, 0, 1)]
        [DataRow(-1, -2, 1, 2)]
        public void Konstruktor_PoprawneDaneZNormalizacjaZnaku_OK(long licznik, long mianownik, long expextedLicznik, long expectedMianownik)
        {
            // arrange - realizowane jako DataRow

            // act
            Ulamek u = new Ulamek(licznik, mianownik);

            // assert
            Assert.AreEqual(u.Licznik, expextedLicznik);
            Assert.AreEqual(u.Mianownik, expectedMianownik);
        }


        /// <summary>
        /// Weryfikacja poprawności wartości domyslnej ułamka --> 0/1
        /// </summary>
        public void Konstruktor_DomyslnyBezparametrowy_LicznikZERO()
        {
            Ulamek u = new Ulamek();
            Assert.AreEqual(u.Licznik, 0);
            Assert.AreEqual(u.Mianownik, 1);
        }



        [DataTestMethod]
        [DataRow(1, 1, 1)]
        [DataRow(0, 0, 1)]
        [DataRow(5, 5, 1)]
        [DataRow(-3, -3, 1)]
        public void Konstruktor_Jednoargumentowy_OK(long liczba, long expectedLicznik, long expectedMianownik)
        {
            Ulamek u = new Ulamek(liczba);
            Assert.AreEqual(expectedLicznik, u.Licznik);
            Assert.AreEqual(expectedMianownik, u.Mianownik);
        }



        #endregion

        #region Test properties Licznik i Mianownik

        [TestMethod]
        public void Properties_Licznik_UlamekDomyslny_Zero()
        {
            // arrange

            // act
            Ulamek u = new Ulamek();
            // assert
            Assert.AreEqual(0, u.Licznik);
        }

        [TestMethod]
        public void Properties_Mianownik_UlamekDomyslny_Jeden()
        {
            // arrange

            // act
            Ulamek u = new Ulamek();
            // assert
            Assert.AreEqual(1, u.Mianownik);
        }

        [DataTestMethod]
        [DataRow(1, 1, +1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, +1)]
        [DataRow(-1, 1, -1)]
        public void Properties_ZnakUlamkaWLiczniku(long licznik, long mianownik, int znakLicznika)
        {
            //arrange
            //act
            Ulamek u = new Ulamek(licznik, mianownik);
            //assert
            Assert.AreEqual(Math.Sign(u.Licznik), znakLicznika);
        }

        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(-1, 1)]
        public void Properties_MianownikZawszeDodatni(long licznik, long mianownik)
        {
            //arrange
            //act
            Ulamek u = new Ulamek(licznik, mianownik);
            //assert
            Assert.AreEqual(Math.Sign(u.Mianownik), +1);
        }
        #endregion

        #region Test - reprezentacja tekstowa


        [DataTestMethod]
        [DataRow(1, 2, "1/2")]
        [DataRow(3, 2, "3/2")]
        [DataRow(-1, 2, "-1/2")]
        [DataRow(1, -2, "-1/2")]
        [DataRow(-1, -2, "1/2")]
        public void ToString_OK(long licznik, long mianownik, string napis)
        {
            Ulamek u = new Ulamek(licznik, mianownik);
            Assert.AreEqual(napis, u.ToString());
        }

        #endregion




    }
}
