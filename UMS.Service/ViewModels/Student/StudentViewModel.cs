using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Student
{
    public record StudentViewModel(Guid Id, string FullName, string NationalID, int Level, decimal TotalMark, int RegisteredHours, decimal GPA);
}
