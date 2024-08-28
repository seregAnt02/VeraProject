// using System;
// using System.Collections.Generic;
// using System.DirectoryServices;
// using System.DirectoryServices.AccountManagement;
// using System.DirectoryServices.ActiveDirectory;
// using System.Linq;
// using System.Text;
// using System.Collections;
// using System.Net;
// using System.Security.Principal;
// using ActiveDs;

// namespace work_ad
// {
//     public static class AccountManagementExtensions
//     {

//         /// <summary>
//         /// Получить свойства
//         /// </summary>
//         /// <param name="property">Имя свойства</param>
//         /// <returns>Возвращает значение свойства</returns>
//         public static String GetProperty(this Principal principal, String property)
//         {
//             DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;
//             if (directoryEntry.Properties.Contains(property))
//                 return directoryEntry.Properties[property].Value.ToString();
//             else
//                 return String.Empty;
//         }

//         /// <summary>
//         /// Задать свойства
//         /// </summary>
//         /// <param name="property">Имя свойства</param>
//         /// <param name="value">Присваеваемое значение</param>
//         public static void SetProperty(this Principal principal, String property,String value)
//         {
//             DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;

//             directoryEntry.Properties[property].Value = value;

//         }
    


//     }

    

//       /// <summary>
//     /// Флаги свойства пользователя
//     /// </summary>
//      public struct UserFlags
//     {
//      public bool enable;
//      public bool PasswordNeverExpires;
//      public bool UserCannotChangePassword;
//      public bool ExpirePassword;
//      }

//     public class cAD
//     {
//         #region Переменные

//         public static string sDomainDefault = "192.168.71.111";//куда подключаемся по умолчанию
//         public static string sDomain = "contoso.com";//куда подключаемся
//     //    readonly static string sDefaultOU = "OU=users,OU=uns,DC=contoso,DC=com";
//         public static string sDefaultRootOU = "DC=contoso,DC=com";//где ищем по умолчанию
//         public static string sServiceUser = "user";//пользователь от кого делаем
//         public static string sServicePassword = "password";
//         private static bool enabl = true;

//         #endregion

//         #region Методы проверки

//         /// <summary>
//         /// Проверка имени пользователя и пароля
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <param name="sPassword">Пароль</param>
//         /// <returns>Возвращает true, если имя и пароль верны</returns>
//         public static bool OldValidateCredentials(string sUserName, string sPassword)
//         {
//             return GetPrincipalContext().ValidateCredentials(sUserName, sPassword);
//         }


//         /// <summary>
//         /// Проверка имени пользователя и пароля
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <param name="sPassword">Пароль</param>
//         /// <returns>Возвращает true, если имя и пароль верны</returns>
//         public static bool LDAPValidateCredentials(string sUserName, string sPassword)
//         {

//             var pc = LDAPFindOne("", sUserName, LdapFilter.UsersSAN, sUserName, sPassword);

//             if (pc == null) return false;
//             else { return true; }
//         }

//         /// <summary>
//         /// Проверка имени пользователя и пароля
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <param name="sPassword">Пароль</param>
//         /// <returns>Возвращает true, если имя и пароль верны</returns>
//         public static bool LDAPValidateCredentials()
//         {

//             var pc = LDAPFindOne("", sServiceUser, LdapFilter.UsersSAN);

//             if (pc == null) return false;
//             else { return true; }
//         }

//         //------------------------------------------------------------------------
//         /// <summary>
//         /// Проверка сервера
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <param name="sPassword">Пароль</param>
//         /// <returns>Возвращает true, если имя и пароль верны и сервер хороший</returns>
//         public static bool OldValidateController(string sUserName, string sPassword)
//         {
//             string err;
//             PrincipalContext pc = TryGetPrincipalContext(out err);
//             if (pc == null) return false;
//             else { return pc.ValidateCredentials(sUserName, sPassword); }
            
//         }

//        /// <summary>
//         /// Проверка сервера
//         /// </summary>
//         /// <returns>Возвращает true, если имя и пароль верны и сервер хороший</returns>
//         public static bool OldValidateController()
//         {
//             string err;
//             PrincipalContext pc = TryGetPrincipalContext(out err);
//             if (pc == null) return false;
//             else { return pc.ValidateCredentials(sServiceUser, sServicePassword); }

//         }

//         /// <summary>
//         /// Проверка сервера
//         /// </summary>
//         /// <returns>Возвращает true, есои сервер вернул данные</returns>
//         public static bool ValidateController()
//         {
//             return LDAPValidateCredentials();

//         }
//         //----------------------------------------------------------
       
//         /// <summary>
//         /// Проверка срока действия учетной записи
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <returns>Возвращает true, если срок действия истек</returns>
//         public static bool IsUserExpired(string sUserName)
//         {
//             return GetUser(sUserName).AccountExpirationDate != null ? false : true;
//         }

//         /// <summary>
//         /// Проверка флага на истекший пароль пользователя
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <returns>Возвращает true, если срок действия пароля истек</returns>
//         public static bool IsChangePasswordAtNextLogonSet(string sUserName)
//         {

//             var user = GetUser(sUserName);

//             if (user.LastPasswordSet == null) return true;

//             else  return false;
//         }


//         /// <summary>
//         /// Проверка флага на истекший пароль пользователя
//         /// </summary>
//         /// <param name="user">UserPrincipal пользователя</param>
//         /// <returns>Возвращает true, если срок действия пароля истек</returns>
//         public static bool IsChangePasswordAtNextLogonSet(UserPrincipal user)
//         {
//             if (user.LastPasswordSet == null) return true;

//             else return false;
//         }

//         /// <summary>
//         /// Проверка существования пользователя в AD
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <returns>Возвращает true, если пользователь существует</returns>
//         public static bool IsUserExisiting(string sUserName)
//         {
//             return GetUser(sUserName) == null ? false : true;

//           /*  bool found = false;
//             if (DirectoryEntry.Exists("LDAP://" + objectPath))
//             {
//                 found = true;
//             }
//             return found;*/

//         }

//         /// <summary>
//         /// Проверка существования пользователя в AD
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <returns>Возвращает true, если пользователь существует</returns>
//         public static bool LDAPIsUserExisiting(string sUserName)
//         {
            
//               bool found = false;
//               if (LDAPFindOne("",sUserName,LdapFilter.UsersSAN)!=null)
//               {
//                   found = true;
//               }

//               return found;

//         }


//         /// <summary>
//         /// Проверка существования пользователя в AD по DN
//         /// </summary>
//         /// <param name="DN">DN пользователя</param>
//         /// <returns>Возвращает true, если пользователь существует</returns>
//         public static bool IsUserDNExisiting(string dn,out string err)
//         {
//             PrincipalContext oPrincipalContext = GetPrincipalContext();
//             UserPrincipal result = null;
//             err=null;
//             //Перемудрено с вложением и перехватом ошибок//
//             if (dn != null && dn != "")
//             {
//                 try
//                 {
//                     result = GetUser(dn);
//                 }
//                 catch (Exception e) 
//                 {
//                     err = e.Message;
//                     return false;
//                 }
     
//             }
            
//             return result == null ? false : true;
            
//         }


//       /*  /// <summary>
//         /// Проверка существования пользователя по полному имени и аккаунту
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <param name="sUserName">Полное имя</param>
//         /// <returns>Возвращает true, если пользователь существует</returns>
//         public static bool IsUserExisiting(string sUserName, string fullname)
//         {
//             return GetUser(sUserName, fullname) == null ? false : true;
//         }*/

