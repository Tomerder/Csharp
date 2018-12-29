using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Toolkit
{
    public class StateSetter : Freezable
    {

        #region Binding Property
        public static readonly DependencyProperty BindingProperty =
                                    DependencyProperty.Register(
                                        "Binding",
                                        typeof(object),
                                        typeof(StateSetter),
                                        new PropertyMetadata(null, onBindingChanged));

        public object Binding
        {
            get
            {
                return (object)GetValue(BindingProperty);
            }
            set
            {
                SetValue(BindingProperty, value);
            }
        }

        private static void onBindingChanged(object s, DependencyPropertyChangedEventArgs e)
        {
            var sender = s as StateSetter;
            sender.onBindingChanged(e);
        }

        private void onBindingChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion

        #region Prefix Property
        public static readonly DependencyProperty PrefixProperty =
                                    DependencyProperty.Register(
                                        "Prefix",
                                        typeof(string),
                                        typeof(StateSetter),
                                        new PropertyMetadata("", onPrefixChanged));

        public string Prefix
        {
            get
            {
                return (string)GetValue(PrefixProperty);
            }
            set
            {
                SetValue(PrefixProperty, value);
            }
        }

        private static void onPrefixChanged(object s, DependencyPropertyChangedEventArgs e)
        {
            var sender = s as StateSetter;
            sender.onPrefixChanged(e);
        }

        private void onPrefixChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class StateSetterCollection : FreezableCollection<StateSetter>
    {
    }
}
