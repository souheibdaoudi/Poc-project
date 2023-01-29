using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteInerfaces.Services;

namespace Server.Services
{
    class AccountService : MarshalByRefObject, IAccountService
    {
        DB_Connection connection;
        
        public AccountService()
        {
            connection = new DB_Connection();
        }

        public void createAdminAccount(RemoteInerfaces.Entities.Account account, RemoteInerfaces.Entities.House house)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                account.Privilege = "ADMIN";
                cnn.ExecuteScalar($"INSERT INTO account  (id_house, first_name, last_name, birthdate, privilege, face_id, profile_image, password, is_active) VALUES (@id_house, @first_name, @last_name, @birthdate, @privilege, @face_id, @profile_image, @password, @is_active)", account);
                cnn.ExecuteScalar("INSERT INTO house  (id, owner_first_name, owner_last_name, type, id_admin, descriptive_name, address) VALUES (@id, @owner_first_name, @owner_last_name, @type, @id_admin, @descriptive_name, @address)", house);
                Console.WriteLine("ADMIN account create with success");
            }
        }

        public void createResidentAccount(RemoteInerfaces.Entities.Account account)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                if (new HouseService().getHouse(account.id_house) != null) { 
                    account.Privilege = "RESIDENT";
                    cnn.ExecuteScalar($"INSERT INTO account  (id_house, first_name, last_name, birthdate, privilege, face_id, profile_image, password, is_active) VALUES (@id_house, @first_name, @last_name, @birthdate, @privilege, @face_id, @profile_image, @password, @is_active)", account);
                    Console.WriteLine("RESIDENT account create with success");
                }
                else
                {
                    throw new Exception("Wrong House Key");
                }
            }
        }

        public void editAccount(RemoteInerfaces.Entities.Account account)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar("UPDATE account SET first_name = @first_name, last_name = @last_name, bithdate = @birthdate, password = @password, profile_image = @profile_image WHERE (id = @id)", account);
                Console.WriteLine("account edited with success");
            }
        }

        public void activateAccount(int id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar($"UPDATE account SET is_active = 'true' WHERE id = '{id}'");
                Console.WriteLine("account activated with success");
            }
        }

        public void disactivateAccount(int id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar($"UPDATE account SET is_active = 'false' WHERE id = '{id}'");
                Console.WriteLine("account disactivated with success");
            }
        }

        public RemoteInerfaces.Entities.Account getAccount(int id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                RemoteInerfaces.Entities.Account account = cnn.Query<RemoteInerfaces.Entities.Account>($"SELECT * FROM account WHERE id = '{id}'").FirstOrDefault();
                return account;
            }
        }

        public List<RemoteInerfaces.Entities.Account> getHouseAccount(int house_id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                List<RemoteInerfaces.Entities.Account> accounts = cnn.Query<RemoteInerfaces.Entities.Account>($"SELECT * FROM account WHERE id_house = '{house_id}'").ToList();
                return accounts;
            }
        }

        public List<RemoteInerfaces.Entities.FaceID> getAccountFacesWithIDs()
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                List<RemoteInerfaces.Entities.FaceID> accountFaces = cnn.Query<RemoteInerfaces.Entities.FaceID>($"SELECT id, face_id FROM account").ToList();
                return accountFaces;

            }
        }

        public void deleteAccount(int id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar($"DELETE FROM account WHERE id = '{id}'");
                Console.WriteLine("account deleted with success");
            }
        }


    }

}