//         /// <summary>
//         /// Проверяет блокировку пользователя
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <returns>Возвращает true, если учетная запись заблокирована</returns>
//         public static bool IsAccountLocked(string sUserName)
//         {
//             return GetUser(sUserName).IsAccountLockedOut();
//         }

//         #endregion

//         #region Методы поиска


//         /// <summary>
//         /// Возвращаем объект домена
//         /// </summary>
//         /// <param name="Dn">домен</param>
//         public static Domain GetDomain(string dn=null)
//         {
//             try
//             {
//                 if (dn == null) return Domain.GetDomain(new DirectoryContext(DirectoryContextType.Domain));
//                 return Domain.GetDomain(new DirectoryContext(DirectoryContextType.Domain, dn));
//             }
//             catch
//             {
//                 return null;
//             }
//         }

//         //------------------------------------------------------------------------------------------------


//         /// <summary>
//         /// Возвращаем список контроллеров домена
//         /// </summary>
//         /// <param name="Dn">домен</param>
//         public static ArrayList GetListOfDomainControllersByDirectorySearcher()
//         {
//             ArrayList alDcs = new ArrayList();

//                 try
//             {
//                 PrincipalContext context = new PrincipalContext(ContextType.Domain);
//                 DirectoryEntry domainEntry = new DirectoryEntry("LDAP://"+ context.ConnectedServer +"/OU=Domain Controllers,"+sDefaultRootOU);
//                 DirectorySearcher searcher = new DirectorySearcher(domainEntry);
//                 searcher.PageSize = 100;
//                 searcher.SizeLimit = 5000;

//                 searcher.Filter = "(&(objectCategory=computer)(objectClass=computer)(userAccountControl:1.2.840.113556.1.4.803:=8192))";

//               //  searcher.PropertiesToLoad.AddRange(new string[] { "name", "operatingsystem" });

//                 SearchResultCollection fnd=searcher.FindAll();

//                 foreach (SearchResult result in fnd)
//                 {
//                     alDcs.Add(result.Properties["name"][0].ToString());
                    
//                 }
//             }
//             catch (Exception ex)
//             {
//                 return null;
//             }
            
//             alDcs.Sort();

//             return alDcs;
//         }

//         /// <summary>
//         /// Возвращаем список контроллеров домена
//         /// </summary>
//         /// <param name="ip_get">разрешать ИП адрес, по умолчанию да</param>
//         public static ArrayList GetListOfDomainControllersByDirectorySearcherFull(bool ip_get=true)
//         {
//             ArrayList alDcs = new ArrayList();
//             string[] Dlist;

//             try
//             {
//                 PrincipalContext context = new PrincipalContext(ContextType.Domain);
//                 DirectoryEntry domainEntry = new DirectoryEntry("LDAP://" + context.ConnectedServer + "/OU=Domain Controllers," + sDefaultRootOU);
//                 DirectorySearcher searcher = new DirectorySearcher(domainEntry);

//                 searcher.PageSize = 100;
//                 searcher.SizeLimit = 5000;
                
//                 searcher.Filter = "(&(objectCategory=computer)(objectClass=computer)(userAccountControl:1.2.840.113556.1.4.803:=8192))";

//                 //  searcher.PropertiesToLoad.AddRange(new string[] { "name", "operatingsystem" });

//                 SearchResultCollection fnd = searcher.FindAll();

//                 foreach (SearchResult result in fnd)
//                 {
//                     Dlist = new string[3];

//                     Dlist[0] = result.Properties["dNSHostName"][0].ToString();
//                     Dlist[1] = result.Properties["operatingSystem"][0].ToString();
//                     Dlist[2] = "";

//                     if (ip_get)
//                     {
//                     try {

//                         IPAddress[] ips = Dns.GetHostAddresses(Dlist[0]);

//                         foreach (IPAddress ip in ips)
//                         {

//                             Dlist[2] += ip.ToString() + ";";
//                         }

//                     } catch
//                     {
//                         Dlist[2] = "no IP";
//                     }
//                      }

//                    alDcs.Add(Dlist);

//                 }
//             }
//             catch (Exception ex)
//             {
//                 return null;
//             }


//             try
//             {
//                 alDcs.Sort();
//             }
//             catch
//             {

//             }

//             /*  alDcs.Sort(delegate (string[] x, string[] y)
//               {
//                   if (x[0] == null && y[0] == null) return 0;
//                   else if (x[0] == null) return -1;
//                   else if (y[0] == null) return 1;
//                   else return x[0].CompareTo(y[0]);
//               });*/

         

//             return alDcs;
//         }

//         /// <summary>
//         /// Получить указанного пользователя Active Directory
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя для извлечения</param>
//         /// <returns>Объект UserPrincipal</returns>
//         public static UserPrincipal GetUser(string sUserName)
//         {
//             PrincipalContext oPrincipalContext = GetPrincipalContext();
//             UserPrincipal result=null;
//             sUserName = sUserName.Trim();

//             if (sUserName != null && sUserName != "")
//             {
//                 try
//                 {
//                     result = UserPrincipal.FindByIdentity(oPrincipalContext, sUserName);
                    
//                 }
//                 catch (Exception er)
//                 {
                    
//                     return null;
//                 }
                
//                 return result;

//             }
//             else { return null; }

//         }


//         /// <summary>
//         /// Получить указанного пользователя Active Directory
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя для извлечения</param>
//         /// <returns>Объект UserPrincipal</returns>
//         public static UserPrincipal GetUser(string sUserName, out string user_err)
//         {
//             PrincipalContext oPrincipalContext = GetPrincipalContext();
//             UserPrincipal result = null;
//             sUserName = sUserName.Trim();

//             if (sUserName != null && sUserName != "")
//             {
//                 try
//                 {
//                     result = UserPrincipal.FindByIdentity(oPrincipalContext, sUserName);

//                 }
//                 catch (Exception er)
//                 {
//                     user_err=er.Message;
//                     return null;
//                 }
//                 user_err = "ok";
//                 return result;

//             }
//             else {

//                 user_err = "Пустой логин";
//                 return null;

//                 }

//         }

//         /// <summary>
//         /// Получить указанного пользователя Active Directory
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя для извлечения</param>
//         /// <param name="err">Возвращает ошибку</param>
//         /// <returns>Объект UserPrincipal</returns>
//         public static UserPrincipal GetUserCN(string sUserName, out string err)
//         {

//          PrincipalContext oPrincipalContext = GetPrincipalContext();
//             err = null;

//             if (sUserName != null && sUserName != "")
//             {
//                 try
//                 {
//                     return UserPrincipal.FindByIdentity(oPrincipalContext, IdentityType.Name, sUserName);

//                 }catch(Exception e)
//                 {
//                     err = e.Message;
//                     return null;
//                 }

//             }
//             else return null;

//         }

//         /// <summary>
//         /// Возвращает DirectoryEntry пользователя по DistinguishedName
//         /// </summary>
//         /// <param name="sUserPrincipal"> DistinguishedName пользователя</param>
//         /// <returns>Возвращает DirectoryEntry пользователя</returns>
//         public static DirectoryEntry LDAPGetUser(string UserPath)
//         {
//             DirectoryEntry user = new DirectoryEntry("LDAP://" + sDomain + "/" + UserPath, sServiceUser, sServicePassword);

