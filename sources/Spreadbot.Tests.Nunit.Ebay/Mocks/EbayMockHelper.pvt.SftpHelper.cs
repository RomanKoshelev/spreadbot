﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// EbayMockHelper.pvt.SftpHelper.cs

using System;
using System.IO;
using Moq;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.StatusCode;
using Spreadbot.Core.Channels.Ebay.Services.Mip.SftpHelper;

namespace Spreadbot.Nunit.Ebay.Mocks
{
    internal partial class EbayMockHelper
    {
        private static void ConfigureSftpHelperToGetContentFromLocalFolder( Mock< WinScpSftpHelper > mockSftpHelper )
        {
            ConfigureSftpHelperToIgnoreInprocess( mockSftpHelper );
            ConfigureSftpHelperToAnswerFoundInOutput( mockSftpHelper );
            ConfigureSftpHelperToGetContentFromLocalFile( mockSftpHelper );
        }

        private static void ConfigureSftpHelperToGetContentFromLocalFile( Mock< WinScpSftpHelper > mockSftpHelper )
        {
            mockSftpHelper
                .Setup(
                    mock => mock.GetRemoteFileContent(
                        It.IsAny< string >(),
                        It.IsAny< string >(),
                        It.IsAny< string >() ) )
                .Returns( (
                    string remoteFolder,
                    string fileName,
                    string localFolder )
                    => {
                    var filePath = string.Format( @"{0}\{1}", localFolder, fileName );
                    return File.ReadAllText( filePath );
                } );
        }

        private static void ConfigureSftpHelperToAnswerFoundInOutput( Mock< WinScpSftpHelper > mockSftpHelper )
        {
            mockSftpHelper
                .Setup(
                    mock => mock.FindRemoteFile(
                        It.IsAny< string >(),
                        It.Is< String >( s => s.ToLower().Contains( "output" ) ) ) )
                .Returns( (
                    string filePrefix,
                    string remoteDir )
                    => new MipResponse< MipFindRemoteFileResult > {
                        IsSuccess = true,
                        StatusCode = MipOperationStatus.FindRemoteFileSuccess,
                        Result = new MipFindRemoteFileResult {
                            RemoteDir = "fake",
                            RemoteFileName = filePrefix + ".xml",
                        }
                    } );
        }

        private static void ConfigureSftpHelperToIgnoreInprocess( Mock< WinScpSftpHelper > mockSftpHelper )
        {
            mockSftpHelper
                .Setup(
                    mock => mock.FindRemoteFile(
                        It.IsAny< string >(),
                        It.Is< String >( s => s.ToLower().Contains( "inproc" ) ) ) )
                .Returns(
                    new MipResponse< MipFindRemoteFileResult > {
                        IsSuccess = false,
                        StatusCode = MipOperationStatus.FindRemoteFileFailure
                    } );
        }
    }
}