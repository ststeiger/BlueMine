using System;
using System.Collections.Generic;

namespace BlueMine.Redmine
{
    public partial class attachments
    {
        public int Id { get; set; }
        public int? ContainerId { get; set; }
        public string ContainerType { get; set; }
        public string Filename { get; set; }
        public string DiskFilename { get; set; }
        public long Filesize { get; set; }
        public string ContentType { get; set; }
        public string Digest { get; set; }
        public int Downloads { get; set; }
        public int AuthorId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Description { get; set; }
        public string DiskDirectory { get; set; }
    }
}