//             return user;
//         }



//         /// <summary>
//         /// Получить группу Active Directory
//         /// </summary>
//         /// <param name="sGroupName">Группа для получения</param>
//         /// <returns>Возвращает объект GroupPrincipal</returns>
//         public static GroupPrincipal GetGroup(string sGroupName)
//         {
//             if (sGroupName!="")
//             {
//                 PrincipalContext oPrincipalContext = GetPrincipalContext();
//                 return GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);
//             } return null;
//         }

//         #endregion

//         #region Методы управления учетными записями

//         /// <summary>
//         /// Установка нового пароля
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <param name="sNewPassword">Новый пароль</param>
//         /// <param name="sMessage">Описание ошибки (если возникла)</param>
//         public static void SetUserPassword(string sUserName, string sNewPassword, out string sMessage)
//         {
//             try
//             {
//                 GetUser(sUserName).SetPassword(sNewPassword);
//                 sMessage = string.Empty;
//             }
//             catch (Exception ex)
//             {
//                 sMessage = ex.Message;
//             }
//         }

//         /// <summary>
//         /// Установка свойств пользователя
//         /// </summary>
//         /// <param name="prop">Устанавливаемые параметры</param>
//         /// <param name="sMessage">Описание ошибки (если возникла)</param>
//         public static bool SetUserProperty(UserPrincipal oUserPrincipal, UserProperty prop, out string sMessage)
//         {
//             if (oUserPrincipal != null)
//             {
//                 try
//                 {

//                     if (prop.displayname != null)
//                         if (prop.displayname != "") oUserPrincipal.DisplayName = prop.displayname; else oUserPrincipal.DisplayName = null;

//                     if (prop.sn != null)
//                         if (prop.sn != "") oUserPrincipal.Surname = prop.sn; else oUserPrincipal.Surname = null;

//                     if (prop.givenname != null)
//                         if (prop.givenname != "") oUserPrincipal.GivenName = prop.givenname; else oUserPrincipal.GivenName = null;

//                      if (prop.description != null)
//                     if (prop.description != "") { prop.description = prop.description.Length >= 65 ? prop.description.Substring(0, 64) : prop.description; oUserPrincipal.Description = prop.description; } else oUserPrincipal.Description = null;

//                      if (prop.mail != null)
//                     if (prop.mail != "") oUserPrincipal.EmailAddress = prop.mail; else oUserPrincipal.EmailAddress = null;

//                      if (prop.telephoneNumber != null)
//                     if (prop.telephoneNumber != "") oUserPrincipal.VoiceTelephoneNumber = prop.telephoneNumber; else oUserPrincipal.VoiceTelephoneNumber = null;

//                      if (prop.scriptPath != null)
//                     if (prop.scriptPath != "") oUserPrincipal.ScriptPath = prop.scriptPath; else oUserPrincipal.ScriptPath = null;

//                      if (prop.physicaldeliveryofficename != null)
//                     if (prop.physicaldeliveryofficename != "") oUserPrincipal.SetProperty("physicaldeliveryofficename", prop.physicaldeliveryofficename); else oUserPrincipal.SetProperty("physicaldeliveryofficename", null);

//                      if (prop.title != null)
//                     if (prop.title != "") { prop.title = prop.title.Length >= 65 ? prop.title.Substring(0, 64) : prop.title; oUserPrincipal.SetProperty("title", prop.title); } else oUserPrincipal.SetProperty("title", null);

//                      if (prop.department != null)
//                     if (prop.department != "") { prop.department = prop.department.Length >= 65 ? prop.department.Substring(0, 64) : prop.department; oUserPrincipal.SetProperty("department", prop.department); } else oUserPrincipal.SetProperty("department", null);

//                      if (prop.company != null)
//                     if (prop.company != "") { prop.company = prop.company.Length >= 65 ? prop.company.Substring(0, 64) : prop.company; oUserPrincipal.SetProperty("company", prop.company); } else oUserPrincipal.SetProperty("company", null);

//                      if (prop.manager != null)
//                     if (prop.manager != "") oUserPrincipal.SetProperty("manager", prop.manager); else oUserPrincipal.SetProperty("manager", null);

//                      if (prop.homeDrive != null)
//                          if (prop.homeDrive != "") oUserPrincipal.HomeDrive = prop.homeDrive; else oUserPrincipal.HomeDrive = null;

//                      if (prop.homeDirectory != null)
//                          if (prop.homeDirectory != "") oUserPrincipal.HomeDirectory = prop.homeDirectory; else oUserPrincipal.HomeDirectory = null;

//                      if (prop.userworkstations != null)
//                          if (prop.userworkstations != "") oUserPrincipal.SetProperty("userworkstations", prop.userworkstations); else oUserPrincipal.SetProperty("userworkstations", null);

//                     oUserPrincipal.Save();

//                 }catch(Exception e){
//                     sMessage = e.Message; return false; 
//                 }

//                 sMessage = string.Empty; return true;
//             }
//             else { sMessage = string.Empty; return false; }
//         }


//         /// <summary>
//         /// Включить учетную запись пользователя
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         public static void EnableUserAccount(string sUserName)
//         {
//             using (UserPrincipal oUserPrincipal = GetUser(sUserName))
//             {
//                 oUserPrincipal.Enabled = true;
//                 oUserPrincipal.Save();
//             }
//         }

//         /// <summary>
//         /// Отключить учетную запись пользователя
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         public static void DisableUserAccount(string sUserName)
//         {
//             using (UserPrincipal oUserPrincipal = GetUser(sUserName))
//             {
//                 oUserPrincipal.Enabled = false;
//                 oUserPrincipal.Save();
//             }
//         }

//         /// <summary>
//         /// Установить признак истечения срока действия пароля
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя с "истекающим" сроком действия</param>
//         public static void ExpireUserPassword(string sUserName)
//         {
//             using (UserPrincipal oUserPrincipal = GetUser(sUserName))
//             {
//                 oUserPrincipal.ExpirePasswordNow();
//                 oUserPrincipal.Save();
//             }
//         }

//         /// <summary>
//         /// Разблокировка заблокированного пользователя
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя для снятия lock'а</param>
//         public static void UnlockUserAccount(string sUserName)
//         {
//             using (UserPrincipal oUserPrincipal = GetUser(sUserName))
//             {
//                 oUserPrincipal.UnlockAccount();
//                 oUserPrincipal.Save();
//             }
//         }

//         /// <summary>
//         /// Создание нового пользователя Active Directory
//         /// </summary>
//         /// <param name="sOU">OU создания нового пользователя</param>
//         /// <param name="sUserName">логин пользователя</param>
//         /// <param name="sPassword">Пароль</param>
//         /// <param name="sGivenName">Имя</param>
//         /// <param name="sSurName">Фамилия</param>
//         /// <returns>Возвращает объект UserPrincipal</returns>
//         public static UserPrincipal CreateNewUser(string sOU, string sUserName, string sPassword,UserProperty userProp, UserFlags flag, out string ErrMessage)
//         {
//             if (sUserName != "")
//             {
//                 //string fullname = sSurName + " " + sGivenName;
//                 if (LDAPIsUserExisiting(sUserName)) { ErrMessage = sUserName + "- Пользователь существует"; return null; }
//                 // if (IsUserExisiting(fullname)) { ErrMessage = sUserName + "- Пользователь существует"; return null; }
//                 else
//                 {
//                 /*    string[] ms = userProp.displayname.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

