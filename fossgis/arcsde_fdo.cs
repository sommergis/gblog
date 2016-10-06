using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OSGeo.FDO;
using OSGeo.FDO.Connections;
using OSGeo.FDO.ClientServices;
using OSGeo.FDO.Geometry;
using OSGeo.FDO.Commands;
using OSGeo.FDO.Commands.Feature;
using OSGeo.FDO.Expression;
using OSGeo.FDO.Commands.DataStore;
using OSGeo.FDO.Commands.Schema;
using OSGeo.FDO.Filter;
using OSGeo.FDO.Schema;
using DotSpatial.Data;
 
namespace GSharpDotSpatial
{
 
    public class ArcSDEHelper
    {
        private string hostname;
        private int port;
        private string user;
        private string password;
        private string datastore;
        private string connStr;
        private string pkeyColumn;
        private DataTable dataTable;
 
        private IConnection FDOConnection;
        private IProviderRegistry FDORegistry;
        private IConnectionManager FDOManager;
        private OSGeo.FDO.Connections.ConnectionState ConState;
 
        public ArcSDEHelper(string _hostname, int _port, string _user, string _password, string _datastore)
        {
            this.hostname = _hostname;
            this.port = _port;
            this.user = _user;
            this.password = _password;
            this.datastore = _datastore;
 
            connStr = "Server=" + hostname + ";" + "Instance=" + port + ";" + "User=" + user + ";" + "Password=" + password + ";" + "Datastore=" + datastore + ";";
        }
 
        public IConnection connect()
        {
            try
            {
                FDORegistry = FeatureAccessManager.GetProviderRegistry();
                FDOManager = FeatureAccessManager.GetConnectionManager();
                FDOConnection = FeatureAccessManager.GetConnectionManager().CreateConnection("OSGeo.ArcSDE.3.6"); // Replace the version of DLL your using…
 
                IConnectionPropertyDictionary connectionPropertyDictionary;
                connectionPropertyDictionary = FDOConnection.ConnectionInfo.ConnectionProperties;
                connectionPropertyDictionary.SetProperty("Server",hostname);
                connectionPropertyDictionary.SetProperty("Instance",port.ToString());
                connectionPropertyDictionary.SetProperty("Username",user);
                connectionPropertyDictionary.SetProperty("Password",password);
                connectionPropertyDictionary.SetProperty("Datastore",datastore);
 
//              FDO Gets the Datastore options out of a pending connection (2-step connection)
//              FDOConnection.Open();
//              string s = "";
//              string[] strArry = null;
//              IConnectionPropertyDictionary dict = FDOConnection.ConnectionInfo.ConnectionProperties;
//              foreach (string st in dict.PropertyNames) {
//
//                  string val = dict.GetProperty(st);
//                  string defVal = dict.GetPropertyDefault(st);
//                  string localname = dict.GetLocalizedName(st);
//                  bool isRequired = dict.IsPropertyRequired(st);
//                  bool isEnumerable = dict.IsPropertyEnumerable(st);
//
//                  if (isEnumerable)
//                  {
//                      strArry = new string[dict.EnumeratePropertyValues(st).Length];
//                      dict.EnumeratePropertyValues(st).CopyTo(strArry,0);
//                  }
//              }
 
                return FDOConnection;
            }
            catch (OSGeo.FDO.Common.Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return null;
            }
        }        
 
        public string ConnStr
        { get { return connStr; } }
 
