using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Oct.Framework.Core.ApiData;
using Oct.Framework.Core.Cache;
using Oct.Framework.Core.Common;
using Oct.Framework.Core.Cookie;
using Oct.Framework.Core.Export;
using Oct.Framework.Core.Image;
using Oct.Framework.Core.IOC;
using Oct.Framework.Core.Json;
using Oct.Framework.Core.Reflection;
using Oct.Framework.Core.Security;
using Oct.Framework.Core.Session;
using Oct.Framework.Core.Xml;
using Oct.Framework.DB.Base;
using Oct.Framework.DB.Interface;
using Oct.Framework.Entities;
using Oct.Framework.MvcExt.Base;
using Oct.Framework.MvcExt.User;

namespace Oct.Framework.MvcExt.Extisions
{
    public static class KernelExtision
    {
        /// <summary>
        /// 通过类型获取全部结果集生成的listitem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="knl"></param>
        /// <param name="where"></param>
        /// <param name="select"></param>
        /// <param name="addall"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetSelectListItems<T>(this IKernel knl,
          string where,
            Func<T, SelectListItem> select, bool addall = false) where T : BaseEntity<T>, new()
        {
            DbContext context = new DbContext();
            var repo = context.GetContext<T>();
            var data = repo.Query(where).Select(select).ToList();
            if (addall)
            {
                data.Insert(0, new SelectListItem { Text = "-全部-", Value = string.Empty });
            }
            return data;
        }