//                     if (ms.Length > 2)
//                     {*/
//                         PrincipalContext oPrincipalContext = GetPrincipalContext(sOU);
//                         if (oPrincipalContext != null)
//                         {
//                             try
//                             {
//                                 UserPrincipal oUserPrincipal = new UserPrincipal(oPrincipalContext, sUserName, sPassword, true)
//                                 {
//                                     Name = userProp.name,
//                                     UserPrincipalName = sUserName + "@" + sDomainDefault,
//                                  //   GivenName = userProp.givenname,
//                                     Surname = userProp.sn,
//                                     DisplayName = userProp.displayname,
//                                     Enabled = flag.enable,
//                                     PasswordNeverExpires = flag.PasswordNeverExpires,
//                                     UserCannotChangePassword = flag.UserCannotChangePassword,
//                                     PasswordNotRequired=false
//                                 };

//                                 if (flag.ExpirePassword) oUserPrincipal.ExpirePasswordNow();

//                                 oUserPrincipal.Save();

//                                 ErrMessage = string.Empty;
//                                 return oUserPrincipal;

//                             }
//                             catch (Exception e)
//                             {
//                                 ErrMessage = e.Message;
//                                 return null;

//                             }

//                         }
//                         else { ErrMessage = "Ошибка получения OU"; return null; }

//            /*         }
//                     else
//                     {
//                         ErrMessage = "ошибка fullname"; return null;
//                     }*/

//                 }
//             }
//             else { ErrMessage = "логин == пусто"; return null; }
//         }

//         /// <summary>
//         /// Переименование пользователя Active Directory
//         /// </summary>
//         /// <param name="sUserName">логин пользователя</param>
//         /// <param name="sFullName">новое имя пользователя</param>
//         /// <returns>Возвращает Истина если все хорошо</returns>
//         public static bool RenameUser(string sUserName,string sFullName, out string err)
//         {
//             try
//             {
//                 var user = UserPrincipal.FindByIdentity(GetPrincipalContext(), sUserName);
//                 if (user != null && sFullName!=null)
//                 {
//                     var dirEntry = (DirectoryEntry)user.GetUnderlyingObject();
//                     dirEntry.Rename("CN=" + sFullName);                 
//                     dirEntry.CommitChanges();
//                     dirEntry.Close();
//                 }
//                 else { err = "Ошибка в передаваемых параметрах!"; return false; }

//                 err = null;
//                 return true;
//             }
//             catch(Exception e) {
//                 err = e.Message;
//                 return false;
//             }
            
//         }

//         /// <summary>
//         /// Переименование пользователя Active Directory
//         /// </summary>
//         /// <param name="sUserName">логин пользователя</param>
//         /// <param name="sFullName">новое имя пользователя</param>
//         /// <returns>Возвращает Истина если все хорошо</returns>
//         public static bool LDAPRenameUser(string sUserName, string sFullName, out string err)
//         {
//             try
//             {
//                 SearchResult user= LDAPFindOne("", sUserName,LdapFilter.UsersSAN);
//                 //var user = UserPrincipal.FindByIdentity(GetPrincipalContext(), sUserName);
//                 if (user != null && sFullName != null)
//                 {
//                     using (DirectoryEntry dirEntry = new DirectoryEntry(user.Path, sServiceUser, sServicePassword))
//                     {
//                         if (dirEntry != null)
//                         {
//                             dirEntry.Rename("CN=" + sFullName);
//                             dirEntry.CommitChanges();
//                         }
//                         else { err = null; return false; }
//                     }

//                 }
//                 else { err = "Ошибка в передаваемых параметрах!"; return false; }

//                 err = null;
//                 return true;
//             }
//             catch (Exception e)
//             {
//                 err = e.Message;
//                 return false;
//             }

//         }

//         /// <summary>
//         /// Перемещение пользователя Active Directory
//         /// </summary>
//         /// <param name="sUserName">логин пользователя</param>
//         /// <param name="newDest">Новое размещение</param>
//         /// <returns>Возвращает Истина если все хорошо</returns>
//         public static bool MoveUser(string sUserName, string newDest,out string err)
//         {
          
//                 var user = UserPrincipal.FindByIdentity(GetPrincipalContext(), sUserName);

//                 return MoveUser(user, newDest,out err);
            
//         }

//         /// <summary>
//         /// Перемещение пользователя Active Directory
//         /// </summary>
//         /// <param name="sPrincipalName">UserPrincipal пользователя</param>
//         /// <param name="newDest">Новое размещение</param>
//         /// <returns>Возвращает Истина если все хорошо</returns>
//         public static bool MoveUser(UserPrincipal sPrincipalName, string newDest, out string err)
//         {
//             try
//             {

//                 if (sPrincipalName != null && newDest != null)
//                 {
//                     DirectoryEntry dirEntry = (DirectoryEntry)sPrincipalName.GetUnderlyingObject();

//                     DirectoryEntry nLocation = GetOU(newDest);

//                     dirEntry.MoveTo(nLocation);
//                    // dirEntry.CommitChanges();

//                     dirEntry.Close();
//                     nLocation.Close();
//                 }
//                 else { err = "Ошибка в передаваемых параметрах!"; return false; }

//                 err = null;
//                 return true;
//             }
//             catch (Exception e)
//             {
//                 err = e.Message;
//                 return false;
//             }


//         }


//         /// <summary>
//         /// Перемещение компьютер Active Directory
//         /// </summary>
//         /// <param name="name">имя компьютера</param>
//         /// <param name="newDest">Новое размещение</param>
//         /// <returns>Возвращает Истина если все хорошо</returns>
//         public static bool LDAPMoveWorkStations(string name, string newDest, out string err)
//         {
//             try
//             {

//                 if (name != null && newDest != null)
//                 {
//                     SearchResult SRcomp=LDAPFindOne("", name, LdapFilter.Computers);
//                     if (SRcomp != null)
//                     {
//                         DirectoryEntry dirEntry = SRcomp.GetDirectoryEntry();

//                     DirectoryEntry nLocation = GetOU(newDest);

//                     dirEntry.MoveTo(nLocation);
//                     // dirEntry.CommitChanges();

//                     dirEntry.Close();
//                     nLocation.Close();
//                     }
//                     else { err = "Объект не найден!"; return false; }
//                 }
//                 else { err = "Ошибка в передаваемых параметрах!"; return false; }

//                 err = null;
//                 return true;
//             }
//             catch (Exception e)
//             {
//                 err = e.Message;
//                 return false;
//             }


//         }

//         /// <summary>
//         /// Удаление пользователя Active Directory
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя для удаления</param>
//         /// <returns>Возвращает true, если удаление прошло успешно</returns>
//         public static bool DeleteUser(string sUserName)
//         {
//             try
//             {
//                 using (UserPrincipal oUserPrincipal = GetUser(sUserName))
//                 {
//                     oUserPrincipal.Delete();
//                 }
//                 return true;
//             }
//             catch
//             {
//                 return false;
//             }
//         }

//         #endregion

//         #region Методы для работы с группами