        public DataTable ListSchema()
        {
            OSGeo.FDO.Connections.IConnection conn = connect();
            ConState = conn.Open();
            try
            {
                if (null != conn && ConState == OSGeo.FDO.Connections.ConnectionState.ConnectionState_Open)
                {
                    // Get Schema Names:
                    OSGeo.FDO.Commands.Schema.IGetSchemaNames pGSN =(OSGeo.FDO.Commands.Schema.IGetSchemaNames)conn.CreateCommand
                        (OSGeo.FDO.Commands.CommandType.CommandType_GetSchemaNames);
 
                    OSGeo.FDO.Common.StringCollection stCol = pGSN.Execute();
 
                    dataTable = new DataTable();
                    dataTable.Columns.Add("FeatureSchemaName", typeof(string));
 
                    foreach ( OSGeo.FDO.Common.StringElement s in stCol)
                    {
                        dataTable.Rows.Add(s.String);
                    }
                    // sort dataTable
                    DataView v = dataTable.DefaultView;
                    v.Sort = "FeatureSchemaName ASC";
                    dataTable = v.ToTable();
 
                    conn.Close();
                    return dataTable;
                }
                else {return null;}
            }
            catch (OSGeo.FDO.Common.Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return null;
            }
        }
        public DataTable ListTablesOfDB(string _schemaName)
        {
            OSGeo.FDO.Connections.IConnection conn = connect();
            ConState = conn.Open();
            try
            {
                if (null != conn && ConState == OSGeo.FDO.Connections.ConnectionState.ConnectionState_Open)
                {
                    //Get a list of tables in the DB
                    OSGeo.FDO.Commands.Schema.IGetClassNames pGSN =(OSGeo.FDO.Commands.Schema.IGetClassNames)conn.CreateCommand
                        (OSGeo.FDO.Commands.CommandType.CommandType_GetClassNames);
 
                    OSGeo.FDO.Common.StringCollection stCol = pGSN.Execute();
 
                    dataTable = new DataTable();
                    dataTable.Columns.Add("FeatureClasses", typeof(string));
 
                    foreach ( OSGeo.FDO.Common.StringElement s in stCol)
                    {
                        if (s.String.StartsWith(_schemaName))
                        {
                            string newStr = s.String.Replace(_schemaName + ':', string.Empty);
                        }
                    }
 
                    // sort dataTable
                    DataView v = dataTable.DefaultView;
                    v.Sort = "FeatureClasses ASC";
                    dataTable = v.ToTable();
 
                    conn.Close();
                    return dataTable;
                }
                else {return null;}
            }
            catch (OSGeo.FDO.Common.Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return null;
            }
            finally { conn.Close(); }
            }
 
        public string GetGeomColumn(string _tableName, string _schemaName)
        {
            string geomCol = "";
            OSGeo.FDO.Connections.IConnection conn = connect();
            ConState = conn.Open();
            try
            {
                if (null != conn && ConState == OSGeo.FDO.Connections.ConnectionState.ConnectionState_Open)
                {
                    ISelect sel = (ISelect)conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_Select);
                    sel.SetFeatureClassName(_tableName);
 
                    IFeatureReader FDOReader = sel.Execute();
                    OSGeo.FDO.Schema.ClassDefinition cDef = FDOReader.GetClassDefinition();
 
                    foreach (OSGeo.FDO.Schema.PropertyDefinition def in cDef.Properties)
                    {
                        if (def.PropertyType == OSGeo.FDO.Schema.PropertyType.PropertyType_GeometricProperty)
                        {
                            geomCol = def.Name;
                        }
                    }
                    conn.Close();
                    return geomCol;
                }
                else {return null;}
            }
            catch (OSGeo.FDO.Common.Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return null;
            }
            finally { conn.Close(); }
 
        }
        public FeatureSet GetAllFeatures(string _featureClassName, string _geomColumn)
        {
            OSGeo.FDO.Connections.IConnection conn = connect();
            ConState = conn.Open();
 
            if (ConState == OSGeo.FDO.Connections.ConnectionState.ConnectionState_Open
                && _featureClassName != null && (_geomColumn != null || _geomColumn != ""))
            {
                ISelect sel = (ISelect)conn.CreateCommand(OSGeo.FDO.Commands.CommandType.CommandType_Select);
                sel.SetFeatureClassName(_featureClassName);
 
                IFeatureReader FDOReader = sel.Execute();
 
                GeometryCollection Geo_Collection = new GeometryCollection();
                FgfGeometryFactory fdoGeoFac = new FgfGeometryFactory();
 
                OSGeo.FDO.Schema.ClassDefinition cDef = FDOReader.GetClassDefinition();
 
                DotSpatial.Data.FeatureSet fs = new FeatureSet();
 
                //Populate schema of DataTable
                foreach (OSGeo.FDO.Schema.PropertyDefinition pDef in cDef.Properties)
                {
                    // Only Data - no geometry!
                    if (OSGeo.FDO.Schema.PropertyType.PropertyType_DataProperty == pDef.PropertyType)
                    {
                        //Get Datatype
                        var dProDef = pDef as OSGeo.FDO.Schema.DataPropertyDefinition;
                        OSGeo.FDO.Schema.DataType typ = dProDef.DataType;
                        Type t = ChangeType(typ);
                        //Get Property name
                        string propertyName = pDef.Name;
                        fs.DataTable.Columns.Add(pDef.Name, t);
                    }
                }
 
                while (FDOReader.ReadNext())
                {
                    Feature feat = new Feature();
                    DataRow dr = fs.DataTable.NewRow();
                    foreach (PropertyDefinition pDef in cDef.Properties)
                    {
                        if (pDef.PropertyType == PropertyType.PropertyType_DataProperty)
                        {
                            DataPropertyDefinition dDef = pDef as DataPropertyDefinition;
                            switch (dDef.DataType)
                            {
                                case OSGeo.FDO.Schema.DataType.DataType_String:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetString(dDef.Name);
                                    break;
                                case OSGeo.FDO.Schema.DataType.DataType_Int16:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetInt16(dDef.Name);
                                    break;
                                case OSGeo.FDO.Schema.DataType.DataType_Int32:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetInt32(dDef.Name);
                                    break;
                                case OSGeo.FDO.Schema.DataType.DataType_Int64:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetInt64(dDef.Name);
                                    break;
                                case OSGeo.FDO.Schema.DataType.DataType_DateTime:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetDateTime(dDef.Name);
                                    break;
                                case OSGeo.FDO.Schema.DataType.DataType_Single:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetSingle(dDef.Name);
                                    break;
                                case OSGeo.FDO.Schema.DataType.DataType_Double:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetDouble(dDef.Name);
                                    break;
                                case OSGeo.FDO.Schema.DataType.DataType_Decimal:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetDouble(dDef.Name);
                                    break;
                                case OSGeo.FDO.Schema.DataType.DataType_Boolean:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetBoolean(dDef.Name);
                                    break;
                                default:
                                    if (!FDOReader.IsNull(dDef.Name))
                                        dr[dDef.Name] = FDOReader.GetByte(dDef.Name);
                                    break;
                            }
                            feat.DataRow = dr;
                        }
 
                        if (pDef.PropertyType == PropertyType.PropertyType_GeometricProperty)
                        {
                            //Get Geometry with FDO Reader
                            Byte[] Tmppts = FDOReader.GetGeometry(_geomColumn);
                            IGeometry fdoGeo = fdoGeoFac.CreateGeometryFromFgf(Tmppts);
                            // Convert to WKB
                            Byte[] wkbFDO = fdoGeoFac.GetWkb(fdoGeo);
 
                            // Read WKB from FDO and convert to DotSpatial Geometry
                            DotSpatial.Topology.GeometryFactory geoFac = new DotSpatial.Topology.GeometryFactory();
                            DotSpatial.Topology.Utilities.WkbReader wkbReader = new DotSpatial.Topology.Utilities.WkbReader();
                            DotSpatial.Topology.IGeometry geom = wkbReader.Read(wkbFDO);
 
                            //Add DotSpatial Geometry
                            feat.BasicGeometry = geom;
                        }
                    }
                    fs.Features.Add(feat);
                }
                return fs;
            }
            else {return null;}
        }
 
