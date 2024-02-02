using Newtonsoft.Json;

namespace vscs2.DTO
{
    public class VisitorModel
    {
        //class fields /properties

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }


        [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string? Address { get; set; }

        [JsonProperty(PropertyName = "mono", NullValueHandling = NullValueHandling.Ignore)]
        public int MoNo { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string? Email { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
   

   
    }
}
