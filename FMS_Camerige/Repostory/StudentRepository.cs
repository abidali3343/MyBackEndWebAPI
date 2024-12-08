using Dapper;
using FMS_Camerige.Data;
using FMS_Camerige.UoW;
using System.Data;
using System.Data.SqlClient;

namespace FMS_Camerige.Repostory
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
     
        public async Task<int> AddFeeAsync(AddFeeModel addFee)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(new
                {
                    RollNumber = addFee.RollNumber,
                    MonthlyFee = addFee.MonthlyFee,
                    ReceivedFee = addFee.ReceivedFee,
                    RemainingFee = addFee.RemainingFee,
                    Class =       addFee.Class,
                    ReceivedBy = addFee.ReceivedBy,
                    Remarks = addFee.Remarks,
                    FeeMonth = addFee.FeeMonth,
                 
                });


                var result = await _unitOfWork.Connection.ExecuteAsync(
                    "Sp_FMS_AddFee",
                    parameters,
                    commandType: CommandType.StoredProcedure,
                    transaction: _unitOfWork.Transaction
                );

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding student: {ex.Message}");
                throw new InvalidOperationException("An error occurred while adding the student.", ex);
            }
        }
        public async Task<List<AddFeeModel>> GetFeeAsync()
        {
            try
            {
                var query = "FMS_Sp_GetFee";

                var addFees = await _unitOfWork.Connection.QueryAsync<AddFeeModel>(query, commandType: CommandType.StoredProcedure);

                return addFees.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while fetching students from the database.");
            }
        }

        public async Task<int> AddStudentAsync(StudentModel student)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(new
                {
                    RollNumber = student.RollNumber,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    FatherName = student.FatherName,
                    PhoneNumber = student.PhoneNumber,
                    RelativeNumber = student.RelativeNumber,
                    DateOfBirth = student.DateOfBirth,
                    StudentBForm = student.StudentBForm,
                    Address = student.Address,
                    FatherCNIC = student.FatherCNIC,
                    Class = student.Class,
                    DateOfAdmission = student.DateOfAdmission,
                    Village = student.Village,
                    Migration = student.Migration,
                    Section = student.Section,
                    Fee = student.Fee,
                    Remarks = student.Remarks
                });

                // Add a parameter to capture the return value
                parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                // Execute the stored procedure
                await _unitOfWork.Connection.ExecuteAsync(
                    "Sp_FMS_AddStudent",
                    parameters,
                    commandType: CommandType.StoredProcedure,
                    transaction: _unitOfWork.Transaction
                );

                // Get the return value from the stored procedure
                int returnValue = parameters.Get<int>("@ReturnValue");

                return returnValue;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the student.", ex);
            }
        }


        public async Task<List<GetStudentModel>> GetAllStudentsAsync()
        {
            try
            {
                var query = "FMS_Sp_GetAllStudents"; 

                var students = await _unitOfWork.Connection.QueryAsync<GetStudentModel>(query,commandType: CommandType.StoredProcedure);

                return students.ToList();
            }
            catch (Exception ex)
            {
              
                throw new Exception("An error occurred while fetching students from the database.");
            }
        }

    }

}
