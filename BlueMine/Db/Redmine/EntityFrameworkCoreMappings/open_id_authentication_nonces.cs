using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class open_id_authentication_nonces
    {
        public int Id { get; set; }
        public int Timestamp { get; set; }
        public string ServerUrl { get; set; }
        public string Salt { get; set; }
    }
}
