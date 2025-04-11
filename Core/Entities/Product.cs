using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//[Table("Products" , Schema ="Sale") ]
public class Product
{

    [Key]
    public int Id { get; set; }
    [MaxLength(128), Required]
    public string ProductName { get; set; }
    public long Price { get; set; }
    public byte[] Thumbnail { get; set; }

    public string ThumbnailFileName { get; set; }
    public long FileSize { get; set; }
    public string ThumbnailFileExtension { get; set; }


}