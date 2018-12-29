using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace ConfigFileLib
{
    public class ConfigFile
    {
        //----------------------------------------------------------------------------------------------------

        private XmlDocument m_xml;

        //----------------------------------------------------------------------------------------------------

        public ConfigFile(string _fileName, out bool _success) 
        {
            try
            {
                m_xml = new XmlDocument();
                m_xml.Load(_fileName);
            }
            catch
            {
                _success = false;
                return;
            }

            _success = true;
        }

        //----------------------------------------------------------------------------------------------------
        /*        
         <_nodeName>
            <_nodeKey="Value">
         </_nodeName>      
        */
        public bool GetValueForNodeKey(string _nodeName, string _nodeKey, out string _valueToGet)  
        {
            _valueToGet = "";

            try
            {
                XmlNode rootNode = m_xml.GetElementsByTagName(_nodeName).Item(0);
                _valueToGet = rootNode.Attributes[_nodeKey].Value;
            }
            catch
            {
                return false;
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        /*
        <root>
           <_nodeName>	
		        <key1>value1</key1>
			    <key2>value2</key2>		
		   </_nodeName>
		   <_nodeName>
			    <key1>value1</key1>			
                <key2>value2</key2>	
		   </_nodeName>
        </root>
        */
        public bool GetNodesAttributesToListOfMaps(string _nodeName, out List<Dictionary<string,string>> _nodesAttrList)  
        {
            _nodesAttrList = new List<Dictionary<string, string>>();

            try
            {
                XmlNodeList nodeList = m_xml.GetElementsByTagName(_nodeName);

                foreach (XmlNode csciNode in nodeList)
                {
                    Dictionary<string, string> csciAtrributes = new Dictionary<string, string>();

                    foreach (XmlNode attribute in csciNode.ChildNodes)
                    {
                        string key = attribute.Name;
                        string value = attribute.InnerXml;

                        csciAtrributes[key] = value;
                    }

                    _nodesAttrList.Add(csciAtrributes);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------

        /*
        <GENERAL_CONFIGURATIONS>
            <LOOGER_MODE>ERROR</LOOGER_MODE>
            <MAP_SCRIPT_PATH>C:\AutoRelease\Map.bat</MAP_SCRIPT_PATH>
            <_tagName>_outputValue</_tagName>
        </GENERAL_CONFIGURATIONS>
        */
        public bool GetValueByTagName(string _tagName, out string _outputValue)
        {
            _outputValue = "";

            try
            {
                XmlNodeList nodeList = m_xml.GetElementsByTagName(_tagName);
                _outputValue = nodeList[0].InnerXml;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*--------------------------------------------------------------------------------------------------*/
            /*
           <GENERAL_CONFIGURATIONS>
               <LOOGER_MODE>ERROR</LOOGER_MODE>
               <MAP_SCRIPT_PATH>C:\AutoRelease\Map.bat</MAP_SCRIPT_PATH>
           </GENERAL_CONFIGURATIONS>
           */
            

        /*--------------------------------------------------------------------------------------------------*/

        /*XmlNodeList nodeValues = m_xml.SelectNodes(_nodePath);*/

        /*--------------------------------------------------------------------------------------------------*/

    }  // class ConfigFile
} //namespace 


