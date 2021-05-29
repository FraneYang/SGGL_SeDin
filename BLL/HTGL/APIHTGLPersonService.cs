using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public static  class APIHTGLPersonService
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="PersonjsonData">人员信息（json字符串）</param>
        public static Model.Person SavePerson(Person PersonjsonData)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
				
                Person rb = PersonjsonData;
                Person person = new Person();
                person.data = new List<PersonDataItem>();

                 foreach (PersonDataItem item in rb.data)
                 {
                    try
                    {
                        if (item.IdentityCard == null || item.IdentityCard == "" || item.IdentityCard.Length != 18)
                        {

                            PersonDataItem personDataItem = new PersonDataItem();
                            personDataItem.UserCode = item.UserCode;
                            personDataItem.UserName = item.UserName;
                            personDataItem.IdentityCard = item.IdentityCard;

                            person.data.Add(personDataItem);
                            person.Message += item.UserCode + "--身份证信息有误";
                            continue;
                        }

                        bool IsExistUser = UserService.IsExistUserIdentityCard("", item.IdentityCard);
                        if (IsExistUser)
                        {
                            Sys_User sys_User = UserService.GetUserByIdentityCard(item.IdentityCard);
                            sys_User.Account = item.UserCode;
                            sys_User.UserName = item.UserName;
                            sys_User.IdentityCard = item.IdentityCard;
                            sys_User.UnitId = Const.UnitId_SEDIN;
 
                            BLL.UserService.UpdateSysUser(sys_User);

 
                        }
                        else
                        {
                            Sys_User sys_User = new Sys_User();
                            sys_User.UserId = SQLHelper.GetNewID(typeof(Model.Sys_User));
                            sys_User.Account = item.UserCode;
                            sys_User.UserName = item.UserName;
                            sys_User.IdentityCard = item.IdentityCard;
                            sys_User.UnitId = Const.UnitId_SEDIN;
                            sys_User.Password = Funs.EncryptionPassword(Const.Password);


                            BLL.UserService.AddUser(sys_User);
                         }

                    }
                    catch (Exception ex)
                    {
                        PersonDataItem personDataItem = new PersonDataItem();
                        personDataItem.UserCode = item.UserCode;
                        personDataItem.UserName = item.UserName;
                        personDataItem.IdentityCard = item.IdentityCard;

                        person.data.Add(personDataItem);
                        person.Message += ex.Message;

                     }
       
                }

                return person;

            }
        }

        public static void AddProjectUser( string ProjectCode,string UserID)
        {
            Base_Project base_Project = new Base_Project();
            Project_ProjectUser project_ProjectUser = new Project_ProjectUser();


            base_Project = BLL.ProjectService.GetProjectByProjectCode(ProjectCode);
            var ProUser = ProjectUserService.GetProjectUserByUserIdProjectId(base_Project.ProjectId, UserID);
            if (ProUser == null)
            {
                project_ProjectUser.ProjectUserId = SQLHelper.GetNewID(typeof(Model.Project_ProjectUser));
                project_ProjectUser.ProjectId = base_Project.ProjectId;
                project_ProjectUser.UserId = UserID;
                project_ProjectUser.UnitId = Const.UnitId_SEDIN;
                project_ProjectUser.IsPost = true;
                ProjectUserService.AddProjectUser(project_ProjectUser);
            }
            else
            {
                project_ProjectUser.ProjectId = base_Project.ProjectId;
                project_ProjectUser.UserId = UserID;
                project_ProjectUser.UnitId = Const.UnitId_SEDIN;
                project_ProjectUser.IsPost = true;
                ProjectUserService.UpdateProjectUser(project_ProjectUser);

            }
        }


        public static Pro_Person SavePro_Person(Pro_Person PersonjsonData)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Pro_Person rb = PersonjsonData;
               
                Pro_Person _Person = new Pro_Person();
                _Person.data = new  List<Pro_PersonDataItem>();
                foreach (Pro_PersonDataItem item in rb.data)
                {
                    try
                    { 
                         Project_ProjectUser project_ProjectUser = new Project_ProjectUser();
                         Base_Project base_Project = new Base_Project();
                        if (item.IdentityCard==null|| item.IdentityCard==""||item.IdentityCard.Length!=18)
                        {
                            Pro_PersonDataItem pro_personDataItem = new Pro_PersonDataItem();
                            pro_personDataItem.ProjectCode = item.ProjectCode;
                            pro_personDataItem.UserCode = item.UserCode;
                            pro_personDataItem.UserName = item.UserName;
                            pro_personDataItem.IdentityCard = item.IdentityCard;

                            _Person.data.Add(pro_personDataItem);
                            _Person.Message += item.UserCode+"--身份证信息有误||";
                            continue;
                        }

                        bool IsExistUser = UserService.IsExistUserIdentityCard("", item.IdentityCard);
                        if (IsExistUser)
                        {
                            Sys_User sys_User = UserService.GetUserByIdentityCard(item.IdentityCard);
                            sys_User.Account = item.UserCode;
                            sys_User.UserName = item.UserName;
                            sys_User.IdentityCard = item.IdentityCard;
                            sys_User.UnitId = Const.UnitId_SEDIN;


                            BLL.UserService.UpdateSysUser(sys_User);

                            AddProjectUser(item.ProjectCode, sys_User.UserId);
   
                        }
                        else
                        {
                            Sys_User sys_User = new Sys_User();
                            sys_User.UserId = SQLHelper.GetNewID(typeof(Model.Sys_User));
                            sys_User.Account = item.UserCode;
                            sys_User.UserName = item.UserName;
                            sys_User.IdentityCard = item.IdentityCard;
                            sys_User.UnitId = Const.UnitId_SEDIN;
                            sys_User.Password = Funs.EncryptionPassword(Const.Password);


                            BLL.UserService.AddUser(sys_User);

                            AddProjectUser(item.ProjectCode, sys_User.UserId);
  
                        }
  
                    }
                    catch (Exception ex)
                    {
                        Pro_PersonDataItem pro_personDataItem = new Pro_PersonDataItem();
                        pro_personDataItem.ProjectCode = item.ProjectCode;
                        pro_personDataItem.UserCode = item.UserCode;
                        pro_personDataItem.UserName = item.UserName;
                        pro_personDataItem.IdentityCard = item.IdentityCard;

                        _Person.data.Add(pro_personDataItem);
                        _Person.Message += ex.Message;
                    }

                }

                return _Person;

            }
        }

    }
}