//         /// <summary>
//         /// Создание новой группы Active Directory
//         /// </summary>
//         /// <param name="sOU">OU, где будет создана новая группа</param>
//         /// <param name="sGroupName">Имя группы</param>
//         /// <param name="sDescription">Описание группы</param>
//         /// <param name="oGroupScope">Область группы</param>
//         /// <param name="bSecurityGroup">True - группа безопасности, false - группа рассылки</param>
//         /// <returns>Возвращает объект GroupPrincipal</returns>
//         public static GroupPrincipal CreateNewGroup(string sOU, string sGroupName, string sDescription, GroupScope oGroupScope, bool bSecurityGroup)
//         {
//             PrincipalContext oPrincipalContext = GetPrincipalContext(sOU);

//             GroupPrincipal oGroupPrincipal = new GroupPrincipal(oPrincipalContext, sGroupName)
//             {
//                 Description = sDescription,
//                 GroupScope = oGroupScope,
//                 IsSecurityGroup = bSecurityGroup
//             };

//             oGroupPrincipal.Save();

//             return oGroupPrincipal;
//         }

//         /// <summary>
//         /// Добавляет пользователя в указанную группу
//         /// </summary>
//         /// <param name="sUserName">Имя добавляемого пользователя</param>
//         /// <param name="sGroupName">Группа</param>
//         /// <returns>В случае успеха возвращает true</returns>
//         public static bool AddUserToGroup(string sUserName, string sGroupName,out string err)
//         {
//          UserPrincipal oUserPrincipal = GetUser(sUserName);
//          if (oUserPrincipal != null)
//          {
//              return AddUserToGroup(oUserPrincipal, sGroupName, out err);
//          }
//          else { err = "Пользователь не найден!"; return false; }
//         }

//         /// <summary>
//         /// Добавляет пользователя в указанную группу
//         /// </summary>
//         /// <param name="sUserName">UserPrincipal добавляемого пользователя</param>
//         /// <param name="sGroupName">Группа</param>
//         /// <returns>В случае успеха возвращает true</returns>
//         public static bool AddUserToGroup(UserPrincipal oUserPrincipal, string sGroupName, out string err)
//         {
//             try
//             {
                
//                 using (GroupPrincipal oGroupPrincipal = GetGroup(sGroupName))
//                 {
//                     if (oUserPrincipal != null && oGroupPrincipal != null)
//                     {
//                         if (!IsUserGroupMember(oUserPrincipal.Name, sGroupName))
//                         {
//                             oGroupPrincipal.Members.Add(oUserPrincipal);
//                             oGroupPrincipal.Save();
//                         }
//                         else { err = sGroupName + " уже состоит в этой группе!"; }
//                     }
//                 }
//                 err = string.Empty;
//                 return true;
//             }
//             catch (Exception e)
//             {
//                 err =e.Message;
//                 return false;
//             }
//         }


//         /// <summary>
//         /// Добавляет пользователя в указанную группу
//         /// </summary>
//         /// <param name="sUserName">UserPrincipal добавляемого пользователя</param>
//         /// <param name="sGroupName">Группа</param>
//         /// <returns>В случае успеха возвращает true</returns>
//         public static bool LDAPAddUserToGroup(string sUserName, string sGroupName, out string err)
//         {

//         SearchResult user = LDAPFindOne("", sUserName, LdapFilter.UsersSAN);
//         SearchResult group = LDAPFindOne("", sGroupName, LdapFilter.Groups);
        
//             try
//             {
//                 DirectoryEntry de_group = group.GetDirectoryEntry();

//                 string userDN = user.Properties["distinguishedName"][0].ToString();
//                 string gpDN = de_group.Properties["distinguishedName"][0].ToString();

//                 //  if (!de_group.Properties["member"].Contains(userDN))
//                 if (!user.Properties["memberOf"].Contains(gpDN))
//                 {
//                     de_group.Properties["member"].Add(userDN);
//                     de_group.CommitChanges();

//                     err = string.Empty;
//                     return true;
//                 }
//                 else {
//                     err = "Уже состоит в данной группе";
//                     return false;
                
//                 }
//             }
//             catch (Exception e)
//             {
//                 err = e.Message;
//                 return false;
//             }
//         }

//         /// <summary>
//         /// Удаляет пользователя из указанной группы
//         /// </summary>
//         /// <param name="sUserName">UserPrincipal добавляемого пользователя</param>
//         /// <param name="sGroupName">Группа</param>
//         /// <returns>В случае успеха возвращает true</returns>
//         public static bool LDAPRemoveUserFromGroup(string sUserName, string sGroupName, out string err)
//         {

//             SearchResult user = LDAPFindOne("", sUserName, LdapFilter.UsersSAN);
//             SearchResult group = LDAPFindOne("", sGroupName, LdapFilter.Groups);

//             try
//             {
//                 DirectoryEntry de_group = group.GetDirectoryEntry();

//                 string userDN = user.Properties["distinguishedName"][0].ToString();
//                 string gpDN = de_group.Properties["distinguishedName"][0].ToString();
                

//                 // if (de_group.Properties["member"].Contains(userDN))
//                 if(user.Properties["memberOf"].Contains(gpDN))
//                 {
//                     de_group.Properties["member"].Remove(userDN);
//                     de_group.CommitChanges();

//                     err = string.Empty;
//                     return true;
//                 }
//                 else
//                 {
//                     err = "Не состоит в данной группе";
//                     return false;

//                 }
//             }
//             catch (Exception e)
//             {
//                 err = e.Message;
//                 return false;
//             }
//         }


//         /// <summary>
//         /// Удаляет пользователя из указанной группы
//         /// </summary>
//         /// <param name="sUserName">Имя удаляемого пользователя</param>
//         /// <param name="sGroupName">Группа</param>
//         /// <returns>Возвращает true в случае успеха</returns>
//         public static bool RemoveUserFromGroup(string sUserName, string sGroupName)
//         {
//             try
//             {
//                 using (UserPrincipal oUserPrincipal = GetUser(sUserName))
//                 using (GroupPrincipal oGroupPrincipal = GetGroup(sGroupName))
//                 {
//                     if (oUserPrincipal != null && oGroupPrincipal != null)
//                     {
//                         if (IsUserGroupMember(sUserName, sGroupName))
//                         {
//                             oGroupPrincipal.Members.Remove(oUserPrincipal);
//                             oGroupPrincipal.Save();
//                         }
//                     }
//                 }
//                 return true;
//             }
//             catch
//             {
//                 return false;
//             }
//         }


//         /// <summary>
//         /// Удаляет пользователя из указанной группы
//         /// </summary>
//         /// <param name="sUserName">Имя удаляемого пользователя</param>
//         /// <param name="sGroupName">Группа</param>
//         /// <returns>Возвращает true в случае успеха</returns>
//         public static bool LDAPRemoveUserFromGroup(string sUserName, string sGroupName)
//         {

//             SearchResult user = LDAPFindOne("", sUserName, LdapFilter.UsersSAN);
//             SearchResult group = LDAPFindOne("", sGroupName, LdapFilter.Groups);

//             try
//             {
//                 string userDN = user.Properties["distinguishedName"][0].ToString();
//                 DirectoryEntry de_group = group.GetDirectoryEntry();

//                 de_group.Properties["member"].Remove(userDN);
//                 de_group.CommitChanges();

            
//                 return true;
//             }
//             catch (Exception e)
//             {
               
//                 return false;
//             }
//         }



