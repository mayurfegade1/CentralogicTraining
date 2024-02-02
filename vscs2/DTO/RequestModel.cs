using Newtonsoft.Json;

namespace vscs2.DTO
{
    public class RequestModel
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }


        [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]  
        public string? Name { get; set; }


        [JsonProperty(PropertyName = "mono", NullValueHandling = NullValueHandling.Ignore)]
        public int MoNo { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string? Email { get; set; }

        [JsonProperty(PropertyName = "action", NullValueHandling = NullValueHandling.Ignore)]// for request   true means approve request. false means reject
        public bool Action { get; set; }

        [JsonProperty(PropertyName = "trackingId", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string TrackingId { get; set; }

        

        [JsonProperty(PropertyName = "officerEmail", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string officerEmail { get; set; }

        [JsonProperty(PropertyName = "subject", NullValueHandling = NullValueHandling.Ignore)]// for request and emal
        public string Subject { get; set; }
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string Description { get; set; }
        /* [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]// for request
         public string Body { get; set; }*/
    }
}
