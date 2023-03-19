using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class ExpandedUserDTO
    {
        [Key]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "**")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Display(Name = "Clave")]
        public string Password { get; set; }
        [Display(Name = "Fecha Fin Bloqueo")]
        public DateTime? LockoutEndDateUtc { get; set; }
        public int AccessFailedCount { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<UserRolesDTO> Roles { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string IdUsuario { get; set; }
        public bool EsUsuarioPrincipal { get; set; }
        public string NombresRoles { get; set; }

        public ExpandedUserDTO() { }
    }

    public class UserRolesDTO
    {
        [Key]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        public UserRolesDTO() { }
    }

    public class UserRoleDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    public class RoleDTO
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Rol")]
        public string RoleName { get; set; }
    }

    public class UserAndRolesDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public List<UserRoleDTO> colUserRoleDTO { get; set; }
    }
}