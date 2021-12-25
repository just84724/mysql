using UnityEngine;
using System;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;
public class newtest : MonoBehaviour
{
    // Global variables
    public static MySqlConnection dbConnection;//Just like MyConn.conn in
                                               //StoryTools before
    static string host = "192.168.1.100";
    static string id = "mysql";//這裡是你自己的數據庫的用戶名字，我一開始想用
                               //root，發現不行，後來添加了新的用戶才可以
    static string pwd = "123456";
    static string database = "test";
    static string result = "";

    private string strCommand = "Select * from unity3d_test ORDER BY id;";
    public static DataSet MyObj;
    void OnGUI()
    {
        host = GUILayout.TextField(host, 200, GUILayout.Width(200));
        id = GUILayout.TextField(id, 200, GUILayout.Width(200));
        pwd = GUILayout.TextField(pwd, 200, GUILayout.Width(200));
        if (GUILayout.Button("Test"))
        {
            string connectionString = string.Format("Server = {0}; Database = {1}; User ID = { 2}; Password = { 3}; ,host,database,id,pwd");
            openSqlConnection(connectionString);

            MyObj = GetDataSet(strCommand);
        }
        GUILayout.Label(result);
    }
    // On quit
    public static void OnApplicationQuit()
    {
        closeSqlConnection();
    }

    // Connect to database
    private static void openSqlConnection(string connectionString)
    {
        dbConnection = new MySqlConnection(connectionString);
        dbConnection.Open();
        result = dbConnection.ServerVersion;
    //("Connected to database."+result);
    }

    // Disconnect from database
    private static void closeSqlConnection()
    {
        dbConnection.Close();
        dbConnection = null;
    // ("Disconnected from database."+result);

    }

    // MySQL Query
    public static void doQuery(string sqlQuery)
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Close();
        reader = null;
        dbCommand.Dispose();
        dbCommand = null;
    }
    #region Get DataSet
    public DataSet GetDataSet(string sqlString)
    {
        //string sql = UnicodeAndANSI.UnicodeAndANSI.UnicodeToUtf8(sqlString);


        DataSet ds = new DataSet();
        try
        {
            MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection);
            da.Fill(ds);

        }
        catch (Exception ee)
        {

            throw new Exception("SQL:" + sqlString + "\n" +
            ee.Message.ToString());
        }
        return ds;

    }
    #endregion
}

/*using UnityEngine;
using System;
using System.Collections;
using System.Data;
public class DataBaseTest : MonoBehaviour
{
    public GUISkin myGUISkin = new GUISkin();
    string strID = "";
    string strName = "";
    string strSex = "";
    int Index = 1;
    // Use this for initialization
    void Start()
    {
    }
    void OnGUI()
    {
        GUI.skin = myGUISkin;
        if (GUI.Button(new Rect(100, 320, 100, 100), "Click Me"))
        {
            foreach (DataRow dr in CMySql.MyObj.Tables[0].Rows)
            {
                if (Index.ToString() == dr["ID"].ToString())
                {
                    strID = dr["ID"].ToString();
                    strName = dr["Name"].ToString();
                    strSex = dr["Sex"].ToString();

                    break;
                }
            }
            Index++;
            if (Index > 5)
            {
                Index = 1;
            }

        }
        GUI.Label(new Rect(320, 100, 150, 70), "DataBaseTest");
        GUI.Label(new Rect(300, 210, 150, 70), strID);
        GUI.Label(new Rect(300, 320, 150, 70), strName);
        GUI.Label(new Rect(300, 430, 150, 70), strSex);

    }
}*/
