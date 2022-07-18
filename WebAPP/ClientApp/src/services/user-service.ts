import { Injectable } from "@angular/core";
import { HttpRequestMethods } from "../shared/functions/http-request-methods.class";
import { User } from "../dto/user";
import { UtilsFunctions } from "../shared/functions/utils-functions";

@Injectable()
export class UserService extends HttpRequestMethods {

    update(uUser: User) {
        uUser.gender = UtilsFunctions.StringToBoolean(uUser.gender);
        
        return this.patch("user", uUser);
    }
}