﻿// ReSharper disable RedundantUsingDirective
using System;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // PutFiles
            private static void PutFiles(string localPath, string remotePath)
            {
                using (var session = new Session())
                {
                    var sessionOptions = CreateSessionOptions();
                    var transferOptions = CreateTransferOptions();

                    session.Open(sessionOptions);

                    var transferResult = session.PutFiles(
                        localPath,
                        remotePath,
                        false,
                        transferOptions
                        );

                    transferResult.Check();
                }
            }

            // ===================================================================================== []
            // CreateSessionOptions
            private static SessionOptions CreateSessionOptions(string password = null)
            {
                return new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    GiveUpSecurityAndAcceptAnySshHostKey = true,
                    HostName = Settings.HostName,
                    PortNumber = Settings.PortNumber,
                    UserName = Settings.UserName,
                    Password = password ?? Settings.Password
                };
            }

            // ===================================================================================== []
            // CreateTransferOptions
            private static TransferOptions CreateTransferOptions()
            {
                return new TransferOptions
                {
                    TransferMode = TransferMode.Binary
                };
            }
        }
    }
}