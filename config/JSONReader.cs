﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TSP_Butler.config
{
    internal class JSONReader
    {
        public string token { get; set; }
        public string prefix { get; set; }
        public string mongoDBConnection { get; set; }

        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("config.json"))
            {
                string json = await sr.ReadToEndAsync();
                JSONStructure data = JsonConvert.DeserializeObject<JSONStructure>(json);

                this.token = data.token;
                this.prefix = data.prefix;
                this.mongoDBConnection = data.mongoDBConnection;
            }
        }
    }
}

internal sealed class JSONStructure
{
    public string token { set; get;  }
    public string prefix { set; get; }
    public string mongoDBConnection { set; get;}
}
