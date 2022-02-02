using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace ZK_Lymytz.TOOLS
{
    class RemoteAcces
    {
        string adresse = "51.68.230.114";
        int port = 1910;
        string protocol = "FTP";

        string users = "yves";
        string password = "yves1910/";

        public RemoteAcces(string adresse, int port, string protocol)
        {
            this.adresse = adresse;
            this.port = port;
            this.protocol = protocol;
        }

        public RemoteAcces(string adresse, int port, string protocol, string users, string password)
            : this(adresse, port, protocol)
        {
            this.users = users;
            this.password = password;
        }

        public string GetPathFile(string file_local)
        {
            try
            {
                switch (protocol)
                {
                    case "FTP":
                        return new FTP(adresse, port, users, password).UploadFile(file_local);
                    case "SFTP":
                        return new SFTP(adresse, port, users, password).UploadFile(file_local);
                    default:
                        return new DESKTOP(adresse, users, password).UploadFile(file_local);
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
            return null;
        }
    }

    class FTP
    {
        string adresse = "51.68.230.114";
        int port = 1910;
        string users = "lymytz";
        string password = "yves1910/";
        string host = "ftp://";
        string path_root = "/home/";

        public FTP(string adresse, int port, string users, string password)
        {
            this.adresse = adresse;
            this.port = port;
            this.users = users;
            this.password = password;
            this.host += adresse + ":" + port + "/files/";
            this.path_root += users + "/files/";
        }

        public string UploadFile(string file_local)
        {
            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(file_local);
                if (file.Exists)
                {
                    string remote_path = host + file.Name;
                    using (WebClient client = new WebClient())
                    {
                        client.Credentials = new NetworkCredential(users, password);
                        client.UploadFile(remote_path, WebRequestMethods.Ftp.UploadFile, @file_local);
                    }
                    return path_root + file.Name;
                }
            }
            catch (Exception ex)
            {
                var _ex_ = ex;
                Messages.Exception(ex);
            }
            return null;
        }
    }

    class SFTP
    {
        string adresse = "51.68.230.114";
        int port = 1910;
        string users = "lymytz";
        string password = "yves1910/";
        string path_root = "/home/";

        public SFTP(string adresse, int port, string users, string password)
        {
            this.adresse = adresse;
            this.port = port;
            this.users = users;
            this.password = password;
            this.path_root += users+"/files/";
        }

        public string UploadFile(string file_local)
        {
            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(file_local);
                if (file.Exists)
                {
                    string remote_path = path_root + file.Name;
                    var connectionInfo = new ConnectionInfo(adresse, Convert.ToInt16(port), users, new PasswordAuthenticationMethod(users, password));
                    using (var sftp = new SftpClient(connectionInfo))
                    {
                        sftp.Connect();
                        sftp.ChangeDirectory("files");
                        using (var uplfileStream = System.IO.File.OpenRead(file.FullName))
                        {
                            sftp.UploadFile(uplfileStream, file.Name, true);
                        }
                        sftp.Disconnect();
                    }
                    return remote_path;
                }
            }
            catch (Exception ex)
            {
                var _ex_ = ex;
                Messages.Exception(ex);
            }
            return null;
        }
    }

    class DESKTOP
    {
        string adresse = "51.68.230.114";
        string users = "hp elite 8300";
        string password = "lymytz109374141/";
        string host = "\\\\";

        public DESKTOP(string adresse, string users, string password)
        {
            this.adresse = adresse;
            this.users = users;
            this.password = password;
            this.host += adresse + "\\ZK-Lymytz";
        }

        public string UploadFile(string file_local)
        {
            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(file_local);
                if (file.Exists)
                {
                    string remote_path = host + "\\" + file.Name;
                    using (new NetworkConnection(@host, new NetworkCredential(users, password)))
                    {
                        if (File.Exists(@remote_path))
                        {
                            File.Delete(@remote_path);
                        }
                        file.CopyTo(@remote_path);
                    }
                    return @remote_path;
                }
            }
            catch (Exception ex)
            {
                var _ex_ = ex;
                Messages.Exception(ex);
            }
            return null;
        }
    }

    class NetworkConnection : IDisposable
    {
        string _networkName;

        public NetworkConnection(string networkName, NetworkCredential credentials)
        {
            _networkName = networkName;

            var netResource = new NetResource()
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = networkName
            };

            var userName = string.IsNullOrEmpty(credentials.Domain) ? credentials.UserName : string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

            var result = WNetAddConnection2(netResource, credentials.Password, userName, 0);

            if (result != 0)
            {
                throw new Win32Exception(result);
            }
        }

        ~NetworkConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(_networkName, 0, true);
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource, string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags, bool force);
    }

    [StructLayout(LayoutKind.Sequential)]
    class NetResource
    {
        public ResourceScope Scope;
        public ResourceType ResourceType;
        public ResourceDisplaytype DisplayType;
        public int Usage;
        public string LocalName;
        public string RemoteName;
        public string Comment;
        public string Provider;
    }

    enum ResourceScope : int
    {
        Connected = 1,
        GlobalNetwork,
        Remembered,
        Recent,
        Context
    };

    enum ResourceType : int
    {
        Any = 0,
        Disk = 1,
        Print = 2,
        Reserved = 8,
    }

    enum ResourceDisplaytype : int
    {
        Generic = 0x0,
        Domain = 0x01,
        Server = 0x02,
        Share = 0x03,
        File = 0x04,
        Group = 0x05,
        Network = 0x06,
        Root = 0x07,
        Shareadmin = 0x08,
        Directory = 0x09,
        Tree = 0x0a,
        Ndscontainer = 0x0b
    }
}
