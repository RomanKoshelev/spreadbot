﻿// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// MipConnector_Zip_Tests.cs
// Roman, 2015-04-07 2:58 PM

using System;
using System.IO;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Tests.NUnit.Code;

namespace Tests.NUnit.Units
{
    [TestFixture]
    public class MipConnector_Zip_Tests : SpreadbotTestBase
    {
        // --------------------------------------------------------[]
        [SetUp]
        public static void Init()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // --------------------------------------------------------[]
        [Test]
        public void ZipFeed()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var reqId = MipRequestHandler.GenerateZeroId();

            var response = MipConnector.Instance.ZipHelper.ZipFeed( feed, reqId );
            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.ZipFeedSuccess, response.StatusCode );
            Assert.IsTrue( File.Exists( response.Result.ZipFileName ) );
            Assert.AreEqual( MipOperationStatus.ZipFeedSuccess, response.StatusCode );
        }
    }
}