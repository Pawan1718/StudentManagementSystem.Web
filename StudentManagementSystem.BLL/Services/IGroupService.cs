using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentManagementSystem.BLL.Services
{
    public interface IGroupService
    {
        PagedResult<GroupViewModel> GetAll(int pageNo,int pageSize);
        IEnumerable<GroupViewModel> GetAllGroups();
        GroupViewModel GetGroup(int id);
        GroupViewModel AddGroup(GroupViewModel group);
    }
}
