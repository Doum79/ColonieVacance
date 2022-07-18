using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Exceptions
{
    public class BusinessException : Exception
    {
        public const string UNAUTHORIZED_OPERATION = "Opération non autorisé";
        public const string EMAIL_NOT_EXIST = "Adresse email non existante";
        public const string INVALID_PASSWORD = "Mot de passe incorrecte";
        public const string EMAIL_EXISTING = "Cette email existe déjà";
        public const string INVALID_DATA_STAY = "Champ(s) de séjour manquant";
        public const string UNAUTHORISE_OPERATION = "Opération non autorisé !";
        public string Code { get; set; }

        //ACTIVITY EXCEPTION
        public const string NO_ACTIVITY_LABEL = "Libéllé incorrect";

        //THEMATIC EXCEPTION
        public const string NO_THEMATIC_LABEL = "Libéllé incorrect";

        //USER EXCEPTION
        public const string NO_USER_EMAIL = "Adresse email incorrecte";
        public const string NO_USER_FIRSTNAME = "Prénom incorrect";
        public const string NO_USER_LASTNAME = "Nom incorrect";
        public const string NO_USER_STREET = "Rue incorrecte";
        public const string NO_USER_ZIPCODE = "Code postal incorrect";
        public const string NO_USER_CITY = "Ville incorrecte";
        public const string NO_USER_COUNTRY = "Pays incorrect";

        //STRUCTURE EXCEPTION
        public const string NO_STRUCTURE_EMAIL = "Adresse email incorrecte";
        public const string NO_STRUCTURE_NAME = "Nom de structure incorrect";
        public const string NO_STRUCTURE_SIRET = "SIRET incorrect";
        public const string NO_STRUCTURE_STREET = "Rue incorrecte";
        public const string NO_STRUCTURE_ZIPCODE = "Code postal incorrect";
        public const string NO_STRUCTURE_CITY = "Ville incorrecte";
        public const string NO_STRUCTURE_COUNTRY = "Pays incorrect";
        public const string STRUCTURE_SIRET_EXIST = "Ce numéro de SIRET éxiste déjà";

        //FURTHER INFORMATION EXCEPTION
        public const string ANY_STAY_FURTHER_INFORMATION = "Aucune donnée de tarifs et réservations remplies";
        public const string NO_STAY_FURTHER_INFORMATION_START_DATE = "Date de départ manquante";
        public const string NO_STAY_FURTHER_INFORMATION_END_DATE = "Date de fin manquante";
        public const string NO_STAY_FURTHER_INFORMATION_WITH_TRANSPORT = "Information de transport manquante";
        public const string NO_STAY_FURTHER_INFORMATION_START_CITY = "Ville de départ manquante";
        public const string NO_STAY_FURTHER_INFORMATION_PRICE = "Prix manquant";
        public const string NO_STAY_FURTHER_INFORMATION_REDIRECTION_LINK = "Lien de redirection manquant";


        public BusinessException(string code)
        {
            Code = code;
        }

        public BusinessException(string code, string message) : base(message)
        {
            Code = code;
        }

        public BusinessException(string code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }
    }
}
