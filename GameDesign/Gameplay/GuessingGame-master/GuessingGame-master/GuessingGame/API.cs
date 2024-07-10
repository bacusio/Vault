using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using unirest_net.http;

namespace GuessingGame
{
    class API
    {
        // { "word":"mountain fern","results":[{"definition":"common European mountain fern having fragrant lemon or balsam scented fronds","partOfSpeech":"noun","synonyms":["dryopteris oreopteris","oreopteris limbosperma"],"typeOf":["fern"],"memberOf":["genus oreopteris","oreopteris"] }],"pronunciation":{"all":"'ma?nt?n_f?rn"}}
        //Console.WriteLine(content);

        // Validate response does not have multiple words, retry if it does
        // Abstract to method
        // Separate everything out into its own class

        public string GetRightGuess()
        {
            var word = "";

            while (!ValidateApiResponse(word))
            {
                var wordData = getWord();
                word = (string)wordData["word"];
            }

            // { "word":"mountain fern","results":[{"definition":"common European mountain fern having fragrant lemon or balsam scented fronds","partOfSpeech":"noun","synonyms":["dryopteris oreopteris","oreopteris limbosperma"],"typeOf":["fern"],"memberOf":["genus oreopteris","oreopteris"] }],"pronunciation":{"all":"'ma?nt?n_f?rn"}}

            return word;
        }

        // Check that API response is only a single word.
        private bool ValidateApiResponse(string word)
        {
            bool isValidResponse = true;

            if(word.Contains(" ") || word.Contains("-") || word == "")
            {
                isValidResponse = false;
            }
            return isValidResponse;
        }

        private JObject getWord()
        {
            var client = new RestClient("https://wordsapiv1.p.rapidapi.com/words");
            client.AddDefaultHeader("X-RapidAPI-Key", "e6d8e3cecamshfb49e6270e0ca4ep1a4eebjsn4ed629c3d865");

            var request = new RestRequest("/?random=true", Method.GET);
            IRestResponse response = client.Execute(request);
            string content = response.Content; //raw content as string

            JObject jsonContent = JObject.Parse(content);

            return jsonContent;
        }
    }
}
