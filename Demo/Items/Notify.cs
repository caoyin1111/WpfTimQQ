using Demo.Expends;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Items
{
    /// <summary>
    /// 通知类
    /// </summary>
    public class Notify:INotifyPropertyChanged
    {
        /// <summary>
        /// 堆栈信息
        /// </summary>
        private StackTrace trace = new StackTrace();
        /// <summary>
        /// 属性变更
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性变更
        /// </summary>
        /// <param name="name">变更的属性名称</param>
        protected void Changed(string name = null)
        {
            if (name == null)
            {
                name = new StackFrame(1).GetMethod().Name.Replace("set_", "");
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        /// <summary>
        /// 属性变更
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="expression"></param>
        protected void Changed<T, V>(Expression<Func<T, V>> expression)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(expression.ToPropertyName()));
        }
      
    }
   
}
