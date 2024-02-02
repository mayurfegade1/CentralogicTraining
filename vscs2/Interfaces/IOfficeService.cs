using vscs2.Entities;

namespace vscs2.Interfaces
{
    public interface IOfficeService
    {
        Task<CompanyUsers> LoginOfficeUser(string Email, string Password);
        Task<Request> ApproveRejectRequest(string Trackingid, bool action_true_or_false);

        Task<List<Request>> SeeAllPendingrequests();
        Task<List<Request>> SeeAllApproverequests();
        Task<List<Request>> SeeAllRejectedrequests();
    }
}
