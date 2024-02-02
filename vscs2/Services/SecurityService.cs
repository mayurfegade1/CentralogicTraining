using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using vscs2.CosmosDBServices;
using vscs2.Entities;
using vscs2.Interfaces;

namespace vscs2.Services
{
    public class SecurityService:ISecurityservice
    {
        public ICosmosService _cosmosService;

        public SecurityService(ICosmosService cosmosService) {

            _cosmosService = cosmosService;

        }
        public async Task<CompanyUsers> LoginSecurityUser(string Email, string Password)
        {

            return await _cosmosService.LoginSecurityUser(Email, Password);
        }


        public async Task<Request> SearchVisitorByMono(int Search)
        {
            return await _cosmosService.SearchVisitorByMono(Search);
        }



       
    }
}
