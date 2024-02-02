using Microsoft.AspNetCore.Mvc;
using vscs2.Entities;

namespace vscs2.Interfaces
{
    public interface ISecurityservice
    {

        Task<CompanyUsers> LoginSecurityUser(string Email, string Password);

        Task<Request> SearchVisitorByMono(int Search);
    }
}
