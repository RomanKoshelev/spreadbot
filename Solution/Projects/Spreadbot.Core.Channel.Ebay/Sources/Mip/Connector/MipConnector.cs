﻿using System;
using System.IO;
using Crocodev.Common;
using Crocodev.Common.Identifier;

// >> Core | Connector

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public partial class MipConnector
    {
        // ===================================================================================== []
        // SendFeedFolder
        public static MipResponse<MipSendZippedFeedFolderResult> SendZippedFeedFolder(MipFeed mipFeed)
        {
            var reqId = MipRequest.GenerateId();
            return DoSendZippedFeedFolder(mipFeed, reqId);
        }

        // --------------------------------------------------------[]
        public static MipResponse<MipSendZippedFeedFolderResult> SendTestFeedFolder(MipFeed mipFeed)
        {
            var reqId = MipRequest.GenerateTestId();
            return DoSendZippedFeedFolder(mipFeed, reqId);
        }

        // --------------------------------------------------------[]
        private static MipResponse<MipSendZippedFeedFolderResult>
            DoSendZippedFeedFolder(MipFeed mipFeed, Identifiable<MipRequest, Guid>.Identifier reqId)
        {
            try
            {
                ZipHelper.ZipFeed(mipFeed, reqId).Check();
                SftpHelper.SendZippedFeed(mipFeed, reqId).Check();
            }
            catch (Exception exception)
            {
                return new MipResponse<MipSendZippedFeedFolderResult>(false,
                    MipStatusCode.SendZippedFeedFolderFail,
                    exception
                    );
            }
            return new MipResponse<MipSendZippedFeedFolderResult>(true,
                MipStatusCode.SendZippedFeedFolderSuccess,
                new MipSendZippedFeedFolderResult(reqId)
                );
        }

        // ===================================================================================== []
        // FindRequest
        public static MipResponse<MipFindRemoteFileResult> FindRequest(MipRequest mipRequest, MipRequestProcessingStage stage)
        {
            MipResponse<MipFindRemoteFileResult> findResponse;
            try
            {
                switch (stage)
                {
                    case MipRequestProcessingStage.Inprocess:
                        findResponse = SftpHelper.FindRequestRemoteFileNameInInprocess(mipRequest);
                        break;
                    case MipRequestProcessingStage.Output:
                        findResponse = SftpHelper.FindRequestRemoteFileNameInOutput(mipRequest);
                        break;
                    default:
                        throw new Exception("Wrong stage {0}".SafeFormat(stage));
                }
                findResponse.Check();
            }
            catch (Exception exception)
            {
                return new MipResponse<MipFindRemoteFileResult>(false, MipStatusCode.FindRequestFail, exception);
            }
            return new MipResponse<MipFindRemoteFileResult>(true, MipStatusCode.FindRequestSuccess, findResponse.Result,
                findResponse);
        }

        // ===================================================================================== []
        // GetRequestStatus
        public static MipResponse<MipGetRequestStatusResult> GetRequestStatus(MipRequest mipRequest, bool ignoreInprocess = false)
        {
            try
            {
                var response = FindRequest(mipRequest, MipRequestProcessingStage.Inprocess);
                if (response.Code == MipStatusCode.FindRequestSuccess && !ignoreInprocess)
                {
                    return new MipResponse<MipGetRequestStatusResult>(true,
                        MipStatusCode.GetRequestStatusSuccess,
                        new MipGetRequestStatusResult(MipRequestStatus.Inprocess)
                        );
                }

                response = FindRequest(mipRequest, MipRequestProcessingStage.Output);
                if (response.Code == MipStatusCode.FindRequestSuccess)
                {
                    return GetRequestOutputStatus(response);
                }

                return new MipResponse<MipGetRequestStatusResult>(true,
                    MipStatusCode.GetRequestStatusSuccess,
                    new MipGetRequestStatusResult(MipRequestStatus.Unknown),
                    response
                    );
            }
            catch (Exception exception)
            {
                return new MipResponse<MipGetRequestStatusResult>(false, MipStatusCode.GetRequestStatusFail, exception);
            }
        }

        // --------------------------------------------------------[]
        private static MipResponse<MipGetRequestStatusResult> GetRequestOutputStatus(
            MipResponse<MipFindRemoteFileResult> response)
        {
            var statusResult = ReadRequestOutputStatus(response);
            return new MipResponse<MipGetRequestStatusResult>(true, MipStatusCode.GetRequestStatusSuccess, statusResult);
        }

        // --------------------------------------------------------[]
        private static MipGetRequestStatusResult ReadRequestOutputStatus(MipResponse<MipFindRemoteFileResult> response)
        {
            var fileName = response.Result.FileName;
            var remotePath = response.Result.FolderPath;
            var localPath = LocalRequestResultsFolder();
            var content = SftpHelper.GetRemoteFileContent(remotePath, fileName, localPath);
            return ParseRequestContent(content);
        }

        // --------------------------------------------------------[]
        private static MipGetRequestStatusResult ParseRequestContent(string content)
        {
            // Todo: Later : Parse XML
            return new MipGetRequestStatusResult(
                content.Contains("<status>SUCCESS</status>")
                    ? MipRequestStatus.Success
                    : MipRequestStatus.Fail,
                content);
        }

        // --------------------------------------------------------[]
        public static string LocalFeedXmlFilePath(MipFeed mipFeed)
        {
            return DoLocalFeedXmlFilePath(mipFeed);
        }

        // --------------------------------------------------------[]
        public static string LocalFeedFolder(MipFeed mipFeed)
        {
            return DoLocalFeedFolder(mipFeed.Name);
        }
    }
}