//         /// <summary>
//         /// Удаляет компьютер из указанной группы (PrincipalContext должен определен за ранее)
//         /// </summary>
//         /// <param name="sUserName">Имя удаляемого компьютера/param>
//         /// <param name="sGroupName">Группа</param>
//         /// <returns>Возвращает true в случае успеха</returns>
//         public static bool RemoveComputerFromGroup(ComputerPrincipal sCompeterPrincipal, GroupPrincipal oGroupPrincipal)
//         {
//             try
//             {
               

//                 if (sCompeterPrincipal != null && oGroupPrincipal != null)
//                     {
//                         if (sCompeterPrincipal.IsMemberOf(oGroupPrincipal))
//                         {
//                             oGroupPrincipal.Members.Remove(sCompeterPrincipal);
//                             oGroupPrincipal.Save();
//                         }
//                     }
                
//                 return true;
//             }
//             catch
//             {
//                 return false;
//             }
//         }

//         /// <summary>
//         /// Проверка на вхождение пользователя в группу
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <param name="sGroupName">Группа</param>
//         /// <returns>Возвращает true, если пользователь входит в группу</returns>
//         public static bool IsUserGroupMember(string sUserName, string sGroupName)
//         {
//             bool bResult = false;

//             using (UserPrincipal oUserPrincipal = GetUser(sUserName))
//             using (GroupPrincipal oGroupPrincipal = GetGroup(sGroupName))
//             {
//                 if (oUserPrincipal != null && oGroupPrincipal != null)
//                 {
//                     bResult = oGroupPrincipal.Members.Contains(oUserPrincipal);
//                 }
//             }

//             return bResult;
//         }

//         /// <summary>
//         /// Возвращает пользователей находящихся в группе (более 1500 членов)
//         /// </summary>
//         /// <param name="sGroupName">Имя группы</param>
//         /// <returns>Возвращает List со всеми пользователями группы</returns>
//         public static List<string> ListMembersGroup1500(string sGroupName)
//         {
//             List<string> myItems = new List<string>();
//             SearchResult group = LDAPFindOne("", sGroupName, LdapFilter.Groups);
//             string gpDN = group.Properties["distinguishedName"][0].ToString();

//             using (DirectoryEntry DE = new DirectoryEntry(gpDN))
//             {
//                 IADsMembers groupMembers = (IADsMembers)DE.Invoke("members", null);
                
//                 foreach (object groupMember in groupMembers)
//                 {
//                     IADs user = (IADs)groupMember;

//                     myItems.Add(user.Name);
                   
//                 }
//             }

//             return myItems;
//         }

//         /// <summary>
//         /// Возвращает список групп, в которых состоит пользователь
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <returns>Возвращает List со всеми группами пользователя</returns>
//         /*public static List<string> GetUserGroups(string sUserName)
//         {
//             List<string> myItems = new List<string>();
//             UserPrincipal oUserPrincipal = GetUser(sUserName);
//             if (oUserPrincipal != null)
//             {
//                 using (PrincipalSearchResult<Principal> oPrincipalSearchResult = oUserPrincipal.GetGroups())
//                 {
//                     foreach (Principal oResult in oPrincipalSearchResult)
//                     {
//                         myItems.Add(oResult.Name);
//                     }
//                 }
            
//             }  else return null;

//             return myItems;
//         }*/

//         public static List<string> GetUserGroups(string sUserName)
//         {
//             UserPrincipal oUserPrincipal = GetUser(sUserName);
//             return GetUserGroups(oUserPrincipal);
//         }

//         /// <summary>
//         /// Возвращает список групп, в которых состоит пользователь
//         /// </summary>
//         /// <param name="sUserPrincipal"> UserPrincipal пользователя</param>
//         /// <returns>Возвращает List со всеми группами пользователя</returns>
//         public static List<string> GetUserGroups(UserPrincipal sUserPrincipal)
//         {
//             List<string> myItems = new List<string>();
//             PrincipalContext oPrincipalContext = GetPrincipalContext();
//             if (sUserPrincipal != null)
//             {
//                 try
//                 {

//                 using (PrincipalSearchResult<Principal> oPrincipalSearchResult = sUserPrincipal.GetGroups(oPrincipalContext))
//                 {
                    
//                         foreach (Principal oResult in oPrincipalSearchResult)
//                         {
//                             myItems.Add(oResult.Name);
//                         }
//                     }
                    
//                 }

//                 catch { return null; }
//             }
//             else return null;

//             return myItems;
//         }

//         /// <summary>
//         /// Возвращает список групп, в которых состоит пользователь
//         /// </summary>
//         /// <param name="sUserPrincipal"> UserPrincipal пользователя</param>
//         /// <returns>Возвращает List со всеми группами пользователя</returns>
//         public static List<string> LDAPGetUserGroups(string sUserName)
//         {
//             List<string> myItems = new List<string>();

//             SearchResult objSearch = LDAPFindOne("",sUserName,LdapFilter.UsersSAN);

//             if (objSearch != null)
//             {
//                 var gp = objSearch.Properties["memberOf"];


//                 if (gp.Count > 0)
//                 {

//                     foreach (string r in gp)
//                     {
//                         string gp_name = r.Substring(3, r.IndexOf(",") - 3);
//                         myItems.Add(gp_name);

//                     }

//                 }

           
//             }
//             else return null;

//             return myItems;
//         }


//         /// <summary>
//         /// Возвращает список групп, в которых состоит пользователь
//         /// </summary>
//         /// <param name="sUserPrincipal">Имя пользователя</param>
//         /// <returns>Возвращает List со всеми группами пользователя</returns>
//         public static List<string> GetUserPermittedWorkstations(UserPrincipal sUserPrincipal)
//         {
//             List<string> myItems = new List<string>();
//             if (sUserPrincipal != null)
//             {
//                 PrincipalValueCollection<string> oPrincipalResult = sUserPrincipal.PermittedWorkstations;
                
//                     foreach (var oResult in oPrincipalResult)
//                     {
//                         myItems.Add(oResult);
//                     }
                

//             }
//             else return null;

//             return myItems;
//         }

//         /// <summary>
//         /// Возвращает список групп, в которых состоит пользователь
//         /// </summary>
//         /// <param name="sUserPrincipal">Имя пользователя</param>
//         /// <returns>Возвращает List со всеми группами пользователя</returns>
//         public static List<string> LDAPGetUserPermittedWorkstations(string sUserName)
//         {
//             List<string> myItems = new List<string>();

//             SearchResult objSearch = LDAPFindOne("", sUserName, LdapFilter.UsersSAN);

//             if (objSearch != null)
//             {
                
//                 var wrs_rez = objSearch.Properties["userWorkstations"];

//                 if (wrs_rez.Count>0)
//                 {
//                 string[] wrs = wrs_rez[0].ToString().Split(',');


//                 if (wrs.Count() > 0)
//                 {

//                     foreach (string wr in wrs)
//                     {
                        
//                         myItems.Add(wr);

//                     }

//                 }
//                 }
//                 else return null;

//             }
//             else return null;

//             return myItems;
//         }

       

//         /// <summary>
//         /// Возвращает список групп GroupPrincipal, в которых состоит пользователь
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <returns>Возвращает List GroupPrincipal со всеми группами пользователя</returns>
//         public static List<GroupPrincipal> GetUserGroupsPrincipal(string sUserName)
//         {
//             UserPrincipal oUserPrincipal = GetUser(sUserName);
//             return GetUserGroupsPrincipal(oUserPrincipal);
//         }

