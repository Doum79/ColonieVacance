import { User } from "../../dto/user";
import { UtilsFunctions } from "./utils-functions";
import { Stay } from "../../dto/stay";
import { Structure } from "../../dto/structure";
import { Activity } from "../../dto/activity";
import { Thematic } from "src/dto/thematic";
import { Tag } from "src/dto/tag";
import { StayFurtherInformation } from "src/dto/stayFurtherInformation";
import { StructurePlay } from "../../dto/structdisplay";

export class CreationObject {

    public static CreateUser(object: Object) {
        return new User(
            object["id"],
            object["email"],
            object["password"],
            object["profil"],
            UtilsFunctions.StringToBoolean(object["gender"]),
            object["firstName"],
            object["lastName"],
            object["street"],
            object["city"],
            UtilsFunctions.StringToNumber(object["zipCode"]),
            UtilsFunctions.StringToNumber(object["phoneNumber"])
        );
    }

    public static CreateStructure(object: Object) {
        return new Structure(
            object["id"],
            object["email"],
            object["password"],
            object["profil"],
            object["street"],
            object["city"],
            object["zipCode"],
            object["department"],
            object["region"],
            object["country"],
            object["phoneNumber"],
            object["longitude"],
            object["latitude"],
            object["siret"],
            object["name"],
        );
    }

    public static CreateStay(object: Object) {
        return new Stay(
            object["id"],
            object["structureId"],
            object["title"],
            object["minYear"],
            object["maxYear"],
            object["abstract"],
            object["description"],
            object["program"],
            object["createDate"],
            object["housing"],
            object["moreInformations"],
            object["street"],
            object["postCode"],
            object["city"],
            object["state"],
            object["country"],
            object["latitude"],
            object["longitude"],
            object["phone"],
            object["partnersList"],
            object["equipmentsList"],
            object["accessesList"],
            object["furtherInformationsList"],
            object["picturesList"],
            object["activitiesList"],
            object["thematicsList"]
        );
    }

    public static CreateStayFurtherInformation(object: Object) {
        return new StayFurtherInformation(
            object["id"],
            object["stayId"],
            object["stay"],
            object["startDate"],
            object["endDate"],
            object["withTransport"],
            object["startCity"],
            object["price"],
            object["redirectionLink"],
        );
    }

    public static CreateActivity(object: Object) {
        return new Activity(
            object["id"],
            object["label"],
        );
    }

    public static CreateStruct(object: Object) {
        return new StructurePlay(
            object["id"],
            object["profil"],
        );
    }

    public static CreateThematic(object: Object) {
        return new Thematic(
            object["id"],
            object["label"],
        );
    }

    public static CreateTag(object: Object) {
        return new Tag(
            object["id"],
            object["label"],
        );
    }
}