namespace WebShop.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base("Bad Request", message)
        {

        }
    }
}
