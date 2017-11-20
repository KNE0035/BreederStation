using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using BreederStationDataLayer.Orm.Dto;
using BreederStationDataLayer.Orm.SelectCriteria;
using BreederStationDataLayer.Database;
using System.Data.Common;

namespace BreederStationDataLayer.Orm.Dao
{
    public class OraclePersonGateway : PersonGateway
    {
        public static string TABLE_NAME = "Person";

        private static string SQL_INSERT_PERSON = @"INSERT INTO PERSON (LOGIN, FIRST_NAME, LAST_NAME, PHONE, BIRTH_DATE, ACTIVE, ROLE_ID, PASSWORD) 
                                                                        VALUES(:p_login, :p_first_name, :p_last_name, :p_phone, :p_birth_date, 1, :p_role_id, :p_password)";

        private static string SQL_INSERT_BREEDER = @"INSERT INTO BREEDER (PERSON_ID, ANIMAL_GROUP_ID) VALUES (:p_person_id, :p_animal_group_id)";

        private static string SQL_INSERT_CLEANER = @"INSERT INTO CLEANER (PERSON_ID, CHEMICAL_QUALIFICATION) VALUES (:p_person_id, :p_chemical_qualification)";

        private static string SQL_SELECT = @"SELECT p.id, p.LOGIN, p.FIRST_NAME, p.LAST_NAME, p.phone, p.BIRTH_DATE,
                                                   p.ACTIVE, p.LAST_ACTIVE_DATE, ag.id, ag.DESCRIPTION,
                                                   r.id, r.TYPE, r.DESCRIPTION, c.CHEMICAL_QUALIFICATION, ca.ID, ca.LENGTH_M, ca.WIDTH_M
                                            FROM PERSON p
                                            LEFT JOIN BREEDER b ON p.ID = b.PERSON_ID
                                            LEFT JOIN CLEANER c ON p.ID = c.PERSON_ID
                                            LEFT JOIN ANIMAL_GROUP ag ON b.ANIMAL_GROUP_ID = ag.ID
                                            LEFT JOIN ROLE r ON p.ROLE_ID = r.ID
                                            LEFT JOIN CAGE_CLEANER cc ON c.PERSON_ID = cc.CLEANER_ID
                                            LEFT JOIN CAGE ca ON cc.CAGE_ID = ca.ID";

        private static string SQL_SELECT_LOGIN = @"SELECT p.id, p.LOGIN, p.FIRST_NAME, p.LAST_NAME, p.phone, p.BIRTH_DATE,
                                                   p.ACTIVE, p.LAST_ACTIVE_DATE, ag.id, ag.DESCRIPTION,
                                                   r.id, r.TYPE, r.DESCRIPTION, c.CHEMICAL_QUALIFICATION, ca.ID, ca.LENGTH_M, ca.WIDTH_M
                                                   FROM PERSON p
                                                   LEFT JOIN BREEDER b ON p.ID = b.PERSON_ID
                                                   LEFT JOIN CLEANER c ON p.ID = c.PERSON_ID
                                                   LEFT JOIN ANIMAL_GROUP ag ON b.ANIMAL_GROUP_ID = ag.ID
                                                   LEFT JOIN ROLE r ON p.ROLE_ID = r.ID
                                                   LEFT JOIN CAGE_CLEANER cc ON c.PERSON_ID = cc.CLEANER_ID
                                                   LEFT JOIN CAGE ca ON cc.CAGE_ID = ca.ID
                                                   WHERE p.active = 1 AND p.login=:p_login";

        private static string SQL_UPDATE_PERSON = @"UPDATE Person SET first_name=:p_first_name, last_name=:p_last_name,
                                                    phone=:p_phone, birth_date=:p_birth_date WHERE login=:p_login";

        private static string SQL_UPDATE_BREEDER = "UPDATE Breeder SET animal_group_id=:p_animal_group_id WHERE person_id=:p_person_id";

        private static string SQL_UPDATE_CLEANER = "UPDATE Cleaner SET chemical_qualification=:p_chemical_qualification WHERE person_id=:p_person_id";

        private static string SQL_DELETE_LOGIN = "UPDATE Person SET active=:p_active, last_active_date=:p_last_active_date WHERE login=:p_login";

        private static string SQL_INSERT_RESPONSIBILITY_FOR_CAGE = "INSERT INTO CAGE_CLEANER (cleaner_id, cage_id) VALUES (:p_cleaner_id, :p_cage_id)";

        private static string SQL_DELETE_RESPONSIBILITY_FOR_CAGE = "DELETE FROM CAGE_CLEANER WHERE CAGE_ID=:p_cage_id AND CLEANER_ID=:p_cleaner_id";

        public OraclePersonGateway(IDatabaseService databaseService) : base (databaseService)
        {
        }

        protected override void PrepareUpdateInsertPersonCommand(DbCommand command, Person person)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_login", OracleDbType.Varchar2).Value = person.Login;
            oracleCommand.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = person.FirstName;
            oracleCommand.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = person.LastName;
            oracleCommand.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = person.Phone;
            oracleCommand.Parameters.Add("p_birth_date", OracleDbType.Date).Value = person.BirthDate;
            oracleCommand.Parameters.Add("p_password", OracleDbType.Varchar2).Value = person.Password;
            oracleCommand.Parameters.Add("p_role_id", OracleDbType.Int32).Value = person.Role.Type;
        }

