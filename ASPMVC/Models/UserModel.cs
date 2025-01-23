namespace ASPMVC.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string HashContrasena { get; set; }
        public string SaltContrasena { get; set; }

        public UserModel(int Id, string Usuario, string HashContrasena, string SaltContrasena)
        {
            this.Id = Id;
            this.Usuario = Usuario;
            this.HashContrasena = HashContrasena;
            this.SaltContrasena = SaltContrasena;
        }

    }
}
