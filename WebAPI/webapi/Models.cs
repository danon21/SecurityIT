using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI
{
    [Table("users")]
    public class User
    {
        [Column("usr_id")]
        public int Id { get; set; }
        [Column("usr_login")]
        public string Login { get; set; } = "";
        [Column("usr_pass")]
        public string Password { get; set; } = "";

        public List<Right> Rights { get; set; } = new();
        public List<UserRight> UserRights { get; set; } = new();
    }

    [Table("rights")]
    public class Right
    {
        [Column("rgh_id")]
        public int Id { get; set; }
        [Column("rgh_name")]
        public string TypeRight { get; set; } = "";

        public List<User> Users { get; set; } = new();
        public List<UserRight> UserRights { get; set; } = new();
    }

    [Table("usr_rights")]
    public class UserRight
    {
        [Column("usr_usr_id")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column("rgh_rgh_id")]
        public int RightId { get; set; }
        public Right? Right { get; set; }

    }

    [Table("data")]
    public class Data
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("dt_id")]
        public int Id { get; set; }
        [Column("dt_value")]
        public string Value { get; set; } = "";
    }

    public class ValueData
    {
        public string val { get; set; } = "";
    }
}