using FMS_Camerige.Data;

namespace FMS_Camerige.Repostory
{
    public interface IUserRepository
    {
        User AuthenticateUser(string email);
        

    }

}
