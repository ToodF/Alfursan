﻿

using System;

namespace Alfursan.Domain
{
    public class AlfursanFile
    {
        public int FileId { get; set; }

        public string OriginalFileName { get; set; }

        public string RelatedFileName { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        public EnumFileType FileType { get; set; }

        public int CustomerUserId { get; set; }

        public int CreatedUserId { get; set; }
        
        public User Customer { get; set; }

        public User CreatedUser { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
