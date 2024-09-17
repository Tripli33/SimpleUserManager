namespace SimpleUserManager.Exceptions;

public class UserBlockedException(string email) : Exception($"User with email: {email} ")
{
}
