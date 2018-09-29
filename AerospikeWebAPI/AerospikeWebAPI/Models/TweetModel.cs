using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AerospikeWebAPI.Models
{
    public class TweetModel
    {
        public long id { get; set; }
        public string text { get; set; }
        public string statusSource { get; set; }
    }
}