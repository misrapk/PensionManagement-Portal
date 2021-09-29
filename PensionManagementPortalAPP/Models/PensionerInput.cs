using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortalAPP.Models
{
	public class PensionerInput
	{
        [Required]
        [DisplayName("Pensioner Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please provide valid date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Only Numbers and Alphabets acceptable")]
        [StringLength(10)]
        public string PAN { get; set; }

        [Required]
        [RegularExpression("^[0-9]{12}$", ErrorMessage = "Should be combination of 12-digits ONLY")]
        [DisplayName("Aadhar Number")]
        public string AadharNumber { get; set; }
        [Required]
        public PensionTypeValue PensionType { get; set; }
    }
    public enum PensionTypeValue
    {
        Self = 1,
        Family = 2
    }
}
