import { User } from "../../dto/user";
import { StructureService } from "../../services/structure-service";
import { Structure } from "../../dto/structure";
import { CreationObject } from "./creation-object";
import { Stay } from "src/dto/stay";
import { AbstractControl, ValidatorFn } from "@angular/forms";

export class UtilsFunctions {

    public static StringToBoolean(el: boolean) {
        if (el == null)
            return null;

        if (el.toString() === "true")
            return true;

        if (el.toString() === "false")
            return false;

        return null;
    }

    public static StringToNumber(el: string) {
        if (el == null)
            return null;

        return parseInt(el);
    }

    public static InitUser(): any {
        if ((JSON.parse(sessionStorage.getItem("USER")) == null && JSON.parse(sessionStorage.getItem("STRUCTURE")) == null) || JSON.parse(sessionStorage.getItem("TOKEN")) == null)
            return null

        if (JSON.parse(sessionStorage.getItem("USER")) != null)
            return CreationObject.CreateUser(JSON.parse(sessionStorage.getItem("USER")));

        if (JSON.parse(sessionStorage.getItem("STRUCTURE")) != null)
            return CreationObject.CreateStructure(JSON.parse(sessionStorage.getItem("STRUCTURE")));
    }

    public static changeLoadingState(bool: boolean): boolean {
        return !bool;
    }

    public static dayEnToFr(day: string) {
        switch (day) {
            case "Monday":
                return "Lundi";
            case "Tuesday":
                return "Mardi";
            case "Wednesday":
                return "Mercredi";
            case "Thursday":
                return "Jeudi";
            case "Friday":
                return "Vendredi";
            case "Saturday":
                return "Samedi";
            case "Sunday":
                return "Dimanche";
        }
    }

    public static monthEnToFr(month: string) {
        switch (month) {
            case "January":
                return "Janvier";
            case "February":
                return "Février";
            case "March":
                return "Mars";
            case "April":
                return "Avril";
            case "May":
                return "Mai";
            case "June":
                return "Juin";
            case "July":
                return "Juillet";
            case "August":
                return "Août";
            case "September":
                return "Septembre";
            case "October":
                return "Octobre";
            case "November":
                return "Novembre";
            case "December":
                return "Décembre";
        }
    }

    public static async getBase64(file: any) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = error => reject(error);
        });
    }
}