﻿using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
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
                        if (CheckDataIsNull(item))
                        {
                            PersonDataItem personDataItem = new PersonDataItem();
                            personDataItem.UserCode = item.UserCode;
                            personDataItem.UserName = item.UserName;
                            personDataItem.IdentityCard = item.IdentityCard;
                            personDataItem.Image = item.Image;
                            person.data.Add(personDataItem);
                            person.Message += item.UserCode + "--参数有误存在null值|";
                            continue;
                        }
                        if (item.UserCode == null || item.UserCode == "" )
                        {

                            PersonDataItem personDataItem = new PersonDataItem();
                            personDataItem.UserCode = item.UserCode;
                            personDataItem.UserName = item.UserName;
                            personDataItem.IdentityCard = item.IdentityCard;
                            personDataItem.Image = item.Image;
                            person.data.Add(personDataItem);
                            person.Message += item.UserCode + "--工号信息不能为空|";
                            continue;
                        }

                        bool IsExistUser = UserService.IsExistUsercode("", item.UserCode);
                        if (IsExistUser)
                        {
                            Sys_User sys_User = UserService.GetUserByUsercode(item.UserCode);
                            sys_User.UserCode = item.UserCode;
                            sys_User.UserName = item.UserName;
                            sys_User.DataFrom = "API";
                            if (item.Image!="")
                            {
                                //    Image bb = Image.FromFile("C:\\Users\\1420031550\\Desktop\\签名.jpg");

                                //    byte[] aa = GetImageBytes(bb);
                                //    //string img = System.Text.Encoding.Default.GetString(aa);
                                //    string img = Convert.ToBase64String(aa);
                                //  //  byte[] imageBytes = Convert.FromBase64String(img);

                                //    Image a = ReadImage(Convert.FromBase64String(img));
                                string rootUrl = ConfigurationManager.AppSettings["localRoot"];
                                string url = @"FileUpload\User\" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + "~" + item.UserName + ".jpg";
                                string filename = rootUrl + url;
                                System.IO.File.WriteAllBytes(filename, Convert.FromBase64String(item.Image));
                                sys_User.SignatureUrl = url;
                                
                            }
                           // db.SubmitChanges();
                            BLL.UserService.UpdateSysUser(sys_User);


                        }
                        else
                        {
                            Sys_User sys_User = new Sys_User();
                            sys_User.UserId = SQLHelper.GetNewID(typeof(Model.Sys_User));
                            sys_User.Account = item.UserCode;
                            sys_User.UserCode = item.UserCode;
                            sys_User.UserName = item.UserName;
                            sys_User.IdentityCard = item.IdentityCard;
                            sys_User.UnitId = Const.UnitId_SEDIN;
                            sys_User.Password = Funs.EncryptionPassword(Const.Password);
                            sys_User.DataFrom = "API";
                             if (item.Image != "")
                             {
                                string rootUrl = ConfigurationManager.AppSettings["localRoot"];
                                string url = @"FileUpload\User\" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + "~" + item.UserName + ".jpg";
                                string filename = rootUrl + url;
                                System.IO.File.WriteAllBytes(filename, Convert.FromBase64String(item.Image));
                                sys_User.SignatureUrl = url;
                             }

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
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Base_Project base_Project = new Base_Project();
            Project_ProjectUser project_ProjectUser = new Project_ProjectUser();


            base_Project = BLL.ProjectService.GetProjectByProjectCode(ProjectCode);
            if (base_Project==null)
            {
                return;
            }
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

                        if (CheckDataIsNull(item)) //判断参数是否有空值
                        {
                            Pro_PersonDataItem pro_personDataItem = new Pro_PersonDataItem();
                            pro_personDataItem.ProjectCode = item.ProjectCode;
                            pro_personDataItem.UserCode = item.UserCode;
                            pro_personDataItem.UserName = item.UserName;
                            pro_personDataItem.IdentityCard = item.IdentityCard;

                            _Person.data.Add(pro_personDataItem);
                            _Person.Message += item.UserCode + "--参数有误存在null值|";
                            continue;
                        } 


                        if (item.UserCode==null|| item.UserCode == "")
                        {
                            Pro_PersonDataItem pro_personDataItem = new Pro_PersonDataItem();
                            pro_personDataItem.ProjectCode = item.ProjectCode;
                            pro_personDataItem.UserCode = item.UserCode;
                            pro_personDataItem.UserName = item.UserName;
                            pro_personDataItem.IdentityCard = item.IdentityCard;

                            _Person.data.Add(pro_personDataItem);
                            _Person.Message += item.UserCode+"--身份证信息有误|";
                            continue;
                        }

                        if (item.ProjectCode == null || item.ProjectCode == "")
                        {
                            Pro_PersonDataItem pro_personDataItem = new Pro_PersonDataItem();
                            pro_personDataItem.ProjectCode = item.ProjectCode;
                            pro_personDataItem.UserCode = item.UserCode;
                            pro_personDataItem.UserName = item.UserName;
                            pro_personDataItem.IdentityCard = item.IdentityCard;

                            _Person.data.Add(pro_personDataItem);
                            _Person.Message += item.UserCode + "--项目代号不能为空|";
                            continue;
                        }

                        bool IsExistUser = UserService.IsExistUsercode("", item.UserCode);
                        if (IsExistUser)
                        {
                            Model. Sys_User sys_User = UserService.GetUserByUsercode(item.UserCode);
                            sys_User.UserCode = item.UserCode;
                            sys_User.UserName = item.UserName;
                            sys_User.DataFrom = "API";
                            db.SubmitChanges();

                            APIHTGLPersonService.AddProjectUser(item.ProjectCode, sys_User.UserId);
   
                        }
                        else
                        {
                            string newKeyID = SQLHelper.GetNewID(typeof(Model.Sys_User));
                            Model.Sys_User newUser = new Model.Sys_User
                            { 
                            UserId = newKeyID,
                            Account = item.UserCode,
                            UserCode = item.UserCode,
                            UserName = item.UserName,
                            IdentityCard = item.IdentityCard,
                            UnitId = Const.UnitId_SEDIN,
                            Password = Funs.EncryptionPassword(Const.Password),
                            DataFrom = "API",
                        };
                            db.Sys_User.InsertOnSubmit(newUser);
                            db.SubmitChanges();
                             AddProjectUser(item.ProjectCode, newKeyID);
  
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

        private static bool CheckDataIsNull(object t)
        {
            bool Isok = false;
            Type type = t.GetType();
            PropertyInfo[] pArray = type.GetProperties();

            foreach (PropertyInfo p in pArray)
            {
                if (p.GetValue(t)==null)
                {
                    Isok = true;

                }
            }
             return Isok;
         }
        /// <summary>
        /// 将图片转换为二进制流
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        private static byte[] GetImageBytes(Image image)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, (object)image);
            ms.Close();
            return ms.ToArray();
        }
        //将图片转换为二进制流的调用
        // bt1 = GetImageBytes(rootComponent.BackgroundImage);


        /// <summary>
        /// 将二进制流转换为图片
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Image ReadImage(byte[] bytes)
        {
            //MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
            //BinaryFormatter bf = new BinaryFormatter();
            //object obj = bf.Deserialize(ms);
            //ms.Close();
            //return (Image)obj; //方法一
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image outputImg = Image.FromStream(ms);
                ms.Close();

                return outputImg;
            }
        }

        //将二进制流转换为图片
      //  pb.Image = ReadImage((byte[]) ri.Result.Rows[i]["image"]);

    }
}