        protected override void PrepareUpdateInsertBreederCommand(DbCommand command, Person person)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_animal_group_id", OracleDbType.Int32).Value = ((Breeder)person).AnimalGroup.Id;
            oracleCommand.Parameters.Add("p_person_id", OracleDbType.Int32).Value = person.Id;
        }

        protected override void PrepareUpdateInsertCleanerCommand(DbCommand command, Person person)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_chemical_qualification", OracleDbType.Char).Value = ((Cleaner)person).ChemicalQualification ? '1' : '0';
            oracleCommand.Parameters.Add("p_person_id", OracleDbType.Int32).Value = person.Id;
        }

        protected override void PrepareResponsibilityForCageCommand(DbCommand command, int idCleaner, int idCage)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_cage_id", OracleDbType.Int32).Value = idCage;
            oracleCommand.Parameters.Add("p_cleaner_id", OracleDbType.Int32).Value = idCleaner;
        }

        protected override void PrepareDeletePersonCommand(DbCommand command, string login)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_active", OracleDbType.Char).Value = 0;
            oracleCommand.Parameters.Add("p_last_active_date", OracleDbType.Date).Value = DateTime.Now;
            oracleCommand.Parameters.Add("p_login", OracleDbType.Varchar2).Value = login;
        }

        protected override void PrepareSelectLoginCommand(DbCommand command, string login)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_login", OracleDbType.Varchar2).Value = login;
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
            OracleDataReader oracleReader = (OracleDataReader)reader;
            bool goNext = false ;

            const int START_COLUMN_POSSITION_OF_CAGE_INFORMATION = 14;
            const int LOGIN_COLUMN_POSSITION = 1;
            IList<Person> persons = new List<Person>();

            while (goNext || oracleReader.Read())
            {
                int i = -1;
                Person person = new Person();
                Breeder breeder = null;
                Cleaner cleaner = null;
                person.Id = oracleReader.GetInt32(++i);
                person.Login = oracleReader.GetString(++i);
                person.FirstName = oracleReader.GetString(++i);
                person.LastName = oracleReader.GetString(++i);
                person.Phone = oracleReader.GetString(++i);
                person.BirthDate = oracleReader.GetDateTime(++i);
                person.Active = oracleReader.GetString(++i) == "1" ? true : false;

                if (!oracleReader.IsDBNull(++i))
                {
                    person.LastActiveDate = oracleReader.GetDateTime(i);
                }

                if (!oracleReader.IsDBNull(++i))
                {
                    breeder = new Breeder(person);
                    breeder.AnimalGroup = new AnimalGroup();
                    breeder.AnimalGroup.Id = oracleReader.GetInt32(i);
                    breeder.AnimalGroup.Description = oracleReader.GetString(++i);
                }
                else
                {
                    i += 1;
                }

                Role role = new Role();
                role.Id = oracleReader.GetInt32(++i);
                role.Type = RoleEnumUtils.getRoleType(oracleReader.GetString(++i));
                role.Description = oracleReader.GetString(++i);

                person.Role = role;

                if (!oracleReader.IsDBNull(++i))
                {
                    cleaner = new Cleaner(person);
                    cleaner.ChemicalQualification = oracleReader.GetString(i) == "1" ? true : false;
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


                if (!oracleReader.IsDBNull(START_COLUMN_POSSITION_OF_CAGE_INFORMATION))
                {
                    cleaner.Cages = new List<Cage>();
                    do
                    {
                        int j = START_COLUMN_POSSITION_OF_CAGE_INFORMATION - 1;
                        Cage cage = new Cage();
                        cage.Id = oracleReader.GetInt32(++j);
                        cage.LengthM = oracleReader.GetInt32(++j);
                        cage.WidthM = oracleReader.GetInt32(++j);
                        cleaner.Cages.Add(cage);
                        goNext = oracleReader.Read();
                    } while (goNext && lastLogin == oracleReader.GetString(LOGIN_COLUMN_POSSITION));
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
            OracleCommand oracleCommand = (OracleCommand)command;

            oracleCommand.BindByName = true;
            oracleCommand.CommandText += " WHERE ACTIVE = 1";
            if (criteria.Role != null)
            {
                oracleCommand.CommandText += " AND r.type=:c_r_type";
                oracleCommand.Parameters.Add("c_r_type", OracleDbType.Varchar2).Value = criteria.Role.ToString();
            }

            if (criteria.WholeName != null)
            {
                oracleCommand.CommandText += " AND (Lower(p.FIRST_NAME) || Lower(p.LAST_NAME)) like '%' || LOWER(:c_whole_name) || '%'";
                oracleCommand.Parameters.Add("c_whole_name", OracleDbType.Varchar2).Value = criteria.WholeName;
            }
            oracleCommand.CommandText += " ORDER BY p.LOGIN";
        }
    }
}
