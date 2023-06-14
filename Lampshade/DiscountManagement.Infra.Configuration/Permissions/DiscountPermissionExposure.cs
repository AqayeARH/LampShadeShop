using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0.Framework.Infrastructure;

namespace DiscountManagement.Infra.Configuration.Permissions
{
    public class DiscountPermissionExposure:IPermissionExposure
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "CustomerDiscounts", new List<PermissionDto>()
                    {
                        new PermissionDto(DiscountPermissionCode.ListCustomerDiscounts, "ListCustomerDiscounts"),
                        new PermissionDto(DiscountPermissionCode.DefineCustomerDiscount, "DefineCustomerDiscounts"),
                        new PermissionDto(DiscountPermissionCode.EditCustomerDiscount, "EditCustomerDiscounts"),
                    }
                },
                {
                    "ColleaguesDiscounts", new List<PermissionDto>()
                    {
                        new PermissionDto(DiscountPermissionCode.ListColleaguesDiscounts, "ListColleaguesDiscounts"),
                        new PermissionDto(DiscountPermissionCode.DefineColleaguesDiscount, "DefineColleaguesDiscounts"),
                        new PermissionDto(DiscountPermissionCode.EditColleaguesDiscount, "EditColleaguesDiscounts"),
                        new PermissionDto(DiscountPermissionCode.ChangeStatusColleaguesDiscount, "ChangeStatusColleaguesDiscount"),
                    }
                },
            };
        }
    }
}
