using System;
using LexisSFTP;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string localFile = "";
            string remoteFile = "";
            string method = "";
            string host = "";
            string user = "";
            string password = "";
            string mode = "ascii";

            string[] arguments;
            LexisSFTP.Class1 lx = new LexisSFTP.Class1();

            foreach (string element in Environment.GetCommandLineArgs())
            {
                arguments = element.Split("=");
                switch (arguments[0])
                {
                    case "localfile":
                        localFile = arguments[1];
                        break;
                    case "remotefile":
                        remoteFile = arguments[1];
                        break;
                    case "method":
                        method = arguments[1];
                        break;
                    case "host":
                        host = arguments[1];
                        break;
                    case "user":
                        user = arguments[1];
                        break;
                    case "password":
                        password = arguments[1];
                        break;
                }
            }    // end for each loop

            if (validateConnectionParameters())
            {

                lx.setRemoteFileName(remoteFile);
                lx.setLocalFileName(localFile);

                lx.setConnectionParams(method, host, user, password);
            }
            else
            {
                Console.WriteLine("Problem with connection parameters");
                Environment.Exit(7);
            }
            if (mode.Length > 0 && (mode.ToLower() == "ascii" || mode.ToLower() == "binary"))
            {
                lx.setTransferType(mode);
                Console.WriteLine("transfer mode: is {0}", mode);
            }

            if (method.ToLower() == "get")
            {
                int rc;
                rc = lx.getFile(remoteFile, localFile);
                if (rc > 0)
                {
                    Console.WriteLine("Problem with download.");
                }
                lx.clientDisconnect();
            }
            if (method.ToLower() == "put")
            {
                int rc;
                rc = lx.putFile(localFile, remoteFile);
                if (rc > 0)
                {
                    Console.WriteLine("Problem uploading file");
                }
                lx.clientDisconnect();
            }

            bool validateConnectionParameters()
            {
                bool paramsOK = true;
                if (host.Length < 1)
                    paramsOK = false;
                if (user.Length < 1)
                    paramsOK = false;
                if (password.Length < 1)
                    paramsOK = false;
                if (method.ToLower() != "put" && method.ToLower() != "get")
                    paramsOK = false;
                return paramsOK;
            }
            Console.WriteLine("Transfer Complete");
        }  // end of main method
    }
}
