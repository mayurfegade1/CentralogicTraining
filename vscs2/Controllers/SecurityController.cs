using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using vscs2.DTO;
using vscs2.Entities;
using vscs2.Interfaces;
using vscs2.Services;

namespace vscs2.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public Container container;
        public IMapper _mapper;
        public ISecurityservice _securityService;
        public SecurityController(ISecurityservice securityService, IMapper mapper)
        {
            _securityService = securityService;
            container = GetContainer();
            _mapper = mapper;
        }
        //LoginSecurity
        [HttpGet]
        public async Task<IActionResult> LoginSecurityUser(string Email, string Password)
        {
            try
            {


                var securityuser= await _securityService.LoginSecurityUser(Email, Password);
                if (securityuser == null)
                {
                    return NotFound("wrong email or password");
                }

                var response = new { securityuser.Name };


                return Ok($"{securityuser.Name} Login Successfully{response}");


            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }
        // search visitor
        [HttpGet]
        public async Task<IActionResult> SearchVisitorByMono(int Search)
        {
            try
            {
                

             
                // get visitor details from security service
                var visitor = await _securityService.SearchVisitorByMono(Search);
                if (visitor == null)
                {
                    return NotFound("request is not accept by oficer ");
                }

                //stp 2 mapping all data
                RequestModel model = new RequestModel();
                model.UId = visitor.UId;

                model.Name = visitor.Name;
               
                model.MoNo = visitor.MoNo;
                model.Email = visitor.Email;
                model.Subject = visitor.Subject;
                model.Description = visitor.Description;
                model.TrackingId = visitor.TrackingId;
                model.Action = visitor.Action;
                model.officerEmail = visitor.officerEmail;


                //Console.WriteLine(subject);
                return Ok(model);
                
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }



        }
        private Container GetContainer()
        {
            string? URI = Environment.GetEnvironmentVariable("cosmos-uri");
            string? PrimaryKey = Environment.GetEnvironmentVariable("auth-token");
            string? DatabaseName = Environment.GetEnvironmentVariable("database-name");
            string? ContainerName = Environment.GetEnvironmentVariable("container-name");
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database db = cosmosClient.GetDatabase(DatabaseName);
            Container container = db.GetContainer(ContainerName);
            return container;
        }
    }
}
