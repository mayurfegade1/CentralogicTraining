using Microsoft.AspNetCore.Mvc;
using vscs2.DTO;
using vscs2.Entities;

namespace vscs2.Interfaces
{
    public interface IManagerService
    {
        Task<CompanyUsers> CompanyUserRegistration(CompanyUsers comapnyuser);

        Task<List<Request>> SeeAllPendingrequests();
        Task<List<Request>> SeeAllApproverequests();
        Task<List<Request>> SeeAllRejectedrequests();
        Task<CompanyUsers> LoginManager(string Email, string Password);

        Task<Request> ApproveRejectRequest(string manageremail, string Trackingid, bool Aprrove_Reject);
        Task<CompanyUsers> UpdateCompanyUsers(CompanyUsersModel companyuserModel);

        Task<CompanyUsers> UpdateUsers(CompanyUsers comapnyuser);

        Task<CompanyUsers> DeleteCompanyUsers(string CompannyUserUId, string Usertype);
    }
}
