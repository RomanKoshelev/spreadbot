﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Mws_Connector_Tests.cs

using System;
using System.IO;
using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Connector;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.StatusCode;
using Spreadbot.Nunit.Amazon.Base;

// Code: Mws_Connector_Tests

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Mws_Connector_Tests : Amazon_Tests
    {
        [SetUp]
        public static void Init() {}

        [Test]
        public void Submit_Product_Feed()
        {
            SubmitFeedTest( MwsFeedType.Product );
        }

        [Test]
        public void Submit_Inventory_Feed()
        {
            SubmitFeedTest( MwsFeedType.Inventory );
        }

        [Test]
        public void Submit_Price_Feed()
        {
            SubmitFeedTest( MwsFeedType.Price );
        }

        [Test]
        public void Submit_Image_Feed()
        {
            SubmitFeedTest( MwsFeedType.Image );
        }


        private const int RecentFeedsNumber = 10;
        
        [Test]
        public void Obtain_Submitted_Feeds_List()
        {
            
        }

        [Test]
        public void Recent_Feed_Submissions_Completed_Without_Errors()
        {
/*
            try {
                var response = GetFeedSubmissionDoneList();
                if( response.GetFeedSubmissionListResult.HasNext ) {
                    Assert.Inconclusive( "Too many completed feed submissions, need to use Next Token" );
                }

                var recentFeedSubmissionIds =
                    response.GetFeedSubmissionListResult.FeedSubmissionInfo.Where( info => info.IsSetFeedSubmissionId() )
                        .Reverse()
                        .ToList();
                var recentNIds =
                    recentFeedSubmissionIds.Skip( Math.Max( 0, recentFeedSubmissionIds.Count() - RecentFeedsNumber ) ).ToList();
                if( !recentNIds.Any() ) {
                    Assert.Inconclusive( "No completed feed submissions" );
                }

                recentNIds.ForEach( info => {
                    Console.WriteLine();
                    Console.WriteLine( info.SubmittedDate );
                    Console.WriteLine( info.FeedSubmissionId );
                    var resultResponse = GetFeedSubmissionStatus( info.FeedSubmissionId );
                    Console.WriteLine( resultResponse );
                    Assert_That_Text_Contains( resultResponse, "<MessagesWithError>0</MessagesWithError>" );
                    Assert_That_Text_Contains( resultResponse, "<MessagesWithWarning>0</MessagesWithWarning>" );
                } );
            }
            catch( MarketplaceWebServiceException e ) {
                if( e.Message.Contains( "Request is throttled" ) ) {
                    Assert.Inconclusive( "Request is throttled" );
                }
            }*/

        }

        /*
        [Test]
        public void Find_Request_Inprocess() {}

        [Test]
        public void Get_Request_Status_Inproc() {}

        [Test]
        public void Get_Request_Status_Success() {}

*/

        // ===================================================================================== []
        // Utils
        private const string FeedFileTemplate = @"{0}Samples\SB_AMZ_002\{1}.Feed.xml";

        private static MwsFeedHandler MakeMwsFeedHandler( MwsFeedType mwsFeedType )
        {
            var fileNAme = string.Format( FeedFileTemplate, AmazonSettings.BasePath, mwsFeedType );
            return new MwsFeedHandler( mwsFeedType ) {
                Content = File.ReadAllText( fileNAme )
            };
        }

        private static void SubmitFeedTest( MwsFeedType feedType )
        {
            var feed = MakeMwsFeedHandler( feedType );
            var response = MwsConnector.Instance.SubmitFeed( feed );

            Console.WriteLine( response );
            IgnoreMwsThrottling( response );

            Assert.AreEqual( MwsOperationStatus.SubmitFeedSuccess, response.StatusCode, feedType.ToString() );
        }
    }
}