        /// <summary>
        /// 通过类型获取全部结果集生成的listitem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="knl"></param>
        /// <param name="where"></param>
        /// <param name="paras"></param>
        /// <param name="select"></param>
        /// <param name="addall"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetSelectListItems<T>(this IKernel knl,
          string where, IDictionary<string, object> paras,
            Func<T, SelectListItem> select, bool addall = false) where T : BaseEntity<T>, new()
        {
            DbContext context = new DbContext();
            var repo = context.GetContext<T>();
            var data = repo.Query(where, paras).Select(select).ToList();
            if (addall)
            {
                data.Insert(0, new SelectListItem { Text = "-全部-", Value = string.Empty });
            }
            return data;
        }

        #region 上传Excel
        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="fileName">Excel文件路径名</param>
        public static DataSet ImportXlsToData(this IKernel knl, string fileName)
        {
            try
            {
                if (fileName == string.Empty)
                {
                    throw new ArgumentNullException("Excel文件上传失败！");
                }

                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.ACE.OLEDB.12.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += fileName;
                oleDBConnString += ";Extended Properties=Excel 8.0;";
                OleDbConnection oleDBConn = null;
                OleDbDataAdapter oleAdMaster = null;
                var m_tableName = new DataTable();
                var ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();
                m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (m_tableName != null && m_tableName.Rows.Count > 0)
                {

                    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString();

                }
                string sqlMaster;
                sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// <summary>
        /// 获取appsetting
        /// </summary>
        /// <param name="knl"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSetting(this IKernel knl, string key)
        {
            return ConfigSettingHelper.GetAppStr(key);
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="knl"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetQueryString(this IKernel knl, string name)
        {
            return knl.Request.QueryString[name];
        }

        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="knl"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(this IKernel knl, string str)
        {
            return Encypt.MD5(str);
        }

        /// <summary>
        /// 序列化xml
        /// </summary>
        /// <param name="knl"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(this IKernel knl, object obj)
        {
            return XmlHelper.Serialize(obj);
        }

        /// <summary>
        /// 反序列化xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="knl"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(this IKernel knl, string xml)
        {
            return XmlHelper.DeSerialize<T>(xml);
        }

        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="knl"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PostData(this IKernel knl, string url, string data)
        {
            return ApiDataHelper.PostData(url, data);
        }

        /// <summary>
        /// 获取string 数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetData(this IKernel knl, string url)
        {
            return ApiDataHelper.GetData(url);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savePath"></param>
        public static void DownLoadFile(this IKernel knl, string url, string savePath)
        {
            ApiDataHelper.DownLoadFile(url, savePath);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        public static void UpLoadFile(this IKernel knl, string url, string filePath)
        {
            ApiDataHelper.UpLoadFile(url, filePath);
        }


        /// <summary>
        /// 添加缓存
        /// </summary>
        public static bool AddCache<T>(this IKernel knl, string key, T v)
        {
            var cache = Bootstrapper.GetRepository<ICacheHelper>();
            return cache.Set<T>(key, v);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        public static bool AddCache<T>(this IKernel knl, string key, T v, int minute)
        {
            var cache = Bootstrapper.GetRepository<ICacheHelper>();
            return cache.Set<T>(key, v, minute);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        public static bool AddCache<T>(this IKernel knl, string key, T v, TimeSpan ts)
        {
            var cache = Bootstrapper.GetRepository<ICacheHelper>();
            return cache.Set<T>(key, v, ts);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        public static T GetCache<T>(this IKernel knl, string key)
        {
            var cache = Bootstrapper.GetRepository<ICacheHelper>();
            return cache.Get<T>(key);
        }

        /// <summary>
        /// 获取ip
        /// </summary>
        /// <param name="knl"></param>
        /// <returns></returns>
        public static string GetIP(this IKernel knl)
        {
            return IpHelper.GetIP();
        }

        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(this IKernel knl, string cookiename)
        {
            CookieHelper.ClearCookie(cookiename);
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookieValue(this IKernel knl, string cookiename)
        {
            return CookieHelper.GetCookieValue(cookiename);
        }

        /// <summary>
        /// 添加一个Cookie（24小时过期）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        public static void SetCookie(this IKernel knl, string cookiename, string cookievalue)
        {
            CookieHelper.SetCookie(cookiename, cookievalue);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(this IKernel knl, string cookiename, string cookievalue, DateTime expires)
        {
            CookieHelper.SetCookie(cookiename, cookievalue, expires);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(this IKernel knl, string cookiename, string cookievalue, string domain, DateTime expires)
        {
            CookieHelper.SetCookie(cookiename, cookievalue, domain, expires);
        }

        /// <summary>
        /// 导出excel  使用fileresult
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <returns></returns>
        public static byte[] RenderDataTableToExcel(this IKernel knl, DataTable sourceTable)
        {
            return ExportExcel.RenderDataTableToExcel(sourceTable);
        }

        public static DataTable RenderDataTableFromExcel(this IKernel knl, Stream ExcelFileStream, string SheetName, int HeaderRowIndex)
        {
            return ExportExcel.RenderDataTableFromExcel(ExcelFileStream, SheetName, HeaderRowIndex);
        }

        /// <summary>
        /// 获取dt
        /// </summary>
        /// <param name="ExcelFileStream"></param>
        /// <param name="SheetIndex"></param>
        /// <param name="HeaderRowIndex"></param>
        /// <returns></returns>
        public static DataTable RenderDataTableFromExcel(this IKernel knl, Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
        {
            return ExportExcel.RenderDataTableFromExcel(ExcelFileStream, SheetIndex, HeaderRowIndex);
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="path">excel文档路径</param>
        /// <returns></returns>
        public static DataTable RenderDataTableFromExcel(this IKernel knl, string path)
        {
            return ExportExcel.RenderDataTableFromExcel(path);
        }

        /// <summary>
        /// 转换图片类型
        /// </summary>
        /// <param name="OriFilename">源文件</param>
        /// <param name="DesiredFilename">目标文件</param>
        /// <returns></returns>
        public static bool ConvertImage(this IKernel knl, string OriFilename, string DesiredFilename)
        {
            return ImageHelper.ConvertImage(OriFilename, DesiredFilename);
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="OriFileName"></param>
        /// <param name="DesiredFilename"></param>
        /// <param name="IntWidth"></param>
        /// <param name="IntHeight"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static bool ChangeImageSize(this IKernel knl, string OriFileName, string DesiredFilename, int IntWidth, int IntHeight,
            ImageFormat imageFormat)
        {
            return ImageHelper.ChangeImageSize(OriFileName, DesiredFilename, IntWidth, IntHeight, imageFormat);
        }

        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="wtext"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool ImageWaterText(this IKernel knl, string wtext, string source, string target)
        {
            return ImageHelper.ImageWaterText(wtext, source, target);
        }

        /// <summary>
        /// 图片水印
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="waterPicSource"></param>
        /// <returns></returns>
        public static bool ImageWaterPic(this IKernel knl, string source, string target, string waterPicSource)
        {
            return ImageHelper.ImageWaterPic(source, target, waterPicSource);
        }

        /// <summary>
        /// 制作图片缩略图
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <param name="thumbnailPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode">HW(指定高宽缩放（可能变形） ),W（指定宽，高按比例 ）,H（指定高，宽按比例）,Cut（指定高宽裁减（不变形） ）,DB（等比缩放（不变形，如果高大按高，宽大按宽缩放） ）</param>
        /// <param name="type">JPG,BMP,GIF,PNG</param>
        public static void MakeThumbnail(this IKernel knl, string originalImagePath, string thumbnailPath, int width, int height,
            string mode, string type)
        {
            ImageHelper.MakeThumbnail(originalImagePath, thumbnailPath, width, height, mode, type);
        }

        /// <summary>
        /// 序列化json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(this IKernel knl, object obj)
        {
            return JsonHelper.SerializeObject(obj);
        }

        /// <summary>
        /// 反序列化json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this IKernel knl, string json) where T : class
        {
            return JsonHelper.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 新增session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddSession(this IKernel knl, string key, object value)
        {
            var session = Bootstrapper.GetRepository<ISessionProvider>();
            return session.AddSession(key, value);
        }

        /// <summary>
        /// 添加session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static bool AddSession(this IKernel knl, string key, object value, int timeOut)
        {
            var session = Bootstrapper.GetRepository<ISessionProvider>();
            return session.AddSession(key, value, timeOut);
        }


        /// <summary>
        /// 获取session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetSession<T>(this IKernel knl, string key)
        {
            var session = Bootstrapper.GetRepository<ISessionProvider>();
            return session.GetSession<T>(key);
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public static UserBase GetCurrentUser(this IKernel knl)
        {
            return LoginHelper.Instance.GetLoginUser<UserBase>();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public static void Login(this IKernel knl, Func<UserBase> func)
        {
            LoginHelper.Instance.Login(func);
        }

        /// <summary>
        /// 缓存权限
        /// </summary>
        /// <returns></returns>
        public static void CacheRoles(this IKernel knl, Func<UserRoles> func)
        {
            LoginHelper.Instance.CacheRoles(func);
        }

        /// <summary>
        /// 缓存权限
        /// </summary>
        /// <returns></returns>
        public static UserRoles GetLoginUserRoles(this IKernel knl)
        {
            return LoginHelper.Instance.GetLoginUserRoles();
        }

        /// <summary>
        /// 创建页码实体
        /// </summary>
        /// <returns></returns>
        public static PageModel CreatePageModel(this IKernel knl, int size, int index, int count)
        {
            return new PageModel() { CurrentPage = index, PageSize = size, Total = count };
        }

        /// <summary>
        /// 获取枚举描述值
        /// </summary>
        /// <param name="knl"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetEnumValue(this IKernel knl, Enum e)
        {
            return EnumHelper.GetEnumDescription(e);
        }
    }
}
