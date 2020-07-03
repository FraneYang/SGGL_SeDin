using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Model
{
    public class BeanUtil
    {
        public static T CopyOjbect<T>(T obj,bool hasProperty)
        {
            if (obj == null)
            {
                return obj;
            }
            T targetDeepCopyObj;
            Type targetType = obj.GetType();
            //值类型  
            if (targetType.IsValueType == true)
            {
                targetDeepCopyObj = obj;
            }
            //引用类型   
            else
            {
                targetDeepCopyObj = (T)System.Activator.CreateInstance(targetType);   //创建引用对象   
                System.Reflection.MemberInfo[] memberCollection = obj.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (System.Reflection.MemberInfo member in memberCollection)
                {
                    //拷贝字段
                    if (member.MemberType == System.Reflection.MemberTypes.Field)
                    {
                        System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)member;
                        Object fieldValue = field.GetValue(obj);
                        
                        if (fieldValue is ICloneable)
                        {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                        }
                        else if (fieldValue!=null&&fieldValue.GetType() == typeof(DateTime))
                        {
                            field.SetValue(targetDeepCopyObj, fieldValue);
                        }
                        else if (fieldValue != null && fieldValue.GetType() == typeof(Boolean))
                        {
                            field.SetValue(targetDeepCopyObj, fieldValue);
                        }

                    }//拷贝属性
                    else if (member.MemberType == System.Reflection.MemberTypes.Property)
                    {
                        System.Reflection.PropertyInfo myProperty = (System.Reflection.PropertyInfo)member;
                        MethodInfo info = myProperty.GetSetMethod(false);
                        if (info != null&&info.DeclaringType.IsValueType)
                        {
                            try
                            {
                                object propertyValue = myProperty.GetValue(obj, null);
                                if (propertyValue is ICloneable)
                                {
                                    myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                                }
                                else
                                {
                                  //  myProperty.SetValue(targetDeepCopyObj, CopyOjbect(propertyValue,hasProperty), null);
                                }
                            }
                            catch (System.Exception ex)
                            {
                                return obj;
                            }
                        }
                    }
                }
            }
            return targetDeepCopyObj;
        }
    }
}