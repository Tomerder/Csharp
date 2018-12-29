using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Toolkit
{
    public static class ValueToState
    {

        #region Setters Attached Property
        public static readonly DependencyProperty SettersProperty =
                                    DependencyProperty.RegisterAttached(
                                        "SettersInternal",
                                        typeof(StateSetterCollection),
                                        typeof(ValueToState),
                                        new PropertyMetadata(null, onSettersChanged));

        public static StateSetterCollection GetSetters(FrameworkElement item)
        {
            var collection = (StateSetterCollection)item.GetValue(SettersProperty);
            if (collection == null)
            {
                collection = new StateSetterCollection();
                item.SetValue(SettersProperty, collection);
            }

            return collection;
        }

        public static void SetSetters(FrameworkElement item, StateSetterCollection value)
        {
            item.SetValue(SettersProperty, value);
        }

        private static void onSettersChanged(object s, DependencyPropertyChangedEventArgs e)
        {
            var sender = s as FrameworkElement;
            if (sender != null)
            {
                Evaluate(sender);
            }
        }

        #endregion


        internal static void Evaluate(FrameworkElement elem)
        {
            if (elem == null) return;
            var setters = GetSetters(elem);

            if (setters == null) return;

            if (!elem.IsLoaded)
            {
                elem.Loaded += (e, s) =>
                {
                    Evaluate(elem);
                };

                return;
            }

            foreach (var setter in setters)
            {
                var bindingText = (setter.Binding != null) ? setter.Binding.ToString() : "";
                var prefix = (setter.Prefix != null) ? setter.Prefix : "";
                var state = prefix + bindingText;

                var res = ExtendedVisualStateManager.GoToElementState(elem, state, true);
            }

        }
    }
}
