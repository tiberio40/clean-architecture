using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Scaffolding
{
    [Table("Files", Schema = "Scaffolding")]
    public class FileEntity
    {
        public FileEntity()
        {
            ConfigThemeEntities = new HashSet<ConfigThemeEntity>();
        }

        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string UrlFile { get; set; } = null!;
        public int TypeFile { get; set; }

        public IEnumerable<ConfigThemeEntity> ConfigThemeEntities { get; set; }
    }
}