//         /// <summary>
//         /// Возвращает список групп GroupPrincipal, в которых состоит пользователь
//         /// </summary>
//         /// <param name="sUserName">UserPrincipal пользователя</param>
//         /// <returns>Возвращает List GroupPrincipal со всеми группами пользователя</returns>
//         public static List<GroupPrincipal> GetUserGroupsPrincipal(UserPrincipal sUserPrincipal)
//         {
//             List<GroupPrincipal> myItems = new List<GroupPrincipal>();
//             PrincipalContext oPrincipalContext = GetPrincipalContext();
            
//             if (sUserPrincipal != null)
//             {
//                 using (PrincipalSearchResult<Principal> oPrincipalSearchResult = sUserPrincipal.GetGroups(oPrincipalContext))
//                 {
//                     foreach (GroupPrincipal oResult in oPrincipalSearchResult)
//                     {
//                         myItems.Add(oResult);
//                     }

//                     return myItems;
//                 }

//             }
//             else return null;



//         }



//         /// <summary>
//         /// Возвращает список авторизационных групп пользователя
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <returns>Возвращает List<string> содержащий авторизационные группы пользователя</returns>
//         public static List<string> GetUserAuthorizationGroups(string sUserName)
//         {
//             List<string> myItems = new List<string>();
//             PrincipalContext oPrincipalContext = GetPrincipalContext();
//             UserPrincipal oUserPrincipal = GetUser(sUserName);

//             using (PrincipalSearchResult<Principal> oPrincipalSearchResult = oUserPrincipal.GetAuthorizationGroups())
//             {
//                 foreach (Principal oResult in oPrincipalSearchResult)
//                 {
//                     myItems.Add(oResult.Name);
//                 }
//             }

//             return myItems;
//         }


//         /// <summary>
//         /// Возвращает список авторизационных групп пользователя
//         /// </summary>
//         /// <param name="sUserPrincipal"> UserPrincipal пользователя</param>
//         /// <returns>Возвращает List содержащий авторизационные группы пользователя</returns>
//         public static List<string> GetUserAuthorizationGroups(UserPrincipal sUserPrincipal)
//         {
//             List<string> myItems = new List<string>();

//         try{
//             using (PrincipalSearchResult<Principal> oPrincipalSearchResult = sUserPrincipal.GetAuthorizationGroups())
//             {
                
//                     foreach (Principal oResult in oPrincipalSearchResult)
//                     {
//                         myItems.Add(oResult.Name);
//                     }
               
//             }
           
//         }
//         catch { return null; }
        
//         return myItems;
//         }



       
//         #endregion

//         #region Вспомогательные методы


//         /// <summary>
//         /// Получает DirectoryEntry деректории OU
//         /// </summary>
//         /// <param name="ou"> путь к OU</param>
//         /// <returns>Возвращает DirectoryEntry деректории OU</returns>
//         public static DirectoryEntry GetOU(string ou)
//         {
//             try
//             {
//                 DirectoryEntry dr = new DirectoryEntry("LDAP://" + sDomain + "/" + ou, sServiceUser, sServicePassword);
//                 dr.RefreshCache();
//                 return dr;
//             }
//             catch
//             {
//                 return null;
//             }

//             /* var result=LDAPFindOne(sDefaultRootOU, "(&(objectClass = oraganizationalUnit)("+ou+"))");
//              if (result != null)
//              {
//                  return result.GetDirectoryEntry();
//              }
//              else return null;
//          */

//         }

        
//         /// <summary>
//         /// Попытка получить базовый основной контекст
//         /// </summary>
//         /// <returns>Возвращает объект PrincipalContext</returns>
//         public static PrincipalContext TryGetPrincipalContext(out string sMessage)
//         {
//             PrincipalContext result;
//             try 
//             {
//                result = new PrincipalContext(ContextType.Domain, sDomain, sServiceUser, sServicePassword);
//                sMessage=string.Empty;
//                return result;

//             }catch (Exception ex)
//             {
//                 sMessage = ex.Message;
           
//                 return null;
//             }

            
//         }

//         /// <summary>
//         /// Попытка получить базовый основной контекст
//         /// </summary>
//         /// <param name="sUserName">Имя пользователя</param>
//         /// <param name="sPassword">Пароль</param>
//         /// <returns>Возвращает объект PrincipalContext</returns>
       
//         public static PrincipalContext TryGetPrincipalContext(string sUserName, string sPassword,out string sMessage)
//         {
//             PrincipalContext result;
//             try
//             {
//                 result = new PrincipalContext(ContextType.Domain, sDomain, sUserName, sPassword);
//                 sMessage = string.Empty;
//                 return result;

//             }
//             catch (Exception ex)
//             {
//                 sMessage = ex.Message;

//                 return null;
//             }


//         }


//         /// <summary>
//         /// Получить основной контекст указанного OU
//         /// </summary>
//         /// <param name="sOU">OU для которого нужно получить основной контекст</param>
//         /// <returns>Возвращает объект PrincipalContext</returns>
//         public static PrincipalContext GetPrincipalContext(string sOU="")
//         {
//             if (string.IsNullOrEmpty(sOU)) return new PrincipalContext(ContextType.Domain, sDomain, sServiceUser, sServicePassword);
//             else 
//                 return new PrincipalContext(ContextType.Domain, sDomain, sOU, sServiceUser, sServicePassword);
//         }

//         /// <summary>
//         /// Возвращает найденные обьекты из АД согласно фильтру
//         /// </summary>
//         /// <param name="ou">Место поиска</param>
//         /// <param name="Filter">Параметры фильтра</param>
//         /// <returns>Возвращает SearchResultCollection</returns>
//         public static SearchResultCollection LDAPFindAll(string ou, string Filter)
//         {
//             if (enabl) return null;

//             if (ou == "")
//             {
//                 ou = sDefaultRootOU;
//             }

//             try
//             {
//                 var domainPath = @"LDAP://" + sDomain + "/" + ou;
//                 var directoryEntry = new DirectoryEntry(domainPath, sServiceUser, sServicePassword);
//                 var dirSearcher = new DirectorySearcher(directoryEntry);
//                 dirSearcher.SearchScope = SearchScope.Subtree;
//                 dirSearcher.PageSize = 100;
//                 dirSearcher.SizeLimit = 5000;
//                 dirSearcher.Filter = Filter;

//                 return dirSearcher.FindAll();
//             }
//             catch { return null; }
//         }

//         /// <summary>
//         /// Возвращает найденные обьекты из АД согласно фильтру
//         /// </summary>
//         /// <param name="ou">Место поиска</param>
//         /// <param name="obj">имя объекта</param>
//         /// <param name="ldf">LdapFilter:  выбор обьекта</param>
//         /// <returns>Возвращает SearchResultCollection</returns>
//         public static SearchResultCollection LDAPFindAll(string ou, string obj, LdapFilter ldf)
//         {
//             string filter = "";


//             switch (ldf)
//             {
//                 case LdapFilter.Computers: filter = "(&(objectCategory=computer)(name=" + obj + "))"; break;
//                 case LdapFilter.OU: filter = "(objectCategory=organizationalUnit)"; break;//----------!!!!!!!!
//                 case LdapFilter.UsersSAN: filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + obj + "))"; break;
//                 case LdapFilter.UsersCN: filter = "(&(objectCategory=person)(objectClass=user)(CN=" + obj + "*))"; break;
//                 case LdapFilter.UsersName: filter = "(&(objectCategory=person)(objectClass=user)(name=" + obj + "*))"; break;
//                 case LdapFilter.Groups: filter = "(&(objectCategory=group)(name=" + obj + ")) "; break;
//             }

