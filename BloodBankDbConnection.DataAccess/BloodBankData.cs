using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BloodBankDbConnection.Entities;
using BloodBankDbConnection.Framework;
using System.IO;

namespace BloodBankDbConnection.DataAccess
{
    public class BloodBankData
    {
        public static int Receiver_id;
        public static string Receiver_name;
        public static string Receiver_password;
        public static DateTime Receiver_dob;
        public static string Receiver_gender;
        public static string Receiver_bloodGroup;
        public static string Receiver_address;
        public static string Receiver_cellphone;
        public static string Receiver_email;
        public static double Donor_height;
        public static double Donor_weight;
        public static string Donor_drugAddiction;
        public static string Donor_HIV;
        public static DateTime Donor_lastDonationDate;
        public static string Donor_fileName;
        public static byte[] receiverImage;

        List<LOGIN_CREDENTIALS> GetDataLogInCredentials(SqlCommand cmd)
        {
            cmd.Connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            List<LOGIN_CREDENTIALS> list = new List<LOGIN_CREDENTIALS>();

            using (reader)
            {
                while(reader.Read())
                {
                    LOGIN_CREDENTIALS obj = new LOGIN_CREDENTIALS();
                    obj.ID = reader.GetInt32(0);
                    obj.PASSWORD = reader.GetString(1);
                    obj.TYPE = reader.GetString(2);
                    obj.STATUS = reader.GetString(3);
                    list.Add(obj);
                }
                reader.Close();
            }
            cmd.Connection.Close();
            return list;
        }

        List<MODERATORS> GetDataModerators(SqlCommand cmd)
        {
            cmd.Connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            List<MODERATORS> list = new List<MODERATORS>();

            using (reader)
            {
                while (reader.Read())
                {
                    MODERATORS obj = new MODERATORS();
                    obj.ID = reader.GetInt32(0);
                    obj.NAME = reader.GetString(1);
                    obj.IMAGE = (byte[])(reader[2]);
                    MemoryStream memoryStream = new MemoryStream(obj.IMAGE);
                    obj.GENDER = reader.GetString(3);
                    obj.ADDRESS = reader.GetString(4);
                    obj.EMAIL = reader.GetString(5);
                    obj.CELLPHONE = reader.GetString(6);
                    obj.JOIN_DATE = reader.GetDateTime(7);
                    list.Add(obj);
                }
                reader.Close();
            }
            cmd.Connection.Close();
            return list;
        }

        List<USERS> GetDataUsers(SqlCommand cmd)
        {
            cmd.Connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            List<USERS> list = new List<USERS>();

            using (reader)
            {
                while (reader.Read())
                {
                    USERS obj = new USERS();
                    obj.ID = reader.GetInt32(0);
                    Receiver_id = obj.ID;
                    obj.NAME = reader.GetString(1);
                    Receiver_name = obj.NAME;
                    receiverImage = (byte[])(reader[2]);
                    MemoryStream memoryStream = new MemoryStream(obj.IMAGE);
                    obj.IMAGE = receiverImage;
                    obj.DOB = reader.GetDateTime(3);
                    Receiver_dob = obj.DOB;
                    obj.GENDER = reader.GetString(4);
                    Receiver_gender = obj.GENDER;
                    obj.BLOOD_GROUP = reader.GetString(5);
                    Receiver_bloodGroup = obj.BLOOD_GROUP;
                    obj.ADDRESS = reader.GetString(6);
                    Receiver_address = obj.ADDRESS;
                    obj.CELLPHONE = reader.GetString(7);
                    Receiver_cellphone = obj.CELLPHONE;
                    obj.EMAIL = reader.GetString(8);
                    Receiver_email = obj.EMAIL;
                    obj.HEIGHT = reader.GetDouble(9);
                    Donor_height = obj.HEIGHT;
                    obj.WEIGHT = reader.GetDouble(10);
                    Donor_weight = obj.WEIGHT;
                    obj.DRUG_ADDICTION = reader.GetString(11);
                    Donor_drugAddiction = obj.DRUG_ADDICTION;
                    obj.HIV_STATUS = reader.GetString(12);
                    Donor_HIV = obj.HIV_STATUS;
                    obj.LAST_DONATION_DATE = reader.GetDateTime(13);
                    Donor_lastDonationDate = obj.LAST_DONATION_DATE;
                    list.Add(obj);
                }
                reader.Close();
            }
            cmd.Connection.Close();
            return list;
        }


