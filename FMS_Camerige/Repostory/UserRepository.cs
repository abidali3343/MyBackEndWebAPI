using Dapper;
using FMS_Camerige.Data;
using FMS_Camerige.UoW;
using System.Data;

namespace FMS_Camerige.Repostory
{
    public class UserRepository:IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public User AuthenticateUser(string email)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email, DbType.String, ParameterDirection.Input);
                //parameters.Add("@Password", password, DbType.String, ParameterDirection.Input);
                var user = _unitOfWork.Connection.QueryFirstOrDefault<User>(
                    "sp_AuthenticateUser",  
                    parameters,             
                    commandType: CommandType.StoredProcedure, 
                    transaction: _unitOfWork.Transaction      
                );

                return user; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error authenticating user: {ex.Message}");
                throw new InvalidOperationException("An error occurred while authenticating the user.", ex);
            }
        }


    }
}
