import { Injectable } from "@angular/core";
import { HttpRequestMethods } from "../shared/functions/http-request-methods.class";
import { DLocation } from "../dto/dialogClass/location";
import { CreationObject } from "src/shared/functions/creation-object";
import { User } from "src/dto/user";
import { Structure } from "src/dto/structure";
import { isNullOrUndefined } from "util";

@Injectable()
export class AnonymousService extends HttpRequestMethods {

    public async connexion(email: string, password: string) {
        var rslt;
        rslt = await this.post("connexion", { email, password });
        if(!isNullOrUndefined(rslt["user"])){
            sessionStorage.setItem("USER", JSON.stringify(CreationObject.CreateUser(rslt["user"])));
        }
        else{
            sessionStorage.setItem("STRUCTURE", JSON.stringify(CreationObject.CreateUser(rslt["structure"])));
        }
        sessionStorage.setItem("TOKEN", JSON.stringify(rslt["token"]));
        return rslt;
    }

    // public async getLocation(street: string, zipCode: number) {
    //     var location = new DLocation();

    //     await this.getLatLong(street, zipCode).then(async rslt => {
    //         location.longitude = rslt["features"][0]["geometry"]["coordinates"][0];
    //         location.latitude = rslt["features"][0]["geometry"]["coordinates"][1];

    //         var context = rslt["features"][0]["properties"]["context"].split(", ")
    //         location.department = context[1];
    //         location.region = context[2];
    //     })

    //     return location;
    // }

    public async addViewToStay(stayId: number){
        await this.get(`view?stayId=${stayId}`);
    }

    public async userRegistration(newUser: User){
        return await this.post("user/registration", newUser);
    }

    public async structureRegistration(newStructure: Structure){
        return await this.post("structure/registration", newStructure);
    }
}