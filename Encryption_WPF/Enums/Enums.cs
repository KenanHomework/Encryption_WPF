using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_WPF.Enums
{
    public enum ProcessResult { Success, NotFound, Empty, IncorrectPassword, Existed, FileError, Error, EmptyArguments }

    public enum DialogResult { Success, Cancel, Ok, No, Yes }

    public enum CMessageButton { Ok, Yes, No, Cancel, Confirm, None }

    public enum EncryptOption {Encrypt, Decrypt }

    public enum CMessageTitle { Error, Info, Warning, Confirm }

}
