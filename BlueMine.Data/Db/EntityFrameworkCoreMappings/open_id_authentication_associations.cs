using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class open_id_authentication_associations
    {
        public int Id { get; set; }
        public int? Issued { get; set; }
        public int? Lifetime { get; set; }
        public string Handle { get; set; }
        public string AssocType { get; set; }
        public byte[] ServerUrl { get; set; }
        public byte[] Secret { get; set; }
    }
}
