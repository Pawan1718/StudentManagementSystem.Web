using StudentManagementSystem.Data.Entities;
using StudentManagementSystem.Data.UnitOfWorks;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentManagementSystem.BLL.Services
{
    public class GroupService : IGroupService
    {
        private  IUnitOfWork _unitOfWork;
        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public GroupViewModel AddGroup(GroupViewModel groupVM)
        {
            try
            {
                var model = groupVM.ConvertToGroup(groupVM);
                _unitOfWork.GenericRepository<Groups>().Add(model);
                _unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
            return groupVM;
        }
        public PagedResult<GroupViewModel> GetAll(int pageNo, int pageSize)
        {
            try
            {
                int excludeRecords = (pageSize * pageNo) - pageSize;
                List<GroupViewModel> groupViewModel = new List<GroupViewModel>();
                var groupList = _unitOfWork.GenericRepository<Groups>()
                    .GetAll().Skip(excludeRecords).Take(pageSize).ToList();

                groupViewModel = ListInfo(groupList);
                var pagedResult = new PagedResult<GroupViewModel>
                {
                    Data = groupViewModel,
                    TotalItems = _unitOfWork.GenericRepository<Groups>()
                    .GetAll().Count(),
                    PageSize = pageSize
                };
                return pagedResult;
            }
            catch (Exception)
            {

                throw;
            }


        }
        private List<GroupViewModel> ListInfo(List<Groups> groupList)
        {
            return groupList.Select(x => new GroupViewModel(x)).ToList();

        }
        public IEnumerable<GroupViewModel> GetAllGroups()
        {
            try
            {
                List<GroupViewModel> groupViewModel = new List<GroupViewModel>();
                var groupList = _unitOfWork.GenericRepository<Groups>()
                    .GetAll().ToList();
                groupViewModel = ListInfo(groupList);
                return groupViewModel;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public GroupViewModel GetGroup(int id)
        {
            var groups = _unitOfWork.GenericRepository<Groups>().GetById(id);
            var vm=new GroupViewModel(groups);
            return vm;
        }

    }
}
