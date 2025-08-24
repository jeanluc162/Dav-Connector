using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Dav_Connector
{
    static class EncryptionPasswordHelper
    {
        public static String GetEncryptionPassword()
        {
            const String VaultIdentifier = "DavConnector";
            var vault = new PasswordVault();
            PasswordCredential credential;
            try
            {
                credential = vault.Retrieve(VaultIdentifier, VaultIdentifier);
            }
            catch
            {
                credential = new PasswordCredential(VaultIdentifier, VaultIdentifier, Guid.NewGuid().ToString());
                vault.Add(credential);
            }
            return credential.Password;
        }
    }
}
