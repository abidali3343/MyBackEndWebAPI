using FMS_Camerige.Data;
using FMS_Camerige.Repostory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMS_Camerige.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentRepository _studentrepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentrepository = studentRepository;
        }
        [HttpPost("add-student")]
        public async Task<IActionResult> AddStudent([FromBody] StudentModel student)
        {
            if (student == null || !ModelState.IsValid)
            {
                return BadRequest(new { status = 0, message = "Invalid student data." });
            }

            try
            {
                var result = await _studentrepository.AddStudentAsync(student);

                if (result == 1)
                {
                    return Ok(new { status = 1, message = "Student added successfully." });
                }

                if (result == 0)
                {
                    return Conflict(new { status = 0, message = "Student with this RollNumber already exists." });
                }

                if (result == -1)
                {
                    return StatusCode(500, new { status = -1, message = "Failed to add student due to an internal error." });
                }

                return StatusCode(500, new { status = -2, message = "An unexpected status was returned from the database." });
            }
            catch (Exception ex)
            {
              

                return StatusCode(500, new { status = -1, message = "Internal server error." });
            }
        }



        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                // Fetch all students from the repository (replace with your actual method to get students)
                var students = await _studentrepository.GetAllStudentsAsync();

                if (students == null)
                {
                    return NotFound(new { ErrorCode = "STU01", Message = "No students found." });
                }

                // Return the students array directly
                return Ok(students);  // This should return an array of students
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "SRV01", Message = "Internal server error." });
            }
        }

        [HttpPost("add-Fee")]
        public async Task<IActionResult> AddFee([FromBody] AddFeeModel addFee)
        {
            if (addFee == null || !ModelState.IsValid)
            {
                return BadRequest(new { ErrorCode = "REQ01", Message = "Invalid student data." });
            }

            try
            {
                var result = await _studentrepository.AddFeeAsync(addFee);

                if (result == 0)
                {
                    return StatusCode(500, new { ErrorCode = "DB01", Message = "An error occurred while adding the student." });
                }

                return Ok(new { Message = "Student added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "SRV01", Message = "Internal server error." });
            }
        }


        [HttpGet("GetFee")]
        public async Task<IActionResult> GetFee()
        {
            try
            {
                var students = await _studentrepository.GetFeeAsync();

                if (students == null)
                {
                    return NotFound(new { ErrorCode = "STU01", Message = "No students found." });
                }

                
                return Ok(students);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorCode = "SRV01", Message = "Internal server error." });
            }
        }
    }
}
