using System;

namespace Web.Utils
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
            : base("User with this email already exists")
        {
        }

        public UserAlreadyExistsException(string message)
            : base(message)
        {
        }
    }

    public class WrongPasswordException : Exception
    {
        public WrongPasswordException()
            : base("Wrong password")
        {
        }

        public WrongPasswordException(string message)
            : base(message)
        {
        }
    }
}