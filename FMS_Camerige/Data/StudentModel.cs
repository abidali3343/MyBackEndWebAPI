using System.ComponentModel.DataAnnotations;

namespace FMS_Camerige.Data
{

    public class StudentModel
    {

        [Required]
        public int RollNumber { get; set; }
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string RelativeNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string StudentBForm { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string FatherCNIC { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        public DateTime DateOfAdmission { get; set; }

        [Required]
        public string Village { get; set; }

        [Required]
        public string Migration { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Fee must be a positive number.")]
        public decimal Fee { get; set; }

        [Required]
        public string Remarks { get; set; }
    }

    public class GetStudentModel
    {
        public int RollNumber { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Class { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public decimal Fee { get; set; }
        public string Address { get; set; }
    }

    public class AddFeeModel
    {
        public string RollNumber { get; set; } 
        public int? MonthlyFee { get; set; }
        public int? ReceivedFee { get; set; } 
        public int? RemainingFee { get; set; } 
        public string FeeMonth { get; set; } 
        public string Class { get; set; } 
        public string ReceivedBy { get; set; }
        public string Remarks { get; set; }
    }


}
