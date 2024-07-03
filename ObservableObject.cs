using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WyyMusicConvertGui
{
    public class ObservableObject : INotifyPropertyChanged
    {
        //当对象的属性更改时，订阅此事件的其他对象可以得到通知
        public event PropertyChangedEventHandler? PropertyChanged;

        //当对象的属性更改时，调用此方法来通知订阅者
        public void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //首先检查提供的新值是否与字段的当前值相等，没改返回false,若不相等，更新新字段的值，然后触发属性更改，返回true
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
