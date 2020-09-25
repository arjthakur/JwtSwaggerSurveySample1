using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.Entities.Standards
{
    interface IBaseProperties : IPrimaryKey
    {
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
