using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary
{
   public class DB
    {
        public MySqlConnection Getconnection()
        {
            
            try
            {
                MySqlConnection conn = new MySqlConnection();
                string path = "C:/public/DBInfo.json"; //파일 경로
                //파일을 열어서 읽고.다읽었으면 종료 시킨다.
                string result = new StreamReader(File.OpenRead(path)).ReadToEnd();
                //제이슨 오브젝트로 바꿀것 = 스트링 값을 오브젝트로 바꿀것.
                JObject jo = JsonConvert.DeserializeObject<JObject>(result);
                Hashtable map = new Hashtable();
                //오브젝트 된 jo의 프로퍼티를 보게 할것
                foreach (JProperty col in jo.Properties())
                {
                    map.Add(col.Name, col.Value);
                }
                string strConnection1= string.Format("server={0};user={1};password={2};database={3}", map["server"], map["user"], map["password"], map["database"]);
                Console.WriteLine(strConnection1);
                conn.ConnectionString = strConnection1;
                conn.Open();
                return conn;
            }
            //오류 걸릴때
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}



