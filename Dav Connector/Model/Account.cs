using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Dav_Connector.Model
{
    class Account:EntityBase
    {
        public String Name { get; set; }
        public String UserName { get; set; }
        [NotMapped]
        public String Password {
            get => StringCipher.Decrypt(EncryptedPassword, GetPassword());
            set => EncryptedPassword = StringCipher.Encrypt(value, GetPassword());
        }
        public String EncryptedPassword { get; set; }


        private String GetPassword()
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
