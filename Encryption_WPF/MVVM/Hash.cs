using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_WPF.MVVM
{
    public class Hash
    {

        public string Password { get; set; }

        int index = 0;

        public Hash(string password)
        {
            Password = password;
        }

        public Hash()
        {

        }

        public void Reset(string pasword)
        {
            Password = pasword;
            index = 0;
        }

        void InitIndex()
        {
            index++;
            if (index >= Password.Length)
                index = 0;
        }
        
        public char Encrypt(byte sym)
        {
            InitIndex();

            return (char)(sym ^ 'k');
        }
    }
}
