﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Sftp_Tests.cs
// romak_000, 2015-03-25 15:24

using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestClass]
    public class MipConnector_Sftp_Tests
    {
        // ===================================================================================== []
        [ClassInitialize]
        public static void Init( TestContext testContext )
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // ===================================================================================== []
        [TestMethod]
        public void SendZippedFeed()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var response = MipConnector.SftpHelper.SendZippedFeed( feed.GetName(), MipRequestHandler.GenerateTestId() );

            Trace.TraceInformation( response.Autoinfo );

            Assert.AreEqual( MipOperationStatus.SendZippedFeedSuccess, response.Code );
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Good()
        {
            var response = MipConnector.SftpHelper.TestConnection();

            Trace.TraceInformation( response.Autoinfo );

            Assert.AreEqual( MipOperationStatus.TestConnectionSuccess, response.Code );
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Bad()
        {
            var response = MipConnector.SftpHelper.TestConnection( "wrong password" );

            Trace.TraceInformation( response.Autoinfo );

            Assert.AreEqual( MipOperationStatus.TestConnectionFailure, response.Code );
        }
    }
}