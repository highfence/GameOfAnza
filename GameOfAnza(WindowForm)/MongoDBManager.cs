using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameOfAnza_WindowForm_
{
	/*
	 * 싱글톤으로 만들어진 DatabaseManager.
	 */
	class MongoDBManager
	{
		private static MongoDBManager _instance;
		public static MongoDBManager GetInstance()
		{
			if (_instance == null)
			{
				_instance = new MongoDBManager();
			}
			return _instance;
		}

		public class InOutData
		{
			public ObjectId _id { get; set; }
			public string USE_DT { get; set; }
			public string BUS_ROUTE_NO { get; set; }
			public string BUS_ROUTE_NM { get; set; }
			public int STND_BSST_ID { get; set; }
			public int BSST_ARS_NO { get; set; }
			public string BUS_STA_NM { get; set; }
			public int RIDE_PASGR_NUM { get; set; }
			public int ALIGHT_PASGR_NUM { get; set; }
		}

		// 데이터 베이스 관련 데이터가 저장된 파일.
		//private string xmlfilePath = "C:/Users/NEXT/Desktop/Project/GameOfAnza(WindowForm)/Resources/Database.xml";
		private string xmlfilePath = "../../Resources/database.xml";

		/*
		 * MongoDBManager 생성자.
		 * DB를 연결하는데 필요한 정보는 Xml파일에서 읽어들이고,
		 * 그 정보에 따라 MongoDB와 연결한다.
		 */
		protected MongoDBManager()
		{
			XmlDocument dbXml = new XmlDocument();
			dbXml.Load(xmlfilePath);

			XmlNodeList xmlNodeList = dbXml.SelectNodes("DatabaseInfo");
			XmlNode xn = xmlNodeList.Item(0);

			_database = xn["_database"].InnerText;
			_user = xn["_user"].InnerText;
			_pwd = xn["_pwd"].InnerText;
			_serverIp = xn["_serverIp"].InnerText;
			_port = Convert.ToInt32(xn["_port"].InnerText);

			Connect();
		}

		// MongoDB variables
		private string _database;
		private string _user;
		private string _pwd;
		private string _serverIp;
		private int _port;

		private MongoCredential _credential;
		private MongoClientSettings _settings;
		private MongoClient _mongoClient;
		private IMongoDatabase _mongoDatabase;

		private bool Connect()
		{
			_credential = MongoCredential.CreateCredential(_database, _user, _pwd);

			_settings = new MongoClientSettings
			{
				Credentials = new[] { _credential },
				Server = new MongoServerAddress(_serverIp, _port)
			};

			_mongoClient = new MongoClient(_settings);
			_mongoDatabase = _mongoClient.GetDatabase(_database);

			return true;
		}

		public List<InOutData> FindWithStationId(int stationId)
		{
			var collection = _mongoDatabase.GetCollection<InOutData>("bus_in_out");

			var list = collection.Find(x => x.STND_BSST_ID == stationId).ToList();

			foreach (var st in list)
			{
				Debug.Write(st.USE_DT);
			}

			return list;
		}
	}
}
