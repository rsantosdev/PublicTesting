﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WeedSeeker.Web.Test.Functional.Properties;

namespace WeedSeeker.Web.Test.Functional
{
    public class BaseTest
    {
        /// <summary>
        /// Gets the instance of Selenium <see cref="IWebDriver"/>.
        /// </summary>
        /// <value>The current instance of Selenium <see cref="IWebDriver"/>.</value>
        public IWebDriver Driver { get; private set; }

        /// <summary>
        /// Gets the root URL of the application being tested.
        /// </summary>
        /// <value>The root URL of the application being tested.</value>
        public Uri RootUrl { get; private set; }

        /// <summary>
        /// Sets up before executing all tests in this fixture
        /// </summary>
        [OneTimeSetUp]
        public void SetUpEnvironmentBeforeAllTests()
        {
            //
            // Currently using ChromeDriver directly. 
            // In the future, we should encapsulate it and use Autofac 
            // dependency injection to instantiate any browser driver.
            //

            var options = new ChromeOptions();

            if (Settings.Default.UseFiddler)
            {

                options.Proxy = new Proxy {HttpProxy = "127.0.0.1:8888"};

            }

            Driver = new ChromeDriver(options);

            RootUrl = Settings.Default.RootUrl;            
        }

        /// <summary>
        /// Setups the environment before each test.
        /// </summary>
        [SetUp]
        public void SetupEnvironmentBeforeEachTest()
        {
            Driver.Navigate().GoToUrl( RootUrl );
        }

                /// <summary>
        /// Cleans up enviroment after each test.
        /// </summary>
        [TearDown]
        public void CleanUpEnviromentAfterEachTest()
        {
            // Right now, the only cleanup action that happens is a simple logout.
            Driver.Navigate().GoToUrl("/wp-login.php?action=logout");
        }



        /// <summary>
        /// Cleans up the environment after executing all tests in this fixture.
        /// </summary>
        [OneTimeTearDown]
        public void CleanUpEnvironmentAfterAllTests()
        {
            Driver.Quit();
        }
    }
}
