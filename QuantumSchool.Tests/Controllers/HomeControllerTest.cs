﻿#region Licence
/*
 * The MIT License
 *
 * Copyright (c) 2014, Andrew Gray
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantumSchool.Controllers;
using System;
using System.Web.Mvc;

namespace QuantumSchool.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context) {
            //use this database file name (in current bin folder)
            var testDatabase = new TestDatabase("SchoolTest1");
            testDatabase.CreateDatabase();
            //globally inject a connection string with this name
            testDatabase.InitConnectionString("SchoolTestContext");
        }

        //[TestInitialize]
        //public static void SetUp(TestContext context) {
        //    Console.WriteLine("Test Initialised!");
        //    AppDomain.CurrentDomain.SetData("DataDirectory", context.TestDeploymentDir);
        //}

        [TestMethod]
        public void Index() {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About() {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.About() as ViewResult;
            // Assert
            Assert.AreEqual("Quantum School", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact() {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Contact() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
