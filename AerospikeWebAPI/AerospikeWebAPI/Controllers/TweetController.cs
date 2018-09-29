using Aerospike.Client;
using AerospikeWebAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AerospikeWebAPI.Controllers
{
    public class TweetController : ApiController
    {
        
        string nameSpace = "AirEngine";
        string setName = "swinal";
        
        [HttpPost]
        [Route("api/Tweet")]
        // GET: api/Tweet/5
        public List<TweetModel> GetTweetById([FromBody] long[] tweetId)
        {
            try
            {
                var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                List<TweetModel> Data = new List<TweetModel>();
                foreach (long tweetid in tweetId)
                {
                    Record record = aerospikeClient.Get(new BatchPolicy(), new Key(nameSpace, setName, tweetid.ToString()));
                    TweetModel tweet = new TweetModel();
                    tweet.id = Convert.ToInt64(record.GetValue("id").ToString());
                    tweet.text = record.GetValue("text").ToString();
                    tweet.statusSource = record.GetValue("statusSource").ToString();
                    Data.Add(tweet);
                }
                return Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION IN GETTING ID'S "+ e);
                throw;
            }
        }
        
        [HttpPut]
        [Route("api/Tweet/{tweetId}")]
        // PUT: api/Tweet/5
        public void UpdateTweet(long tweetId, [FromBody] ModifyTweetModel modifyTweet)
        {
            try
            {
                var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                var key = new Key(nameSpace, setName, tweetId.ToString());
                aerospikeClient.Put(new WritePolicy(), key, new Bin[] { new Bin("text", modifyTweet.Value) });
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION IN UPDATE "+ e);
                throw;
            }
        }
        [HttpDelete]
        [Route("api/Tweet/{tweetId}")]
        // DELETE: api/Tweet/5
        public void Delete(long tweetId)
        {
            try
            {
                var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                var key = new Key(nameSpace, setName, tweetId.ToString());
                aerospikeClient.Delete(new WritePolicy(), key);
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION IN DELET "+ e);
                throw;
            }
        }
    }
}
