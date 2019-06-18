using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.SQL
{
    class SQLQueries
    {
        private SQLConnString db3String = new SQLConnString("Milestone1DB", "Fart", "5433");
        private SQLConstants c = new SQLConstants();

        public SQLQueries() { }

        public List<Object[]> SQLSelectQuery(Dictionary<String, List<String>> queryDictionary)
        {
            List<Object[]> ObjList = new List<Object[]>();
            using (var conn = new NpgsqlConnection(db3String.BuildConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = SimpleCommandString(queryDictionary);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Object[] objArray = new Object[reader.FieldCount];
                            reader.GetValues(objArray);
                            ObjList.Add(objArray);
                        }
                    }
                }
                conn.Close();
            }
            return ObjList;
        }

        // return tuples changed, if 0 we can use an insert, if not the dont....
        public int SQLNonQuery(Dictionary<String, List<String>> queryDictionary)
        {
            int updated = 0;
            using (var conn = new NpgsqlConnection(db3String.BuildConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = GetCommandString(queryDictionary);
                    updated = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            return updated;
        }


        private String SimpleCommandString(Dictionary<String, List<String>> queryDictionary)
        {
            List<String> projectionList = queryDictionary.ContainsKey(c.SQL_PROJECT) ? queryDictionary[c.SQL_PROJECT] : null;
            List<String> conditionList = queryDictionary.ContainsKey(c.SQL_COND) ? queryDictionary[c.SQL_COND] : null;
            List<String> fromList = queryDictionary.ContainsKey(c.SQL_FROM) ? queryDictionary[c.SQL_FROM] : null;
            List<String> whereList = queryDictionary.ContainsKey(c.SQL_WHERE) ? queryDictionary[c.SQL_WHERE] : null;
            List<String> orderList = queryDictionary.ContainsKey(c.SQL_ORDER) ? queryDictionary[c.SQL_ORDER] : null;
            String queryString = "";
            String specialCaseString = "";

            if (projectionList != null)
            {
                queryString += projectionList[0] + " ";
                for (int i = 1; i < projectionList.Count; i++)
                {
                    if (i == projectionList.Count - 1)
                        queryString += projectionList[i] + " ";
                    else
                        queryString += projectionList[i] + ",";
                }
            }

            if (fromList != null)
            {
                queryString += " " + fromList[0] +" ";
                for (int i = 1; i < fromList.Count; i++)
                {
                    if (i == fromList.Count-1 && fromList.Count > 2)
                        queryString += "natural join " + fromList[i] + " ";
                    else
                        queryString += fromList[i] + " ";

                }
            }
            if(queryDictionary.ContainsKey(c.SQL_ATTR) || queryDictionary.ContainsKey(c.SQL_CAT))
            {
                List<String> specialCase =  SpecialCase(queryDictionary);
                queryString += specialCase[0];
                specialCaseString += specialCase[1];
            }


            if (whereList != null)
            {
                if (!queryDictionary.ContainsKey(c.SQL_ATTR) && !queryDictionary.ContainsKey(c.SQL_CAT) && !queryDictionary.ContainsKey(c.SQL_FROM))
                    queryString += "from business as B ";
                queryString += " where ";
                for (int i = 0; i < conditionList.Count; i++)
                {
                    if (i == conditionList.Count - 1)
                        queryString += whereList[i] + " = '" + conditionList[i] + "' ";
                    else
                        queryString += whereList[i] + " = '" + conditionList[i] + "' " + " and ";
                }
                queryString += specialCaseString;
            }


            if (orderList != null)
            {
                queryString += " " + orderList[0] + " ";
                for (int i = 2; i < orderList.Count; i++)
                {
                    if (i == orderList.Count - 1)
                        queryString += orderList[i] + " ";
                    else
                        queryString += orderList[i] + ",";
                }
                queryString += orderList[1];
            }
            return queryString;
        }

        private List<String> SpecialCase(Dictionary<String, List<String>> queryDictionary)
        {
            List<String> categoryList = queryDictionary.ContainsKey(c.SQL_CAT) ? queryDictionary[c.SQL_CAT] : null;
            List<String> attributeList = queryDictionary.ContainsKey(c.SQL_ATTR) ? queryDictionary[c.SQL_ATTR] : null;
            List<String> attrValList = queryDictionary.ContainsKey(c.SQL_VAL) ? queryDictionary[c.SQL_VAL] : null;
            String queryString = "";
            String specialCaseString = "";

            if (categoryList != null)
            {
                queryString += "from categories as C0";
                for (int i = 0; i < categoryList.Count; i++)
                {
                    queryString += " inner join categories as C" + (i + 1) + " on C" + i + ".business_id = C" + (i + 1) + ".business_id";
                    specialCaseString += " and C" + i + ".category_name = '" + categoryList[i] + "'";
                }
                queryString += " inner join business as B on B.business_id = C0.business_id ";
            }

            if (attributeList != null)
            {
                queryString += categoryList != null ? "inner join attributes as A0 on A0.business_id = B.business_id" : "from attributes as A0";
                //queryString += "from attributes as A0";
                for (int i = 0; i < attributeList.Count; i++)
                {
                    queryString += " inner join attributes as A" + (i + 1) + " on A" + i + ".business_id = A" + (i + 1) + ".business_id";
                    specialCaseString += " and A" + i + ".attr_name = '" + attributeList[i] + "' and A" + i + ".attr_value ='" + attrValList[i] + "'";
                }
                queryString += categoryList != null ? "" : " inner join business as B on B.business_id = A0.business_id ";    
            }
            return new List<String>() { queryString, specialCaseString};

        }


        private String GetCommandString(Dictionary<String, List<String>> queryDictionary)
        {
            List<String> conditionList = queryDictionary.ContainsKey(c.SQL_COND) ? queryDictionary[c.SQL_COND] : null;
            List<String> fromList = queryDictionary.ContainsKey(c.SQL_FROM) ? queryDictionary[c.SQL_FROM] : null;
            List<String> whereList = queryDictionary.ContainsKey(c.SQL_WHERE) ? queryDictionary[c.SQL_WHERE] : null;
            List<String> attrValList = queryDictionary.ContainsKey(c.SQL_VAL) ? queryDictionary[c.SQL_VAL] : null;
            List<String> setList = queryDictionary.ContainsKey(c.SQL_SET) ? queryDictionary[c.SQL_SET] : null;
            
            String queryString = "";

            if (fromList != null)
            {
                for (int i = 0; i < fromList.Count; i++)
                {
                    queryString += fromList[i] + " ";
                }
            }

            if(attrValList != null && setList == null)
            {
                queryString += attrValList[0] + "( ";
                for (int i = 1; i < attrValList.Count; i++)
                {
                    if (i == attrValList.Count - 1)
                        queryString += " '" + attrValList[i] + "'" + ") ";
                    else
                        queryString += " '" + attrValList[i] + "'" + ",";
                }
            }

            if(setList != null)
            {
                queryString += setList[0] + " ";
                for (int i = 1; i < setList.Count; i++)
                {
                    if(attrValList[0].Equals("S"))
                    {
                        attrValList[i] = "'" + attrValList[i] + "'";
                    }
                    if (i == setList.Count - 1)
                        queryString += setList[i] + " = " + attrValList[i] + " ";
                    else
                        queryString += setList[i] + " = " + attrValList[i] + ", ";
                }
            }

            if (whereList != null)
            {
                queryString += whereList[0] + " ";
                conditionList.Insert(0, null);
                for (int i = 1; i < conditionList.Count; i++)
                {
                    if (i == conditionList.Count - 1)
                        queryString += whereList[i] + " = '" + conditionList[i] + "' ";
                    else
                        queryString += whereList[i] + " = '" + conditionList[i] + "' and ";
                }
            }

            return queryString;
        }
    }
}
