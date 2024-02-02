using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace vscs2.Entities
{
    
        public class Users
        {    // Madetory Fields
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }

            [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
            public string UId { get; set; }

            [JsonProperty(PropertyName = "dType", NullValueHandling = NullValueHandling.Ignore)]
            public string DocumentType { get; set; }


            [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
            public string CreatedBy { get; set; }

            [JsonProperty(PropertyName = "createdByName", NullValueHandling = NullValueHandling.Ignore)]
            public string CreatedByName { get; set; }

            [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime CreatedOn { get; set; }

            [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
            public string UpdatedBy { get; set; }

            [JsonProperty(PropertyName = "updatedByName", NullValueHandling = NullValueHandling.Ignore)]
            public string UpdatedByName { get; set; }

            [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime UpdatedOn { get; set; }

            [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
            public int Version { get; set; }

            [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
            public bool Active { get; set; }

            [JsonProperty(PropertyName = "archieved", NullValueHandling = NullValueHandling.Ignore)]
            public bool Archieved { get; set; }

      //add
      [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        



        [JsonProperty(PropertyName = "mono", NullValueHandling = NullValueHandling.Ignore)]
        public int MoNo { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

    }

        public class CompanyUsers : Users
        {

           /* [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }*/

            [JsonProperty(PropertyName = "userType", NullValueHandling = NullValueHandling.Ignore)]
            public string UserType { get; set; }


/*
            [JsonProperty(PropertyName = "mono", NullValueHandling = NullValueHandling.Ignore)]
            public int MoNo { get; set; }

            [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
            public string Email { get; set; }*/

            [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
            public string Password { get; set; }
        }

        public class Visitor : Users
        {
/*
            [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

          

        

        [JsonProperty(PropertyName = "mono", NullValueHandling = NullValueHandling.Ignore)]
            public int MoNo { get; set; }*/

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string? Address { get; set; }

       /* [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
            public string Email { get; set; }*/

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }


        /* [JsonProperty(PropertyName = "action", NullValueHandling = NullValueHandling.Ignore)]
                 public bool Action {  get; set; }
     */
       /* [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }*/

      /*  [JsonProperty(PropertyName = "action", NullValueHandling = NullValueHandling.Ignore)]// for request   true means approve request.
        public bool Action { get; set; }

        [JsonProperty(PropertyName = "trackingId", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string TrackingId { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string Description { get; set; }

        [JsonProperty(PropertyName = "officerEmail", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string officerEmail { get; set; }

        [JsonProperty(PropertyName = "subject", NullValueHandling = NullValueHandling.Ignore)]// for request and emal
        public string Subject { get; set; }
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string Body { get; set; }
*/
       
    }


    public class Request : Users
    {/*
        [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "mono", NullValueHandling = NullValueHandling.Ignore)]
        public int MoNo { get; set; }


        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
*/
        [JsonProperty(PropertyName = "action", NullValueHandling = NullValueHandling.Ignore)]// for request   true means approve request.
        public bool Action { get; set; }

        [JsonProperty(PropertyName = "trackingId", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string TrackingId { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string Description { get; set; }

        [JsonProperty(PropertyName = "officerEmail", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string officerEmail { get; set; }

        [JsonProperty(PropertyName = "subject", NullValueHandling = NullValueHandling.Ignore)]// for request and emal
        public string Subject { get; set; }
      /*  [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]// for request
        public string Body { get; set; }*/
    }
   /* public enum UserType
    {
        //[EnumMember(Value = "office")]
       Office,
       // [EnumMember(Value = "security")]
        Security=2,

      //  [EnumMember(Value = "manager")]
        Manager = 3
    }*/

  //UserType usertype= (UserType)Enum.Parse(typeof(UserType));
}

