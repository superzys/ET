using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    public static class ReflexCopyData
    {
        /// <summary>
        /// 纯数据的复制， 属性必须是 public int XXXX;格式
        /// 赋值到 PropertyInfo 也就是  public int XXXX{get;set;};格式
        /// </summary>
        /// <param name="tarObj"></param>
        /// <param name="sourceObj"></param>
        public static void CopyDataToObj(object tarObj, object sourceObj)
        {
            if (tarObj != null && sourceObj != null)
            {
                Type dataBeanType = tarObj.GetType();
                Type sourceType = sourceObj.GetType();
                FieldInfo[] PropsArr = sourceType.GetFields();
                foreach (System.Reflection.FieldInfo p in PropsArr)
                {
                    //                  Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(data));

                    PropertyInfo prop = dataBeanType.GetProperty(p.Name, BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null)
                    {
                        if (p.FieldType == prop.PropertyType)
                        {
                            object val = Convert.ChangeType(p.GetValue(sourceObj), prop.PropertyType);

                            prop.SetValue(tarObj, val, null);
                        }
                        else
                        {
//                            Log.Info($"不同类型想赋值{p.Name} {p.FieldType } 到 {prop.PropertyType}");
                        }
                    }
                }
            }
            else
            {
                Log.Info($"tarObj {tarObj} and  sourceObj {sourceObj} ");
            }
        }
        /// <summary>
        /// 复制通用组件数据， 属性必须是 public int XXXX{get;set;};格式
        /// 赋值到PropertyInfo 属性
        /// </summary>
        /// <param name="tarObj"></param>
        /// <param name="sourceObj"></param>
        public static void CopyEntityToObj(object tarObj, object sourceObj)
        {
            if (tarObj != null && sourceObj != null)
            {
                Type dataBeanType = tarObj.GetType();
                Type sourceType = sourceObj.GetType();
                PropertyInfo[] PropsArr = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);//不管父类的
                foreach (System.Reflection.PropertyInfo p in PropsArr)
                {
                    //                  Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(data));
                    if (p.Name != "Id")
                    {
                        PropertyInfo prop = dataBeanType.GetProperty(p.Name, BindingFlags.Public | BindingFlags.Instance);
                        if (prop != null)
                        {
                            if (p.PropertyType == prop.PropertyType)
                            {
                                object val = Convert.ChangeType(p.GetValue(sourceObj), prop.PropertyType);

                                prop.SetValue(tarObj, val, null);
                            }
                            else
                            {
//                                Log.Info($"不同类型想赋值{p.Name} {p.PropertyType } 到 {prop.PropertyType}");
                            }
       
                        }
                    }
                }
            }
            else
            {
                Log.Info($"tarObj {tarObj} and  sourceObj {sourceObj} ");
            }
        }
        /// <summary>
        /// 游戏类转换通讯类
        /// </summary>
        /// <param name="tarObj"></param>
        /// <param name="sourceObj"></param>
        public static void CopyPropertysToFieldObj(object tarObj, object sourceObj)
        {
            if (tarObj != null && sourceObj != null)
            {
                Type dataBeanType = tarObj.GetType();
                Type sourceType = sourceObj.GetType();
                PropertyInfo[] PropsArr = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);//不管父类的
                foreach (System.Reflection.PropertyInfo p in PropsArr)
                {
                    //                  Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(data));
                    if (p.Name != "Id")
                    {
                        FieldInfo prop = dataBeanType.GetField(p.Name, BindingFlags.Public | BindingFlags.Instance);
                        if (prop != null)
                        {
                            if (p.PropertyType == prop.FieldType)
                            {
                                object val = Convert.ChangeType(p.GetValue(sourceObj), prop.FieldType);

//                                prop.SetValue(tarObj, val, null);
                                prop.SetValue(tarObj, val);
                            }
                            else
                            {
//                                Log.Info($"不同类型想赋值{p.Name} {p.PropertyType } 到 {prop.FieldType}");
                            }

                        }
                    }
                }
            }
            else
            {
                Log.Info($"tarObj {tarObj} and  sourceObj {sourceObj} ");
            }
        }

        //        public static void CopyDataToObj<T, K>(T tarObj, K sourceObj)
        //        {
        //            Type dataBeanType = typeof(T);
        //            PropertyInfo[] PropsArr = sourceObj.GetType().GetProperties();
        //            foreach (System.Reflection.PropertyInfo p in PropsArr)
        //            {
        //                //                  Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(data));
        //                if (p.Name != "Id")
        //                {
        //                    PropertyInfo prop = dataBeanType.GetProperty(p.Name, BindingFlags.Public | BindingFlags.Instance);
        //
        //                    object val = Convert.ChangeType(p.GetValue(sourceObj), prop.PropertyType);
        //
        //                    prop.SetValue(tarObj, val, null);
        //                }
        //            }
        //        }
    }
}
