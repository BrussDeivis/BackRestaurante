
#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Areas.Administracion.Controllers;


#endregion Includes

namespace Tsp.Sigescom.WebApplication.Controllers

{
    public class AdminController : ActorBaseController
    {
        //private readonly IActorNegocioLogica _logica;
        public AdminController()
        {
            //_logica = Dependencia.Resolve<IActorNegocioLogica>();
        }

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        // Controllers

        // GET: /Admin/
        [Authorize(Roles = "AdministradorTI")]
        #region public ActionResult Index(string searchStringUserNameOrEmail)
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Index(string searchStringUserNameOrEmail, string currentFilter, int? page)
        //{
        //    try
        //    {
        //        int intPage = 1;
        //        int intPageSize = 5;
        //        int intTotalPageCount = 0;

        //        if (searchStringUserNameOrEmail != null)
        //        {
        //            intPage = 1;
        //        }
        //        else
        //        {
        //            if (currentFilter != null)
        //            {
        //                searchStringUserNameOrEmail = currentFilter;
        //                intPage = page ?? 1;
        //            }
        //            else
        //            {
        //                searchStringUserNameOrEmail = "";
        //                intPage = page ?? 1;
        //            }
        //        }

        //        ViewBag.CurrentFilter = searchStringUserNameOrEmail;

        //        List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();
        //        int intSkip = (intPage - 1) * intPageSize;

        //        intTotalPageCount = UserManager.Users
        //            .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
        //            .Count();

        //        var result = UserManager.Users
        //            .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
        //            .OrderBy(x => x.UserName)
        //            .Skip(intSkip)
        //            .Take(intPageSize)
        //            .ToList();

        //        foreach (var item in result)
        //        {
        //            ExpandedUserDTO objUserDTO = new ExpandedUserDTO();

        //            objUserDTO.UserName = item.UserName;
        //            objUserDTO.Email = item.Email;
        //            objUserDTO.LockoutEndDateUtc = item.LockoutEndDateUtc;

        //            col_UserDTO.Add(objUserDTO);
        //        }

        //        // Set the number of pages
        //        var _UserDTOAsIPagedList =
        //            new StaticPagedList<ExpandedUserDTO>
        //            (
        //                col_UserDTO, intPage, intPageSize, intTotalPageCount
        //                );

        //        return View(_UserDTOAsIPagedList);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, "Error: " + ex);
        //        List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();

