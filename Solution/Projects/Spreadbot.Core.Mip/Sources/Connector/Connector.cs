﻿using System;
using System.Diagnostics;
using Crocodev.Common;

// >> Connector

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // SendFeed
        public static Response SendFeed(Feed feed, Request.Identifier reqId = null)
        {
            reqId = reqId ?? Request.GenerateId();
            try
            {
                ZipHelper.ZipFeed(feed, reqId).Check();
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception e)
            {
                return Response.NewFail(StatusCode.SendFeedFail, e);
            }
            return Response.NewSuccess(StatusCode.SendFeedSuccess, reqId);
        }

        public static Response SendTestFeed(Feed feed)
        {
            return SendFeed(feed, Request.GenerateTestId());
        }

        // ===================================================================================== []
        // FindRequest
        public static Response FindRequest(Request request, RequestProcessingStage stage)
        {
            Response ftpResponce;
            try
            {
                switch (stage)
                {
                    case RequestProcessingStage.Inprocess:
                        ftpResponce = SftpHelper.FindRequestRemoteFileNameInInprocess(request);
                        break;
                    case RequestProcessingStage.Output:
                        ftpResponce = SftpHelper.FindRequestRemoteFileNameInOutput(request);
                        break;
                    default:
                        throw new Exception("Wrong stage {0}".SafeFormat(stage));
                }
                ftpResponce.Check();
            }
            catch (Exception e)
            {
                return Response.NewFail(StatusCode.FindRequestFail, e);
            }
            return Response.NewSuccess(StatusCode.FindRequestSuccess, ftpResponce.Result, ftpResponce);
        }

        // ===================================================================================== []
        // GetRequestStatus
        public static Response GetRequestStatus(Request request)
        {
            // Now: GetRequestStatus
            try
            {
                var response = FindRequest(request, RequestProcessingStage.Inprocess);
                if (response.StatusCode == StatusCode.FindRequestSuccess)
                {
                    return Response.NewSuccess(StatusCode.GetRequestStatusSuccess, new GetRequetStatusResult(RequetStatus.Inprocess));
                }

                response = FindRequest(request, RequestProcessingStage.Output);
                if (response.StatusCode == StatusCode.FindRequestSuccess)
                {
                    return GetRequestOutputStatus(response);
                }

                return Response.NewSuccess(StatusCode.GetRequestStatusSuccess, RequetStatus.Unknown,
                    response.StatusDescription);
            }
            catch (Exception e)
            {
                return Response.NewFail(StatusCode.GetRequestStatusFail, e);
            }
        }

        // --------------------------------------------------------[]
        private static Response GetRequestOutputStatus(Response response)
        {
            GetRequetStatusResult statusResult;
            string description;
            ReadRequestOutputStatus(response, out statusResult, out description);
            return Response.NewSuccess(StatusCode.GetRequestStatusSuccess, statusResult, description);
        }

        // --------------------------------------------------------[]
        private static void ReadRequestOutputStatus(Response response, out GetRequetStatusResult statusResult,
            out string description)
        {
            throw new NotImplementedException();
/*            var fileName = (string) response.Result;
            var remotePath = RemoteFeedOutputFolderPath();
            var localPath = LocalRequestResultsFolder();
            var content = SftpHelper.GetRemoteFileContent(
                , 
                
                );
            ParseRequestContent(content, out status, out description);

            Trace.Assert(
                status == RequetStatus.Fail || status == RequetStatus.Success,
                "Unknown status=[{0}]".SafeArgs(status)
                );*/
        }

        // --------------------------------------------------------[]
        private static GetRequetStatusResult ParseRequestContent(string content)
        {
            // Todo: Parse XML
            return new GetRequetStatusResult(
                content.Contains("<status>SUCCESS</status>")
                    ? RequetStatus.Success
                    : RequetStatus.Fail,
                content);
        }
    }
}