        public List<LOGIN_CREDENTIALS> GetLogInCredentialsList()
        {
            //returns all user login info
            SqlDbDataAccess da = new SqlDbDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * FROM LOGIN_CRESENTIALS");
            List<LOGIN_CREDENTIALS> logInCredentialsList = GetDataLogInCredentials(cmd);
            return logInCredentialsList;
        }

        //public List<DONATION_HISTORY> GetDonationHistoryList()
        //{
        //    //returns all donation history info
        //    SqlDbDataAccess da = new SqlDbDataAccess();
        //    SqlCommand cmd = da.GetCommand("SELECT * FROM DONATION_HISTORY");
        //    List<DONATION_HISTORY> donationHistoryList = GetDataLogInCredentials(cmd);
        //    return logInCredentialsList;
        //}

        public List<LOGIN_CREDENTIALS> getUserLoginInfoById(int id)
        {
            SqlDbDataAccess da = new SqlDbDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * FROM LOGIN_CREDENTIALS WHERE ID = " + id );
            List<LOGIN_CREDENTIALS> userLoginInfo = GetDataLogInCredentials(cmd);
            return userLoginInfo;
        }

        public void getUsersInfoById(int id)
        {
            SqlDbDataAccess da = new SqlDbDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * FROM LOGIN_CREDENTIALS WHERE ID = " + id);
            List<LOGIN_CREDENTIALS> userLoginInfo = GetDataLogInCredentials(cmd);
        }

        public bool insertLoginCredentials(LOGIN_CREDENTIALS obj)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("INSERT INTO [dbo].[LOGIN_CREDENTIALS] ([PASSWORD],[TYPE],[STATUS])" + "VALUES (@PASSWORD, @TYPE, @STATUS)");
                //SqlParameter p = new SqlParameter("@ID", SqlDbType.Int);
                //p.Value = obj.ID;
                SqlParameter p1 = new SqlParameter("@PASSWORD", SqlDbType.VarChar, 50);
                p1.Value = obj.PASSWORD;
                SqlParameter p2 = new SqlParameter("@TYPE", SqlDbType.VarChar, 10);
                p2.Value = obj.TYPE;
                SqlParameter p3 = new SqlParameter("@STATUS", SqlDbType.VarChar, 10);
                p3.Value = obj.STATUS;

                //cmd.Parameters.Add(p);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch(Exception ex)
            {

            }
            return val > 0;
        }

