using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;
using unirest_net.request;

namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new API();
            var game = new Game(api.GetRightGuess());

            //api.ValidateApiResponse(api.response);

            

            // Validate response does not have multiple words, retry if it does
            // Abstract to method
            // Separate everything out into its own class

            // Check that API response is only a single word.
        }
    }
}
