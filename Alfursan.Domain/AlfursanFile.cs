

namespace Alfursan.Domain
{
    public class AlfursanFile
    {
        public int FileId { get; set; }
        public string OriginalFileName { get; set; }
        public string RelatedFileName { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int OwnerUserId { get; set; }
        public int CreatedUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
