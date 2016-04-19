namespace SCML.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("tblContent")]
    public partial class Content
    {
        public int id { get; set; }

        public int type_id { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Required]
        [StringLength(250)]
        public string summary { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string contents { get; set; }

        [StringLength(250)]
        public string large_image_path { get; set; }

        [StringLength(250)]
        public string thambnail_image_path { get; set; }

        [StringLength(250)]
        public string content_file_path { get; set; }

        public int? sort_order { get; set; }

        [Column(TypeName = "date")]
        public DateTime publish_date { get; set; }

        [Required]
        [StringLength(128)]
        public string publish_by { get; set; }

        public virtual Type Type { get; set; }
    }
}
