import { Injectable } from "@angular/core";
import { HttpRequestMethods } from "../shared/functions/http-request-methods.class";
import { CreationObject } from "../shared/functions/creation-object";
import { Thematic } from "src/dto/thematic";

@Injectable()
export class ThematicService extends HttpRequestMethods {

    async listThematic() {
        var rslt = await this.get("thematic");
        var rsltTab = rslt as string[];

        var thematicsList = new Array<Thematic>();
        rsltTab.forEach(thematic => {
            thematicsList.push(CreationObject.CreateThematic(thematic));
        })
        return thematicsList;
    }

    
}