        public static Type ChangeType(OSGeo.FDO.Schema.DataType dt)
        {
 
            switch (dt)
            {
                case OSGeo.FDO.Schema.DataType.DataType_BLOB:
                {
                    return Type.GetType("Sytem.Object");
                }
                case OSGeo.FDO.Schema.DataType.DataType_Boolean:
                {
                    return Type.GetType("System.Boolean");
                }
                case OSGeo.FDO.Schema.DataType.DataType_Byte:
                {
                    return Type.GetType("System.Byte");
                }
                case OSGeo.FDO.Schema.DataType.DataType_Int16:
                {
                    return Type.GetType("System.Int16");
                }
                case OSGeo.FDO.Schema.DataType.DataType_Int32:
                {
                    return Type.GetType("System.Int32");
                }
                case OSGeo.FDO.Schema.DataType.DataType_Int64:
                {
                    return Type.GetType("System.Int64");
                }
                case OSGeo.FDO.Schema.DataType.DataType_Single:
                {
                    return Type.GetType("System.Single");
                }
                case OSGeo.FDO.Schema.DataType.DataType_Double:
                {
                    return Type.GetType("System.Double");
                }
                case OSGeo.FDO.Schema.DataType.DataType_Decimal:
                {
                    return Type.GetType("System.Decimal");
                }
                case OSGeo.FDO.Schema.DataType.DataType_DateTime:
                {
                    return Type.GetType("System.DateTime");
                }
                case OSGeo.FDO.Schema.DataType.DataType_String:
                {
                    return Type.GetType("System.String");
                }
            }
        throw new ArgumentException("Unknown DataType");
        }
    }
}