//             return LDAPFindAll(ou, filter);
//         }




//         /// <summary>
//         /// Возвращает найденный обьект из АД согласно фильтру
//         /// </summary>
//         /// <param name="ou">Место поиска</param>
//         /// <param name="Filter">Параметры фильтра</param>
//         /// <returns>Возвращает SearchResult</returns>
//         public static SearchResult LDAPFindOne(string ou, string Filter, string user = null, string password = null)
//         {

//             if (enabl) return null;

//             if (ou == "")
//             {
//                 ou = sDefaultRootOU;
//             }

//             var domainPath = @"LDAP://"+ sDomain+"/"+ ou;
//             DirectoryEntry directoryEntry; 

//             if (String.IsNullOrEmpty(user) || String.IsNullOrEmpty(password))
//             {
//                directoryEntry = new DirectoryEntry(domainPath, sServiceUser, sServicePassword);
//             }else 
//             {
//                directoryEntry = new DirectoryEntry(domainPath, user, password);
//             }

//             var dirSearcher = new DirectorySearcher(directoryEntry);
//             dirSearcher.SearchScope = SearchScope.Subtree;
//             dirSearcher.PageSize = 100;
//             dirSearcher.SizeLimit = 5000;
//             dirSearcher.Filter = Filter;

//             try
//             {

//                 return dirSearcher.FindOne();
//             }
//             catch(Exception erousr) 
//             {
//                 return null;
//             }

//         }


//         /// <summary>
//         /// Возвращает найденный обьект из АД под УЗ по умолчанию
//         /// </summary>
//         /// <returns>Возвращает SearchResult</returns>
//         public static SearchResult LDAPFindUserMe(string user)
//         {

//             var domainPath = @"LDAP://" + sDomain + "/" + sDefaultRootOU;
//             DirectoryEntry directoryEntry;

     
//           directoryEntry = new DirectoryEntry(domainPath);
     
//             var dirSearcher = new DirectorySearcher(directoryEntry);
//             dirSearcher.SearchScope = SearchScope.Subtree;
//             dirSearcher.Filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + user + "))";

//             try
//             {

//                 return dirSearcher.FindOne();
//             }
//             catch (Exception erousr)
//             {
//                 return null;
//             }

//         }

//         /// <summary>
//         /// Возвращает найденный обьект из АД согласно выбора
//         /// </summary>
//         /// <param name="ou">Место поиска</param>
//         /// <param name="obj">имя объекта</param>
//         /// <param name="ldf">LdapFilter:  выбор обьекта</param>
//         /// <returns>Возвращает SearchResult</returns>
//         public static SearchResult LDAPFindOne(string ou, string obj, LdapFilter ldf, string user = null, string password = null)
//         {
//             string filter="";
             
            
//             switch(ldf)
//             {
//                 case LdapFilter.Computers : filter="(&(objectCategory=computer)(name=" + obj + "))";break;
//                 case LdapFilter.OU : filter="(objectCategory=organizationalUnit)";break;//----------!!!!!!!!
//                 case LdapFilter.UsersSAN: filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + obj + "))"; break;
//                 case LdapFilter.UsersCN: filter = "(&(objectCategory=person)(objectClass=user)(CN=" + obj + "))"; break;
//                 case LdapFilter.Groups: filter = "(&(objectCategory=group)(name=" + obj + ")) "; break;
//             }

//             return LDAPFindOne(ou, filter,user,password); 
//         }


//       public static bool GetCantChangePassword(SearchResult results)
//         {
//             bool cantChange = false;
//             try
//             {

//                 if (results != null)
//                 {
//                     try
//                     {
//                         DirectoryEntry user = results.GetDirectoryEntry();
//                         ActiveDirectorySecurity userSecurity = user.ObjectSecurity;
//                         SecurityDescriptor sd = (SecurityDescriptor)user.Properties["ntSecurityDescriptor"].Value;
//                         AccessControlList oACL = (AccessControlList)sd.DiscretionaryAcl;
                     
//                         bool everyoneCantChange = false;
//                         bool selfCantChange = false;

//                         foreach (ActiveDs.AccessControlEntry ace in oACL)
//                         {
//                             try
//                             {
//                                 if (ace.ObjectType.ToUpper().Equals("{AB721A53-1E2F-11D0-9819-00AA0040529B}".ToUpper()))
//                                 {
//                                     if (ace.Trustee.Equals("Everyone") && (ace.AceType == (int)ADS_ACETYPE_ENUM.ADS_ACETYPE_ACCESS_DENIED_OBJECT))
//                                     {
//                                         everyoneCantChange = true;
//                                     }
//                                     if (ace.Trustee.Equals(@"NT AUTHORITY\SELF") && (ace.AceType == (int)ADS_ACETYPE_ENUM.ADS_ACETYPE_ACCESS_DENIED_OBJECT))
//                                     {
//                                         selfCantChange = true;
//                                     }
//                                 }
//                             }
//                             catch (NullReferenceException ex)
//                             {
                                
//                             }
//                             catch (Exception ex)
//                             {
                             
//                             }
//                         }


//                         if (everyoneCantChange || selfCantChange)
//                         {
//                             cantChange = true;
//                         }
//                         else
//                         {
//                             cantChange = false;
//                         }

//                         user.Close();
//                     }
//                     catch (Exception ex)
//                     {
                        
//                     }
//                 }
           
//             }
//             catch (Exception ex)
//             {
                
//             }
//             return cantChange;
//         }



//         #endregion
//     }

    
// }


// /// 

//     /// Структура данных пользователя
//     /// 

//     public struct UserProperty
//     {
//         public string sn;
//         public string givenname;
//         public string displayname;
//         public string description;
//         public string physicaldeliveryofficename;
//         public string title;
//         public string department;
//         public string company;
//         public string manager;
//         public string scriptPath;
//         public string telephoneNumber;
//         public string mail;
//         public string name;
//         public string homeDrive;
//         public string homeDirectory;
//         public string userworkstations;
//     }

   
//     public struct UserPropertyExtend
//     {
//         public string samaccountname;
//         public string cn;
//         public string sn;
//         public string givenname;
//         public string displayname;
//         public string description;
//         public string physicaldeliveryofficename;
//         public string title;
//         public string department;
//         public string company;
//         public string manager;
//         public string manager_distinguishedname;
//         public string scriptPath;
//         public string telephonenumber;
//         public string mail;
//         public string name;
//         public string distinguishedname;
//         public bool userdisable;
//         public bool usrpassworddenied;//Установлен параметр "Запрет смены пароля"
//         public bool passwordneverexpires;
//         public string coment;
//         public string userworkstations;
//         public bool usrexpires; //Истек срок действия УЗ
//         public long tic_usrexpires; //Значение истечения срока действия
//         public long loginnever; //Время последнего логина "Никогда"
//     }

//     public struct GroupProperty
//     {
//         public string samaccountname;
//         public string description;
//         public string name;
//         public string cn;
//         public string distinguishedname;
//         public int grouptype;
//         public string coment;
//         public string managedby;
//     }

//     public enum LdapFilter { UsersSAN, UsersCN, Computers, Groups, OU, UsersName };