        public bool updateLoginCredentials_Status(int id)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("UPDATE LOGIN_CREDENTIALS SET STATUS = 'ACTIVE' WHERE ID = "+id);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }

        public bool updateLoginCredentials_TypeToDonor(int id)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("UPDATE LOGIN_CREDENTIALS SET TYPE = 'DONOR' WHERE ID = " + id);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }

        public bool updateLoginCredentials_TypeToReceiver(int id)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("UPDATE LOGIN_CREDENTIALS SET TYPE = 'RECEIVER' WHERE ID = " + id);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }

        public bool updateLoginCredentials_Password(int id, string password)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("UPDATE LOGIN_CREDENTIALS SET PASSWORD = '"+password+"' WHERE ID = " + id);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }

        public bool deleteUsers(int id)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("DELETE FROM USERS WHERE ID = " + id);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }

        public bool deleteLoginCredentials(int id)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("DELETE FROM LOGIN_CREDENTIALS WHERE ID = " + id);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }

        public int getNextID()
        {
            int i = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("SELECT MAX(ID) FROM LOGIN_CREDENTIALS");
                //List <LOGIN_CREDENTIALS> userLoginInfo = GetDataLogInCredentials(cmd);

                cmd.Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                LOGIN_CREDENTIALS obj = new LOGIN_CREDENTIALS();
                using (reader)
                {
                    while (reader.Read())
                    {
                        obj.ID = reader.GetInt32(0);
                    }
                }
                reader.Close();
                cmd.Connection.Close();
                i = obj.ID;
                i++;
            }
            catch (Exception ex)
            {

            }
            return i;
        }

        public int logIn(int id, string password)
        {
            int loginFlag = 0;
            string status;
            string type;

            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("SELECT * FROM LOGIN_CREDENTIALS WHERE ID = " + id + " AND PASSWORD = " + password);
                List<LOGIN_CREDENTIALS> userLoginInfo = GetDataLogInCredentials(cmd);

                cmd.Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                LOGIN_CREDENTIALS obj = new LOGIN_CREDENTIALS();
                using (reader)
                {
                    while (reader.Read())
                    {
                        obj.ID = reader.GetInt32(0);
                        obj.PASSWORD = reader.GetString(1);
                        obj.TYPE = reader.GetString(2);
                        obj.STATUS = reader.GetString(3);
                    }
                }
                reader.Close();
                cmd.Connection.Close();
                status = obj.STATUS;
                type = obj.TYPE;

                if (status.Equals("ACTIVE") && type.Equals("MODERATOR"))
                {
                    if (id == obj.ID && password.Equals(obj.PASSWORD))
                    {
                        loginFlag = 1;
                    }
                }
                else if (status.Equals("ACTIVE") && type.Equals("RECEIVER"))
                {
                    if (id == obj.ID && password.Equals(obj.PASSWORD))
                    {
                        loginFlag = 2;
                    }
                }
                else if (status.Equals("ACTIVE") && type.Equals("DONOR"))
                {
                    if (id == obj.ID && password.Equals(obj.PASSWORD))
                    {
                        loginFlag = 3;
                    }
                }
                else
                {
                    loginFlag = 0;
                }

                
            }
            catch(Exception ex)
            {
                
            }
            return loginFlag;
        }

        public bool insertUsersReceiver(USERS obj)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("INSERT INTO [dbo].[USERS] ([ID],[NAME],[IMAGE], [DOB], [GENDER], [BLOOD_GROUP], [ADDRESS], [CELLPHONE], [EMAIL]) VALUES (@ID, @NAME, @IMAGE, @DOB, @GENDER, @BLOOD_GROUP, @ADDRESS, @CELLPHONE, @EMAIL)");
                SqlParameter p = new SqlParameter("@ID", SqlDbType.Int);
                p.Value = obj.ID;
                SqlParameter p1 = new SqlParameter("@NAME", SqlDbType.VarChar, 50);
                p1.Value = obj.NAME;
                SqlParameter p2 = new SqlParameter("@IMAGE", SqlDbType.Image);
                p2.Value = obj.IMAGE;
                SqlParameter p3 = new SqlParameter("@DOB", SqlDbType.Date);
                p3.Value = obj.DOB;
                SqlParameter p4 = new SqlParameter("@GENDER", SqlDbType.VarChar, 6);
                p4.Value = obj.GENDER;
                SqlParameter p5 = new SqlParameter("@BLOOD_GROUP", SqlDbType.VarChar, 3);
                p5.Value = obj.BLOOD_GROUP;
                SqlParameter p6 = new SqlParameter("@ADDRESS", SqlDbType.VarChar, 50);
                p6.Value = obj.ADDRESS;
                SqlParameter p7 = new SqlParameter("@CELLPHONE", SqlDbType.VarChar, 11);
                p7.Value = obj.CELLPHONE;
                SqlParameter p8 = new SqlParameter("@EMAIL", SqlDbType.VarChar, 50);
                p8.Value = obj.EMAIL;

                cmd.Parameters.Add(p);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch(Exception ex)
            {

            }
            return val > 0;
        }

        public bool insertUsersDonor(USERS obj)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("INSERT INTO [dbo].[USERS] ([ID],[NAME],[IMAGE], [DOB], [GENDER], [BLOOD_GROUP], [ADDRESS], [CELLPHONE], [EMAIL], [DRUG_ADDICTION], [HIV_STATUS], [LAST_DONATION_DATE]) VALUES (@ID, @NAME, @IMAGE, @DOB, @GENDER, @BLOOD_GROUP, @ADDRESS, @CELLPHONE, @EMAIL, @DRUG_ADDICTION, @HIV_STATUS, @LAST_DONATION_DATE)");
                SqlParameter p = new SqlParameter("@ID", SqlDbType.Int);
                p.Value = obj.ID;
                SqlParameter p1 = new SqlParameter("@NAME", SqlDbType.VarChar, 50);
                p1.Value = obj.NAME;
                SqlParameter p2 = new SqlParameter("@IMAGE", SqlDbType.Image);
                p2.Value = obj.IMAGE;
                SqlParameter p3 = new SqlParameter("@DOB", SqlDbType.Date);
                p3.Value = obj.DOB;
                SqlParameter p4 = new SqlParameter("@GENDER", SqlDbType.VarChar, 6);
                p4.Value = obj.GENDER;
                SqlParameter p5 = new SqlParameter("@BLOOD_GROUP", SqlDbType.VarChar, 3);
                p5.Value = obj.BLOOD_GROUP;
                SqlParameter p6 = new SqlParameter("@ADDRESS", SqlDbType.VarChar, 50);
                p6.Value = obj.ADDRESS;
                SqlParameter p7 = new SqlParameter("@CELLPHONE", SqlDbType.VarChar, 11);
                p7.Value = obj.CELLPHONE;
                SqlParameter p8 = new SqlParameter("@EMAIL", SqlDbType.VarChar, 50);
                p8.Value = obj.EMAIL;
                //SqlParameter p9 = new SqlParameter("@HEIGHT", SqlDbType.Float);
                //p9.Value = obj.HEIGHT;
                //SqlParameter p10 = new SqlParameter("@WEIGHT", SqlDbType.Float);
                //p10.Value = obj.WEIGHT;
                SqlParameter p9 = new SqlParameter("@DRUG_ADDICTION", SqlDbType.VarChar, 3);
                p9.Value = obj.DRUG_ADDICTION;
                SqlParameter p10 = new SqlParameter("@HIV_STATUS", SqlDbType.VarChar, 8);
                p10.Value = obj.HIV_STATUS;
                SqlParameter p11 = new SqlParameter("@LAST_DONATION_DATE", SqlDbType.Date);
                p11.Value = obj.LAST_DONATION_DATE;

                cmd.Parameters.Add(p);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);
                //cmd.Parameters.Add(p9);
                //cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p9);
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p11);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }

        public bool insertModerators(MODERATORS obj)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("INSERT INTO [dbo].[MODERATORS] ([ID],[NAME],[IMAGE], [GENDER], [ADDRESS], [EMAIL], [CELLPHONE], [JOIN_DATE]) VALUES (@ID, @NAME, @IMAGE, @GENDER, @ADDRESS, @EMAIL, @CELLPHONE, @JOIN_DATE)");
                SqlParameter p = new SqlParameter("@ID", SqlDbType.Int);
                p.Value = obj.ID;
                SqlParameter p1 = new SqlParameter("@NAME", SqlDbType.VarChar, 50);
                p1.Value = obj.NAME;
                SqlParameter p2 = new SqlParameter("@IMAGE", SqlDbType.Image);
                p2.Value = obj.IMAGE;
                SqlParameter p3 = new SqlParameter("@GENDER", SqlDbType.VarChar, 6);
                p3.Value = obj.GENDER;
                SqlParameter p4 = new SqlParameter("@ADDRESS", SqlDbType.VarChar, 50);
                p4.Value = obj.ADDRESS;
                SqlParameter p5 = new SqlParameter("@EMAIL", SqlDbType.VarChar, 50);
                p5.Value = obj.EMAIL;
                SqlParameter p6 = new SqlParameter("@CELLPHONE", SqlDbType.VarChar, 11);
                p6.Value = obj.CELLPHONE;
                SqlParameter p7 = new SqlParameter("@JOIN_DATE", SqlDbType.Date);
                p7.Value = obj.JOIN_DATE;

                cmd.Parameters.Add(p);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }

        public bool insertDonationHistory(DONATION_HISTORY obj)
        {
            int val = 0;
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("INSERT INTO [dbo].[DONATION_HISTORY] ([DONOR_ID],[RECIEVER_ID],[DONATION_DATE])" + "VALUES (@DONOR_ID, @RECIEVER_ID, @DONATION_DATE)");

                SqlParameter p1 = new SqlParameter("@DONOR_ID", SqlDbType.Int);
                p1.Value = obj.DONOR_ID;
                SqlParameter p2 = new SqlParameter("@RECIEVER_ID", SqlDbType.Int);
                p2.Value = obj.RECIEVER_ID;
                SqlParameter p3 = new SqlParameter("@DONATION_DATE", SqlDbType.Date);
                p3.Value = obj.DONATION_DATE;

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);

                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }
    } 
}
