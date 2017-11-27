using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Orm.SelectCriteria;
using BreederStationDataLayer.Database;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace BreederStationDataLayer.Orm.Dao
{
    public class SqlServerPersonGateway : PersonGateway
    {
        public static string TABLE_NAME = "Person";

        private static string SQL_INSERT_PERSON = @"INSERT INTO PERSON (LOGIN, FIRST_NAME, LAST_NAME, PHONE, BIRTH_DATE, ACTIVE, ROLE_ID, PASSWORD) 
                                                                        VALUES(@p_login, @p_first_name, @p_last_name, @p_phone, @p_birth_date, 1, @p_role_id, @p_password)";

        private static string SQL_INSERT_BREEDER = @"INSERT INTO BREEDER (PERSON_ID, ANIMAL_GROUP_ID) VALUES (@p_person_id, @p_animal_group_id)";

        private static string SQL_INSERT_CLEANER = @"INSERT INTO CLEANER (PERSON_ID, CHEMICAL_QUALIFICATION) VALUES (@p_person_id, @p_chemical_qualification)";

        private static string SQL_SELECT = @"SELECT p.id, p.password, p.LOGIN, p.FIRST_NAME, p.LAST_NAME, p.phone, p.BIRTH_DATE,
                                                   p.ACTIVE, p.LAST_ACTIVE_DATE, ag.id, ag.DESCRIPTION,
                                                   r.id, r.TYPE, r.DESCRIPTION, c.CHEMICAL_QUALIFICATION, ca.ID, ca.LENGTH_M, ca.WIDTH_M
                                            FROM PERSON p
                                            LEFT JOIN BREEDER b ON p.ID = b.PERSON_ID
                                            LEFT JOIN CLEANER c ON p.ID = c.PERSON_ID
                                            LEFT JOIN ANIMAL_GROUP ag ON b.ANIMAL_GROUP_ID = ag.ID
                                            LEFT JOIN ROLE r ON p.ROLE_ID = r.ID
                                            LEFT JOIN CAGE_CLEANER cc ON c.PERSON_ID = cc.CLEANER_ID
                                            LEFT JOIN CAGE ca ON cc.CAGE_ID = ca.ID";

        private static string SQL_SELECT_LOGIN = @"SELECT p.id, p.password, p.LOGIN, p.FIRST_NAME, p.LAST_NAME, p.phone, p.BIRTH_DATE,
                                                   p.ACTIVE, p.LAST_ACTIVE_DATE, ag.id, ag.DESCRIPTION,
                                                   r.id, r.TYPE, r.DESCRIPTION, c.CHEMICAL_QUALIFICATION, ca.ID, ca.LENGTH_M, ca.WIDTH_M
                                                   FROM PERSON p
                                                   LEFT JOIN BREEDER b ON p.ID = b.PERSON_ID
                                                   LEFT JOIN CLEANER c ON p.ID = c.PERSON_ID
                                                   LEFT JOIN ANIMAL_GROUP ag ON b.ANIMAL_GROUP_ID = ag.ID
                                                   LEFT JOIN ROLE r ON p.ROLE_ID = r.ID
                                                   LEFT JOIN CAGE_CLEANER cc ON c.PERSON_ID = cc.CLEANER_ID
                                                   LEFT JOIN CAGE ca ON cc.CAGE_ID = ca.ID
                                                   WHERE p.login=@p_login";

        private static string SQL_SELECT_ID= @"SELECT p.id, p.password, p.LOGIN, p.FIRST_NAME, p.LAST_NAME, p.phone, p.BIRTH_DATE,
                                                   p.ACTIVE, p.LAST_ACTIVE_DATE, ag.id, ag.DESCRIPTION,
                                                   r.id, r.TYPE, r.DESCRIPTION, c.CHEMICAL_QUALIFICATION, ca.ID, ca.LENGTH_M, ca.WIDTH_M
                                                   FROM PERSON p
                                                   LEFT JOIN BREEDER b ON p.ID = b.PERSON_ID
                                                   LEFT JOIN CLEANER c ON p.ID = c.PERSON_ID
                                                   LEFT JOIN ANIMAL_GROUP ag ON b.ANIMAL_GROUP_ID = ag.ID
                                                   LEFT JOIN ROLE r ON p.ROLE_ID = r.ID
                                                   LEFT JOIN CAGE_CLEANER cc ON c.PERSON_ID = cc.CLEANER_ID
                                                   LEFT JOIN CAGE ca ON cc.CAGE_ID = ca.ID
                                                   WHERE p.id=@p_id";

        private static string SQL_UPDATE_PERSON = @"UPDATE Person SET password=@p_password, login=@p_login, first_name=@p_first_name, last_name=@p_last_name,
                                                    phone=@p_phone, birth_date=@p_birth_date WHERE id=@p_id";

        private static string SQL_UPDATE_BREEDER = "UPDATE Breeder SET animal_group_id=@p_animal_group_id WHERE person_id=@p_person_id";

        private static string SQL_UPDATE_CLEANER = "UPDATE Cleaner SET chemical_qualification=@p_chemical_qualification WHERE person_id=@p_person_id";

        private static string SQL_DELETE_LOGIN = "UPDATE Person SET active=@p_active, last_active_date=@p_last_active_date WHERE id=@p_id";

        private static string SQL_INSERT_RESPONSIBILITY_FOR_CAGE = "INSERT INTO CAGE_CLEANER (cleaner_id, cage_id) VALUES (@p_cleaner_id, @p_cage_id)";

        private static string SQL_DELETE_RESPONSIBILITY_FOR_CAGE = "DELETE FROM CAGE_CLEANER WHERE CAGE_ID=@p_cage_id AND CLEANER_ID=@p_cleaner_id";

        public SqlServerPersonGateway(IDatabaseService databaseService) : base (databaseService)
        {
        }

        protected override void PrepareUpdateInsertPersonCommand(DbCommand command, Person person)
        {
            SqlCommand sqlCommand = (SqlCommand)command;

            sqlCommand.Parameters.Add("@p_login", SqlDbType.VarChar).Value = person.Login;
            sqlCommand.Parameters.Add("@p_password", SqlDbType.VarChar).Value = person.Password;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = person.Id;
            sqlCommand.Parameters.Add("@p_first_name", SqlDbType.VarChar).Value = person.FirstName;
            sqlCommand.Parameters.Add("@p_last_name", SqlDbType.VarChar).Value = person.LastName;
            sqlCommand.Parameters.Add("@p_phone", SqlDbType.VarChar).Value = person.Phone;
            sqlCommand.Parameters.Add("@p_birth_date", SqlDbType.Date).Value = person.BirthDate;
            sqlCommand.Parameters.Add("@p_role_id", SqlDbType.Int).Value = (int)person.Role.Type;
        }

        protected override void PrepareUpdateInsertBreederCommand(DbCommand command, Person person)
        {
            SqlCommand sqlCommand = (SqlCommand)command;

            sqlCommand.Parameters.Add("@p_animal_group_id", SqlDbType.Int).Value = ((Breeder)person).AnimalGroup.Id;
            sqlCommand.Parameters.Add("@p_person_id", SqlDbType.Int).Value = person.Id;
        }

        protected override void PrepareUpdateInsertCleanerCommand(DbCommand command, Person person)
        {
            SqlCommand sqlCommand = (SqlCommand)command;

            sqlCommand.Parameters.Add("@p_chemical_qualification", SqlDbType.Bit).Value = ((Cleaner)person).ChemicalQualification;
            sqlCommand.Parameters.Add("@p_person_id", SqlDbType.Int).Value = person.Id;
        }

        protected override void PrepareResponsibilityForCageCommand(DbCommand command, int idCleaner, int idCage)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_cage_id", SqlDbType.Int).Value = idCage;
            sqlCommand.Parameters.Add("@p_cleaner_id", SqlDbType.Int).Value = idCleaner;
        }

        protected override void PrepareDeletePersonCommand(DbCommand command, int id)
        {
            SqlCommand sqlCommand = (SqlCommand)command;

            sqlCommand.Parameters.Add("@p_active", SqlDbType.Int).Value = 0;
            sqlCommand.Parameters.Add("@p_last_active_date", SqlDbType.Date).Value = DateTime.Now;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = id;
        }

        protected override void PrepareSelectLoginCommand(DbCommand command, string login)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_login", SqlDbType.VarChar).Value = login;
        }

        protected override void PrepareSelectIdCommand(DbCommand command, int personId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.VarChar).Value = personId;
        }

        protected override string GetInsertPersonSql()
        {
            return SQL_INSERT_PERSON;
        }

        protected override string GetInsertBreederSql()
        {
            return SQL_INSERT_BREEDER;
        }

        protected override string GetInsertCleanerSql()
        {
            return SQL_INSERT_CLEANER;
        }

        protected override string GetDeletePersonSql()
        {
            return SQL_DELETE_LOGIN;
        }

        protected override string GetUpdatePersonSql()
        {
            return SQL_UPDATE_PERSON;
        }

        protected override string GetUpdateBreederSql()
        {
            return SQL_UPDATE_BREEDER;
        }

        protected override string GetUpdateCleanerSql()
        {
            return SQL_UPDATE_CLEANER;
        }

        protected override string GetSelectLoginSql()
        {
            return SQL_SELECT_LOGIN;
        }

        protected override string GetSelectSql()
        {
            return SQL_SELECT;
        }

        protected override string GetInsertCageResponsibilitySql()
        {
            return SQL_INSERT_RESPONSIBILITY_FOR_CAGE;
        }

        protected override string GetDeleteCageResponsibilitySql()
        {
            return SQL_DELETE_RESPONSIBILITY_FOR_CAGE;
        }

        protected override IList<Person> Read(DbDataReader reader)
        {
            SqlDataReader sqlReader = (SqlDataReader)reader;
            bool goNext = false ;

            const int START_COLUMN_POSSITION_OF_CAGE_INFORMATION = 15;
            const int LOGIN_COLUMN_POSSITION = 2;
            IList<Person> persons = new List<Person>();

            while (goNext || sqlReader.Read())
            {
                int i = -1;
                Person person = new Person();
                Breeder breeder = null;
                Cleaner cleaner = null;
                person.Id = sqlReader.GetInt32(++i);
                person.Password = sqlReader.GetString(++i);
                person.Login = sqlReader.GetString(++i);
                person.FirstName = sqlReader.GetString(++i);
                person.LastName = sqlReader.GetString(++i);
                person.Phone = sqlReader.GetString(++i);
                person.BirthDate = sqlReader.GetDateTime(++i);
                person.Active = sqlReader.GetBoolean(++i);

                if (!sqlReader.IsDBNull(++i))
                {
                    person.LastActiveDate = sqlReader.GetDateTime(i);
                }

                if (!sqlReader.IsDBNull(++i))
                {
                    breeder = new Breeder(person);
                    breeder.AnimalGroup = new AnimalGroup();
                    breeder.AnimalGroup.Id = sqlReader.GetInt32(i);
                    breeder.AnimalGroup.Description = sqlReader.GetString(++i);
                    person = breeder;
                }
                else
                {
                    i += 1;
                }

                Role role = new Role();
                role.Id = sqlReader.GetInt32(++i);
                role.Type = RoleEnumUtils.getRoleType(sqlReader.GetString(++i));
                role.Description = sqlReader.GetString(++i);

                person.Role = role;

                if (!sqlReader.IsDBNull(++i))
                {
                    cleaner = new Cleaner(person);
                    cleaner.ChemicalQualification = sqlReader.GetBoolean(i);
                    person = cleaner;
                }

                if (breeder != null)
                {
                    breeder.Role = role;
                }
                else if(cleaner != null)
                {
                    cleaner.Role = role;
                } else
                {
                    person.Role = role;
                }

                string lastLogin = person.Login;


                if (!sqlReader.IsDBNull(START_COLUMN_POSSITION_OF_CAGE_INFORMATION))
                {
                    cleaner.Cages = new List<Cage>();
                    do
                    {
                        int j = START_COLUMN_POSSITION_OF_CAGE_INFORMATION - 1;
                        Cage cage = new Cage();
                        cage.Id = sqlReader.GetInt32(++j);
                        cage.LengthM = sqlReader.GetInt32(++j);
                        cage.WidthM = sqlReader.GetInt32(++j);
                        cleaner.Cages.Add(cage);
                        goNext = sqlReader.Read();
                    } while (goNext && lastLogin == sqlReader.GetString(LOGIN_COLUMN_POSSITION));
                } else
                {
                    goNext = false;
                }

                persons.Add(person);
            }
            return persons;
        }

        protected override void ApplyCriteriaAndOrder(PersonCriteria criteria, DbCommand command)
        {
            SqlCommand sqlCommand = (SqlCommand)command;

            sqlCommand.CommandText += " WHERE ACTIVE = 1";
            if (criteria.Role != null)
            {
                sqlCommand.CommandText += " AND r.type=@c_r_type";
                sqlCommand.Parameters.Add("@c_r_type", SqlDbType.VarChar).Value = criteria.Role.ToString();
            }

            if (criteria.WholeName != null)
            {
                sqlCommand.CommandText += " AND (Lower(p.FIRST_NAME) || Lower(p.LAST_NAME)) like '%' || LOWER(@c_whole_name) || '%'";
                sqlCommand.Parameters.Add("@c_whole_name", SqlDbType.VarChar).Value = criteria.WholeName;
            }
            sqlCommand.CommandText += " ORDER BY p.LOGIN";
        }

        protected override string GetSelectIdSql()
        {
            return SQL_SELECT_ID;
        }
    }
}
