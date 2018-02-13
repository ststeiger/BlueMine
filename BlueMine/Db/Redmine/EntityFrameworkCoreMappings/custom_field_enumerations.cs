using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class custom_field_enumerations
    {
        public int Id { get; set; }
        public int CustomFieldId { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public int Position { get; set; }
    }
}
