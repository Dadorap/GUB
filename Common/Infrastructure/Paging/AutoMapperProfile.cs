using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Common.Infrastructure.Paging
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Källa => Mål
            // CreateEmployeeViewModel => Employee
            //CreateMap<CreateEmployeeViewModel, Employee>()
                //.ReverseMap();
        }
    }
}
