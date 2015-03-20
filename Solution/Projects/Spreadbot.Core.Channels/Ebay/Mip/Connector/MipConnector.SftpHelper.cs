﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.SftpHelper.cs
// romak_000, 2015-03-20 13:56

using System;
using System.IO;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // TestConnection
            public static MipResponse< MipTestConnectionResult > TestConnection( string password = null )
            {
                return DoTestConnection( password );
            }

            // ===================================================================================== []
            // UploadFeed
            public static MipResponse< MipSendZippedFeedFolderResult > SendZippedFeed( string feed, Guid reqId )
            {
                return DoSendZippedFeed( feed, reqId );
            }

            public static MipResponse< MipSendZippedFeedFolderResult > SendZippedFeed( MipFeedHandler mipFeedHandler, Guid reqId )
            {
                return SendZippedFeed( mipFeedHandler.Name, reqId );
            }

            // ===================================================================================== []
            // Find remote files Inproc
            public static MipResponse< MipFindRemoteFileResult > FindRequestRemoteFileNameInInprocess(
                MipRequest mipRequest )
            {
                var remoteDir = RemoteFeedInprocessFolderPath( mipRequest.MipFeedHandler.Name );
                var prefix = mipRequest.FileNamePrefix();

                return FindRemoteFileNamePrefixInRemoteDir( prefix, remoteDir );
            }

            // ===================================================================================== []
            // Find remote files Output
            public static MipResponse< MipFindRemoteFileResult > FindRequestRemoteFileNameInOutput(
                MipRequest mipRequest )
            {
                var remoteDirs = RemoteFeedOutputFolderPathes( mipRequest.MipFeedHandler.Name );
                var prefix = mipRequest.FileNamePrefix();

                foreach( var remoteDir in remoteDirs ) {
                    var response = FindRemoteFileNamePrefixInRemoteDir( prefix, remoteDir );
                    if( response.Code == MipStatusCode.FindRemoteFileSuccess ) {
                        return response;
                    }
                }
                return new MipResponse< MipFindRemoteFileResult >(
                    false,
                    MipStatusCode.FindRemoteFileFail,
                    "Remote file [{0}] not found in [{1}]".SafeFormat( prefix, remoteDirs.FoldToStringBy( s => s ) ) );
            }

            // ===================================================================================== []
            // GetRemoteFileContent
            public static string GetRemoteFileContent( string remoteFolder, string fileName, string localFolder )
            {
                var remotePath = @"{0}/{1}".SafeFormat( remoteFolder, fileName );
                var localPath = @"{0}\{1}".SafeFormat( localFolder, fileName );

                DownloadFiles( remotePath, localPath );

                return File.ReadAllText( @"{0}\{1}".SafeFormat( localFolder, fileName ) );
            }
        }
    }
}