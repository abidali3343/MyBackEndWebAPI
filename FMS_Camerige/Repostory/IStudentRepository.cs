using FMS_Camerige.Data;

namespace FMS_Camerige.Repostory
{
    public interface IStudentRepository
    {
        Task<int> AddStudentAsync(StudentModel student);
        Task<int> AddFeeAsync(AddFeeModel addFee);
        Task<List<GetStudentModel>>GetAllStudentsAsync();
        Task<List<AddFeeModel>> GetFeeAsync();
    }
}
