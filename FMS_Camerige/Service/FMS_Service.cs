using FMS_Camerige.Data;
using FMS_Camerige.Repostory;
using FMS_Camerige.UoW;

namespace FMS_Camerige.Service
{
    public class FMS_Service
    {
    
    
      private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public FMS_Service(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

        public async Task<User> UserLogin(string email, string password)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                var user = _userRepository.AuthenticateUser(email);

                if (user == null)
                {
                    _unitOfWork.Rollback();
                    return null; 
                }
                _unitOfWork.Commit();

                return  user; 
            }
            catch (Exception)
            {
                
                _unitOfWork.Rollback();
                throw;
            }
        }

    }
}
