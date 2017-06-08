using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

		// MongoDB에서 받아온 데이터를 담아둘 구조체.
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
		private string xmlfilePath = "../../Resources/database.xml";

		/*
		 * MongoDBManager 생성자.
		 * DB를 연결하는데 필요한 정보는 Xml파일에서 읽어들이고,
		 * 그 정보에 따라 MongoDB와 연결한다.
		 */
		protected MongoDBManager()
		{
			try
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
			catch
			{
				MessageBox.Show("MongoDBManager Creation Failed!");

			}

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
		private IMongoCollection<BsonDocument> _collection;

		// Xml파일에 기록된 정보에 따라 접속을 시도하는 메소드.
		private bool Connect()
		{
			try
			{
				_credential = MongoCredential.CreateCredential(_database, _user, _pwd);

				_settings = new MongoClientSettings
				{
					Credentials = new[] { _credential },
					Server = new MongoServerAddress(_serverIp, _port)
				};

				_mongoClient = new MongoClient(_settings);
				_mongoDatabase = _mongoClient.GetDatabase(_database);

				_collection = _mongoDatabase.GetCollection<BsonDocument>("bus_in_out");
				return true;
			}
			catch
			{
				return false;
			}
		}

		// STND_BSSN_ID로 검색한 결과를 List<InOutData> 형태로 반환하는 메소드.
		public List<InOutData> FindWithStationId(int stationId)
		{
			var collection = _mongoDatabase.GetCollection<InOutData>("bus_in_out");

			var list = collection.Find(x => x.STND_BSST_ID == stationId).ToList();

			return list;
		}

		// 인자로 받은 노선이름과 정류장 ID의 승, 하차 인원을 반환하는 메소드.
		public Tuple<int, int> FindPassengerNumberWithStationId(string routeNm, int stationId)
		{
			// 역 Id searching을 도와줄 filter
			var stationFilter = Builders<BsonDocument>.Filter.Eq("STND_BSST_ID", stationId);
			// routeNm이 한글을 포함했는지 검사.
			FilterDefinition<BsonDocument> routeFilter;
			if (IsContainHangul(routeNm))
			{
				// 한글을 포함했다면 string 값 그대로 필터.
				routeFilter = Builders<BsonDocument>.Filter.Eq("BUS_ROUTE_NO", routeNm);
			}
			else
			{
				// 숫자로만 이루어졌다면 int값으로 필터.
				routeFilter = Builders<BsonDocument>.Filter.Eq("BUS_ROUTE_NO", Convert.ToInt32(routeNm));
			}

			var list = _collection.Find(stationFilter & routeFilter).ToList();
			int totalRidePassenger = 0;
			int totalAlightPassenger = 0;

			foreach (var dataLog in list)
			{
				totalRidePassenger += (int)dataLog.GetElement(7).Value;
				totalAlightPassenger += (int)dataLog.GetElement(8).Value;
			}

			return new Tuple<int, int>(totalRidePassenger, totalAlightPassenger);
		}

		/*
		 * goo.gl/uOelSc 블로그의 코드.
		 * string에 한글이 포함되어있는지 체크해주는 함수.
		 */
		public bool IsContainHangul(string str)
		{
			char[] charArray = str.ToCharArray();
			// 한글이 있는지 한 글자씩 검사.
			foreach (char c in charArray)
			{
				// 있다면 true 반환.
				if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
				{
					return true;
				}
			}

			// 없다면 false 반환.
			return false;
		}
	}
}
