using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryLib
{
    static public class RegistryLib
    {
        //-----------------------------------------------------------
        // APIs
        //-----------------------------------------------------------

        static public bool SetRootKeyValueForCurrUser(string _rootToAdd, string _KeyToAdd, string _ValueToAdd)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(_rootToAdd))
            {
                try
                {
                    key.SetValue(_KeyToAdd, _ValueToAdd);
                    key.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

            return true;
        }

        //-----------------------------------------------------------

        static public bool GetKeysValuesByRootForCurrUser(string _rootToGet, out Dictionary<string, object> _keysValuesMap)
        {
            _keysValuesMap = new Dictionary<string, object>();

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(_rootToGet))
            {
                try
                {
                    string[] valueNames = key.GetValueNames();
                    _keysValuesMap = valueNames.ToDictionary(name => name, key.GetValue);
                }
                catch(Exception e)          
                {
                    return false;
                }
            }

            return true;
        }

        //-----------------------------------------------------------

        static public bool GetDefaultValueByRootForCurrUser(string _rootToGet, out string _defaultValue)
        {
            _defaultValue = "";

            using (var key = Registry.CurrentUser.OpenSubKey(_rootToGet))
            {              
                try
                {
                    var defValue = key?.GetValue("");
                    if (defValue != null)
                    {
                        _defaultValue = defValue as string;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        //-----------------------------------------------------------
    }
}
