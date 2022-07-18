import { Injectable } from "@angular/core";
import { HttpRequestMethods } from "../shared/functions/http-request-methods.class";
import { CreationObject } from "../shared/functions/creation-object";
import { Activity } from "../dto/activity";
import { Thematic } from "src/dto/thematic";

@Injectable()
export class ThematicStayService extends HttpRequestMethods {

    async listThematicsOfStay(stayId: number) {
        var rslt = await this.get(`staythematic/stay/${stayId}`);
        var rsltTab = rslt as string[];

        var thematicsList = new Array<Thematic>();
        rsltTab.forEach(thematic => {
            thematicsList.push(CreationObject.CreateThematic(thematic));
        })
        return thematicsList;
    }
}