        //        return View(col_UserDTO.ToPagedList(1, 25));
        //    }
        //}
        public JsonResult ListarUsuarios()
        {
            try
            {
                List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();

                var result = UserManager.Users.ToList();

                foreach (var item in result)
                {
                    ExpandedUserDTO objUserDTO = new ExpandedUserDTO();

                    objUserDTO.IdUsuario = item.Id;
                    objUserDTO.UserName = item.UserName;
                    objUserDTO.Email = item.Email;
                    objUserDTO.LockoutEndDateUtc = item.LockoutEndDateUtc;
                    objUserDTO.EsUsuarioPrincipal = item.UserName.ToLower() != this.User.Identity.Name.ToLower() ? true : false;
                    objUserDTO.NombresRoles = ObtenerNombreDeLosRoles(item.Roles);
                    col_UserDTO.Add(objUserDTO);


                }

                return Json(col_UserDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();

                return Json(col_UserDTO);
            }
        }

        public string ObtenerNombreDeLosRoles(IEnumerable<IdentityUserRole> ListOfRoleIds)
        {
            List<string> ListOfRoleNames = new List<string>();

  
            foreach (var item in ListOfRoleIds)
            {
                string rolename = RoleManager.FindById(item.RoleId).Name;
                ListOfRoleNames.Add(rolename);
            }

            string nombresRoles = string.Join(", ", ListOfRoleNames.ToArray());

            return nombresRoles;

        }

        #endregion

        // Users *****************************

        // GET: /Admin/Edit/Create 
        [Authorize(Roles = "AdministradorTI")]
        #region public ActionResult Create()
        public ActionResult Create()
        {
            //ExpandedUserDTO objExpandedUserDTO = new ExpandedUserDTO();
            //ViewBag.Roles = GetAllRolesAsSelectList();
            return View(/*objExpandedUserDTO*/);
        }
        #endregion

        // PUT: /Admin/Create
        //[Authorize(Roles = "Administrador")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult GuardarUsuario(ExpandedUserDTO paramExpandedUserDTO)
        {
            try
            {
                var Email = paramExpandedUserDTO.Email.Trim();
                var UserName = paramExpandedUserDTO.Email.Trim();
                var Password = paramExpandedUserDTO.Password.Trim();

                if (Email == "")
                {
                    throw new Exception("No Email");
                }

                if (Password == "")
                {
                    throw new Exception("No Password");
                }

                // UserName is LowerCase of the Email
                UserName = Email.ToLower();

                // Create user

                var objNewAdminUser = new ApplicationUser { UserName = UserName, Email = Email };
                var AdminUserCreateResult = UserManager.Create(objNewAdminUser, Password);

                if (AdminUserCreateResult.Succeeded)
                {
                    string strNewRole = paramExpandedUserDTO.Roles.First().RoleName;

                    if (strNewRole != "0")
                    {
                        UserManager.AddToRole(objNewAdminUser.Id, strNewRole);
                    }
                    return new JsonHttpStatusResult(new { code_result = OperationResultEnum.Success, data = "OK", result_description = "Se registro con exito el Usuario: " + Email }, HttpStatusCode.OK);
                }
                else
                {
                    return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al registrar el Usuario:\n " + Email + ":\n " + AdminUserCreateResult.Errors.First().ToString())), HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR REGISTRAR EL USUARIO", e)), HttpStatusCode.InternalServerError);
            }
        }
        public OperationResult GuardarUsuario_(ExpandedUserDTO paramExpandedUserDTO)
        {
            try
            {
                var Email = paramExpandedUserDTO.Email.Trim();
                var UserName = paramExpandedUserDTO.Email.Trim();
                var Password = paramExpandedUserDTO.Password.Trim();

                if (Email == "")
                {
                    throw new Exception("No Email");
                }

                if (Password == "")
                {
                    throw new Exception("No Password");
                }

                // UserName is LowerCase of the Email
                UserName = Email.ToLower();

                // Create user

                var objNewAdminUser = new ApplicationUser { UserName = UserName, Email = Email };
                var AdminUserCreateResult = UserManager.Create(objNewAdminUser, Password);

                if (AdminUserCreateResult.Succeeded == true)
                {
                    foreach(var item in paramExpandedUserDTO.Roles)
                    {
                        UserManager.AddToRole(objNewAdminUser.Id, item.RoleName);
                    }
                }

                return new OperationResult(objNewAdminUser.Id,OperationResultEnum.Success, "Se registro con exito el Usuario:" + Email);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }


        // GET: /Admin/Edit/TestUser 
        [Authorize(Roles = "AdministradorTI")]
        #region public ActionResult EditUser(string UserName)
        public ActionResult EditUser(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);
            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }
            return View(objExpandedUserDTO);
        }
        #endregion

        // PUT: /Admin/EditUser
        [Authorize(Roles = "AdministradorTI")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditUser(ExpandedUserDTO paramExpandedUserDTO)
        public ActionResult EditUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            try
            {
                if (paramExpandedUserDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ExpandedUserDTO objExpandedUserDTO = UpdateDTOUser(paramExpandedUserDTO);
                //OperationResult result = _logica.actualizarCorreoActorConIdUsuario(paramExpandedUserDTO.idUsuario, paramExpandedUserDTO.Email);

                //if (result.code_result == OperationResultEnum.Error)
                //{
                //    throw new Exception("No se pudo editar el usuario", result.exceptions.Count > 0 ? result.exceptions.First() : null);
                //}
                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditUser", GetUser(paramExpandedUserDTO.UserName));
            }
        }
        public OperationResult EditarUsuario(ExpandedUserDTO paramExpandedUserDTO)
        {
            try
            {
                ExpandedUserDTO objExpandedUserDTO = UpdateDTOUser(paramExpandedUserDTO);
                ApplicationUser user = UserManager.FindByName(paramExpandedUserDTO.UserName);

                foreach (var item in paramExpandedUserDTO.Roles)
                {
                    UserManager.AddToRole(user.Id, item.RoleName);
                }

                return new OperationResult(OperationResultEnum.Success, "Se actualizaron con exito los datos el Usuario:" + paramExpandedUserDTO.Email);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        #endregion

        // DELETE: /Admin/DeleteUser
        [Authorize(Roles = "AdministradorTI")]
        #region public ActionResult DeleteUser(string UserName)
        public JsonResult DeleteUser(string UserName)
        {
            try
            {
                if (UserName.ToLower() == this.User.Identity.Name.ToLower())
                {
                    throw new Exception("Error: No se puede eliminar el usuario actual");
                }

                ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                else
                {
                    DeleteUser(objExpandedUserDTO);
                }

                return Json(Util.SuccessJson("Se elimino el usuario"));
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(ex));
                //ModelState.AddModelError(string.Empty, "Error: " + ex);
                //return View("EditUser", GetUser(UserName));
            }
        }

        #endregion

        // GET: /Admin/EditRoles/TestUser 
        [Authorize(Roles = "AdministradorTI")]
        #region ActionResult EditRoles(string UserName)
        public ActionResult EditRoles(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserName = UserName.ToLower();

            // Check that we have an actual user
            ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

            if (objExpandedUserDTO == null)
            {
                return HttpNotFound();
            }

            UserAndRolesDTO objUserAndRolesDTO =
                GetUserAndRoles(UserName);

            return View(objUserAndRolesDTO);
        }
        #endregion

        // PUT: /Admin/EditRoles/TestUser 
        [Authorize(Roles = "AdministradorTI")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO)
        public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO)
        {
            try
            {
                if (paramUserAndRolesDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                string UserName = paramUserAndRolesDTO.UserName;
                string strNewRole = Convert.ToString(Request.Form["AddRole"]);

                if (strNewRole != "No Roles Found")
                {
                    // Go get the User
                    ApplicationUser user = UserManager.FindByName(UserName);

                    // Put user in role
                    UserManager.AddToRole(user.Id, strNewRole);
                }

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                UserAndRolesDTO objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

                return View(objUserAndRolesDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditRoles");
            }
        }
        #endregion

        // DELETE: /Admin/DeleteRole?UserName="TestUser&RoleName=Administrador
        [Authorize(Roles = "AdministradorTI")]
        #region public ActionResult DeleteRole(string UserName, string RoleName)
        public ActionResult DeleteRole(string UserName, string RoleName)
        {
            try
            {
                if ((UserName == null) || (RoleName == null))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                UserName = UserName.ToLower();

                // Check that we have an actual user
                ExpandedUserDTO objExpandedUserDTO = GetUser(UserName);

                if (objExpandedUserDTO == null)
                {
                    return HttpNotFound();
                }

                if (UserName.ToLower() ==
                    this.User.Identity.Name.ToLower() && RoleName == "Administrador")
                {
                    ModelState.AddModelError(string.Empty,
                        "Error: Cannot delete Administrador Role for the current user");
                }

                // Go get the User
                ApplicationUser user = UserManager.FindByName(UserName);
                // Remove User from role
                UserManager.RemoveFromRoles(user.Id, RoleName);
                UserManager.Update(user);

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                return RedirectToAction("EditRoles", new { UserName = UserName });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);

                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

                UserAndRolesDTO objUserAndRolesDTO =
                    GetUserAndRoles(UserName);

                return View("EditRoles", objUserAndRolesDTO);
            }
        }
        #endregion

        // Roles *****************************

        // GET: /Admin/ViewAllRoles
        [Authorize(Roles = "AdministradorTI")]
        #region public ActionResult ViewAllRoles()
        public ActionResult ViewAllRoles()
        {
            return View();
        }
        public JsonResult ListarRoles()
        {
            try
            { 
                var roleManager =
                    new RoleManager<IdentityRole>
                    (
                        new RoleStore<IdentityRole>(new ApplicationDbContext())
                        );

                List<RoleDTO> colRoleDTO = (from objRole in roleManager.Roles
                                            select new RoleDTO
                                            {
                                                Id = objRole.Id,
                                                RoleName = objRole.Name
                                            }).ToList();
                return Json(colRoleDTO);
            }
            catch (Exception ex)
            {
                List<RoleDTO> colRoleDTO = new List<RoleDTO>();

                return Json(colRoleDTO);
            }
        }
        #endregion

            // GET: /Admin/AddRole
        [Authorize(Roles = "AdministradorTI")]
        #region public ActionResult AddRole()
        public ActionResult AddRole()
        {
            //RoleDTO objRoleDTO = new RoleDTO();

            return View(/*objRoleDTO*/);
        }
        #endregion

        // PUT: /Admin/AddRole
        [Authorize(Roles = "AdministradorTI")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        #region public ActionResult AddRole(RoleDTO paramRoleDTO)
        public JsonResult GuardarRol(RoleDTO paramRoleDTO)
        {
            try
            {
                var RoleName = paramRoleDTO.RoleName.Trim();

                if (RoleName == "")
                {
                    throw new Exception("No RoleName");
                }

                // Create Role
                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext())
                        );

                if (!roleManager.RoleExists(RoleName))
                {
                    roleManager.Create(new IdentityRole(RoleName));
                }

                return Json(Util.SuccessJson("Se registro con exito el Rol:" + RoleName));

            }

            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        #endregion

        // DELETE: /Admin/DeleteUserRole?RoleName=TestRole
        [Authorize(Roles = "AdministradorTI")]
        #region public ActionResult DeleteUserRole(string RoleName)
        public JsonResult DeleteUserRole(string RoleName)
        {
            try
            {
                if (RoleName.ToLower() == "Administrador")
                {
                    throw new Exception(String.Format("No se puede borrar el rol {0}.", RoleName));
                }

                var roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(new ApplicationDbContext()));

                var UsersInRole = roleManager.FindByName(RoleName).Users.Count();
                if (UsersInRole > 0)
                {
                    throw new Exception(
                        String.Format(
                            "No se puede borrar el rol {0} porque tiene usuarios.",
                            RoleName)
                            );
                }

                var objRoleToDelete = (from objRole in roleManager.Roles
                                       where objRole.Name == RoleName
                                       select objRole).FirstOrDefault();
                if (objRoleToDelete != null)
                {
                    roleManager.Delete(objRoleToDelete);
                }
                else
                {
                    throw new Exception(
                        String.Format(
                            "No se puede borrar el rol {0} porque no existe.",
                            RoleName)
                            );
                }

                return Json(Util.SuccessJson("Se elimino el Rol"));
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(ex));
            }
        }
       
        #endregion


        // Utility

        #region public ApplicationUserManager UserManager
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        #region public ApplicationRoleManager RoleManager
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        #region private List<SelectListItem> GetAllRolesAsSelectList()
        public JsonResult GetAllRolesAsSelectList()
        {
            List<SelectListItem> SelectRoleListItems =
                new List<SelectListItem>();

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var colRoleSelectList = roleManager.Roles.OrderBy(x => x.Name).ToList();

            //SelectRoleListItems.Add(
            //    new SelectListItem
            //    {
            //        Text = "Seleccionar",
            //        Value = "0"
            //    });

            foreach (var item in colRoleSelectList)
            {
                SelectRoleListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Name.ToString(),
                        Value = item.Name.ToString(),
                    });
            }

            return Json(SelectRoleListItems);
        }
        //private List<SelectListItem> GetAllRolesAsSelectList()
        //{
        //    List<SelectListItem> SelectRoleListItems =
        //        new List<SelectListItem>();

        //    var roleManager =
        //        new RoleManager<IdentityRole>(
        //            new RoleStore<IdentityRole>(new ApplicationDbContext()));

        //    var colRoleSelectList = roleManager.Roles.OrderBy(x => x.Name).ToList();

        //    SelectRoleListItems.Add(
        //        new SelectListItem
        //        {
        //            Text = "Seleccionar",
        //            Value = "0"
        //        });

        //    foreach (var item in colRoleSelectList)
        //    {
        //        SelectRoleListItems.Add(
        //            new SelectListItem
        //            {
        //                Text = item.Name.ToString(),
        //                Value = item.Name.ToString(),
        //                //Selected = item.Name.ToString() == Properties.Settings.Default.rolFuncionario
        //            });
        //    }

        //    return SelectRoleListItems;
        //}
        #endregion

        #region private ExpandedUserDTO GetUser(string paramUserName)
        private ExpandedUserDTO GetUser(string paramUserName)
        {
            ExpandedUserDTO objExpandedUserDTO = new ExpandedUserDTO();

            var result = UserManager.FindByName(paramUserName);

            // If we could not find the user, throw an exception
            if (result == null) throw new Exception("Could not find the User");

            objExpandedUserDTO.UserName = result.UserName;
            objExpandedUserDTO.Email = result.Email;
            objExpandedUserDTO.LockoutEndDateUtc = result.LockoutEndDateUtc;
            objExpandedUserDTO.AccessFailedCount = result.AccessFailedCount;
            objExpandedUserDTO.PhoneNumber = result.PhoneNumber;

            return objExpandedUserDTO;
        }
        #endregion

        #region private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO objExpandedUserDTO)
        private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            ApplicationUser result =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

            // If we could not find the user, throw an exception
            if (result == null)
            {
                throw new Exception("Could not find the User");
            }

            result.Email = paramExpandedUserDTO.Email;
            result.UserName = paramExpandedUserDTO.Email;

            // Lets check if the account needs to be unlocked
            if (UserManager.IsLockedOut(result.Id))
            {
                // Unlock user
                UserManager.ResetAccessFailedCountAsync(result.Id);
            }

            UserManager.Update(result);

            // Was a password sent across?
            if (!string.IsNullOrEmpty(paramExpandedUserDTO.Password))
            {
                // Remove current password
                var removePassword = UserManager.RemovePassword(result.Id);
                if (removePassword.Succeeded)
                {
                    // Add new password
                    var AddPassword =
                        UserManager.AddPassword(
                            result.Id,
                            paramExpandedUserDTO.Password
                            );

                    if (AddPassword.Errors.Count() > 0)
                    {
                        throw new Exception(AddPassword.Errors.FirstOrDefault());
                    }
                }
            }
            paramExpandedUserDTO.IdUsuario = result.Id;

            return paramExpandedUserDTO;
        }
        #endregion

        #region private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
        private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
        {
            ApplicationUser user =
                UserManager.FindByName(paramExpandedUserDTO.UserName);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("Could not find the User");
            }

            UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(user.Id).ToArray());
            UserManager.Update(user);
            UserManager.Delete(user);
        }
        #endregion

        #region private UserAndRolesDTO GetUserAndRoles(string UserName)
        private UserAndRolesDTO GetUserAndRoles(string UserName)
        {
            // Go get the User
            ApplicationUser user = UserManager.FindByName(UserName);

            List<UserRoleDTO> colUserRoleDTO =
                (from objRole in UserManager.GetRoles(user.Id)
                 select new UserRoleDTO
                 {
                     RoleName = objRole,
                     UserName = UserName
                 }).ToList();

            if (colUserRoleDTO.Count() == 0)
            {
                colUserRoleDTO.Add(new UserRoleDTO { RoleName = "No hay Roles" });
            }

            ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));

            // Create UserRolesAndPermissionsDTO
            UserAndRolesDTO objUserAndRolesDTO =
                new UserAndRolesDTO();
            objUserAndRolesDTO.UserName = UserName;
            objUserAndRolesDTO.colUserRoleDTO = colUserRoleDTO;
            return objUserAndRolesDTO;
        }
        #endregion

        #region private List<string> RolesUserIsNotIn(string UserName)
        private List<string> RolesUserIsNotIn(string UserName)
        {
            // Get roles the user is not in
            var colAllRoles = RoleManager.Roles.Select(x => x.Name).ToList();

            // Go get the roles for an individual
            ApplicationUser user = UserManager.FindByName(UserName);

            // If we could not find the user, throw an exception
            if (user == null)
            {
                throw new Exception("Could not find the User");
            }

            var colRolesForUser = UserManager.GetRoles(user.Id).ToList();
            var colRolesUserInNotIn = (from objRole in colAllRoles
                                       where !colRolesForUser.Contains(objRole)
                                       select objRole).ToList();

            if (colRolesUserInNotIn.Count() == 0)
            {
                colRolesUserInNotIn.Add("No Roles Found");
            }

            return colRolesUserInNotIn;
        }
        #endregion
    }
}
