using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class custom_values
    {
        public int Id { get; set; }
        public string CustomizedType { get; set; }
        public int CustomizedId { get; set; }
        public int CustomFieldId { get; set; }
        public string Value { get; set; }
    }
}
